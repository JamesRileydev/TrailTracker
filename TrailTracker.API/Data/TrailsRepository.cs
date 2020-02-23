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

        int Update(int id, Trail trail);
    }


    public class TrailsRepository : ITrailsRepository
    {
        public string ConnectionString { get; set; }

        public TrailsRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        public Trail CreateTrail(Trail trail)
        {
            return new Trail();
        }

        public int DeleteTrail(int id)
        {
            return 0;
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

        public Trail GetTrail(int id)
        {
            return new Trail();
        }

        public int Update(int id, Trail trail)
        {
            return 1;
        }
    }
}
