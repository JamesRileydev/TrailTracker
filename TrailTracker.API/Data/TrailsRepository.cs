using MySql.Data.MySqlClient;
using System.Collections.Generic;
using TrailTracker.API.Models;

namespace TrailTracker.API.Data
{
    public interface ITrailsRepository
    {
        Trail CreateTrail(Trail trail);

        int DeleteTrail(int id);

        List<Trail> GetTrails();

        Trail GetTrail(int id);

        int UpdateTrail(int id, Trail trail);
    }

    public class TrailsRepository : ITrailsRepository
    {
        public string ConnectionString { get; set; }

        private MySqlConnection GetConnection()
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

        public int UpdateTrail(int id, Trail trail)
        {
            //const string mysql = @"
            //        UPDATE `TrailTrackerDb`.`Trails`
            //           SET `name` = @" + nameof(trail.Name) + @", 
            //               `location` = @" + nameof(trail.Location) + @",
            //               `rating` = @" + nameof(trail.Rating) + @"
            //         WHERE `id` = @" + nameof(id) + @"
            //               ;";

            string sql = $"UPDATE TrailTrackerDb.Trails SET `name` = \"{trail.Name}\", `location` = \"{trail.Location}\", `rating` = {trail.Rating} WHERE `id` = {id};";

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(
                    sql, conn
                );

                var result = cmd.ExecuteNonQuery();

                conn.Close();

                return result;
            }
        }
    }
}
