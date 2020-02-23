using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace TrailTracker.API.Models
{
    public class TrailsContext
    {
        public string ConnectionString { get; set; }

        public TrailsContext(string connectionString)
        {
            ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        public List<Trail> GetAllTrails()
        {
            var trails = new List<Trail>();

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
                        trails.Add(new Trail
                        {
                            Id = (int)reader["id"],
                            Name = reader["Name"].ToString(),
                            Location = reader["Location"].ToString(),
                            Rating = (decimal)reader["Rating"]
                        });
                    }
                } 
            }

            return trails;
        }
    }
} 
