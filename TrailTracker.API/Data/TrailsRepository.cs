using MySql.Data.MySqlClient;
using System.Collections.Generic;
using TrailTracker.API.Models;
using Dapper;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace TrailTracker.API.Data
{
    public interface ITrailsRepository
    {
        Trail CreateTrail(Trail trail);

        int DeleteTrail(int id);

        List<Trail> GetAllTrails();

        List<Trail> GetTrails();

        Trail GetTrail(int id);

        Task<int> UpdateTrail(int id, Trail trail);
    }

    public class TrailsRepository : ITrailsRepository
    {
        public string ConnectionString { get; set; }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        private IDbConnection DbConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        public TrailsRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public Trail CreateTrail(Trail trail)
        {
            return new Trail();
        }

        public int DeleteTrail(int id)
        {
            return 0;
        }

        public List<Trail> GetAllTrails()
        {
            var sql = @"SELECT * FROM TrailTrackerDb.Trails;";

            using var db = DbConnection();

            var trails = db.Query<Trail>(sql).ToList();
            return trails;
        }

        public Trail GetTrail(int id)
        {
            var trail = new Trail();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var cmd = new MySqlCommand(
                    $"SELECT * FROM TrailTrackerDb.Trails WHERE id = {id};", conn
                    );

                using (var reader = cmd.ExecuteReader())
                {
                    reader.Read();

                    trail.Id = (int)reader["id"];
                    trail.Name = reader["Name"].ToString();
                    trail.Location = reader["Location"].ToString();
                    trail.Rating = (decimal)reader["Rating"];
                }
            }

            return trail;
        }

        public List<Trail> GetTrails()
        {
            var trailList = new List<Trail>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(
                    "SELECT * FROM TrailTrackerDb.Trails", conn
                );

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        trailList.Add(new Trail
                        {
                            Id = (int)reader["id"],
                            Name = reader["Name"].ToString(),
                            Location = reader["Location"].ToString(),
                            Rating = (decimal)reader["Rating"]
                        });
                    }
                }
            }

            return trailList;
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

