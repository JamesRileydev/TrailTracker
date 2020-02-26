using MySql.Data.MySqlClient;
using System.Collections.Generic;
using TrailTracker.API.Models;
using Dapper;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace TrailTracker.API.Data
{
    public interface ITrailsRepository
    {
        Task<int> CreateTrail(Trail trail);

        Task<int> DeleteTrail(int id);

        Task<List<Trail>> GetAllTrails();

        Task<Trail> GetTrail(int id);

        Task<int> UpdateTrail(int id, Trail trail);
    }

    public class TrailsRepository : ITrailsRepository
    {
        public string ConnectionString { get; set; }

        private IDbConnection DbConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        public TrailsRepository(IConfiguration config)
        {
            ConnectionString = config.GetSection("ConnectionStrings").GetValue<string>("DefaultConnection");
        }

        public async Task<int> CreateTrail(Trail trail)
        {
            const string sql = @"
                     INSERT INTO TrailTrackerDb.Trails (
                        `name`,
                        `location`,
                        `rating`
                        ) VALUES (
                        @" + nameof(trail.Name) + @", 
                        @" + nameof(trail.Location) + @",
                        @" + nameof(trail.Rating) + @"
                        )";

            using var conn = DbConnection();

            var result = await conn.ExecuteAsync(sql, trail).ConfigureAwait(false);
            return result;
        }

        public async Task<int> DeleteTrail(int id)
        {
            const string sql = @"
                DELETE FROM TrailTrackerDb.Trails
                      WHERE `id` = @" + nameof(id) + @"
                    ";

            using var conn = DbConnection();

            var result = await conn.ExecuteAsync(sql, new { id }).ConfigureAwait(false);
            return result;
        }

        public async Task<List<Trail>> GetAllTrails()
        {
            var sql = @"SELECT * FROM TrailTrackerDb.Trails;";

            using var db = DbConnection();

            var trails = await db.QueryAsync<Trail>(sql).ConfigureAwait(false);
            return trails.ToList();
        }

        public async Task<Trail> GetTrail(int id)
        {
            const string sql = @"
                        SELECT `id`,
                               `name`,
                               `location`,
                               `rating`
                          FROM TrailTrackerDb.Trails
                         WHERE `id` = @" + nameof(id) + @"
                               ";

            using var conn = DbConnection();

            var result = await conn.QueryFirstOrDefaultAsync<Trail>(sql, new { id }).ConfigureAwait(false);
            return result;
        }

        public async Task<int> UpdateTrail(int id, Trail trail)
        {
            const string sql = @"
                    UPDATE `TrailTrackerDb`.`Trails`
                       SET `name` = @" + nameof(trail.Name) + @", 
                           `location` = @" + nameof(trail.Location) + @",
                           `rating` = @" + nameof(trail.Rating) + @"
                     WHERE `id` = @" + nameof(id) + @"
                           ;";

            using var db = DbConnection();

            var result = await db.ExecuteAsync(sql, trail).ConfigureAwait(false);
            return result;
        }
    }
}

