using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace svpp_lab6
{
    public class Flower
    {
        public int FlowerID { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Color { get; set; }
        public double FlowerPrice { get; set; }

        private static string connString = ConfigurationManager.ConnectionStrings["DemoConnection"].ConnectionString;

        public override string ToString()
        {
            return $"FlowerID = {FlowerID} - Name: {Name} - Country: {Country} - Color: {Color} - Flower Price: {FlowerPrice}";
        }

        public static List<Flower> GetAllFlowers()
        {
            List<Flower> list = new List<Flower>();
            string commandString = "SELECT FlowerID, Name, Country, Color, FlowerPrice FROM Flowers";
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand getAllCommand = new SqlCommand(commandString, connection);
                connection.Open();
                using (var reader = getAllCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Flower()
                        {
                            FlowerID = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Country = reader.IsDBNull(2) ? null : reader.GetString(2),
                            Color = reader.GetString(3),
                            FlowerPrice = reader.GetDouble(4)
                        });
                    }
                }
            }
            return list;
        }

        public void Insert()
        {
            string command = "INSERT INTO Flowers (Name, Country, Color, FlowerPrice) VALUES (@nm, @cntr, @clr, @flwrPrc)";
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand insertCommand = new SqlCommand(command, connection);
                insertCommand.Parameters.AddRange(new SqlParameter[]
                {
                    new SqlParameter("nm", Name),
                    new SqlParameter("cntr", Country),
                    new SqlParameter("clr", Color),
                    new SqlParameter("flwrPrc", FlowerPrice)
                });
                connection.Open();
                insertCommand.ExecuteNonQuery();
            }
        }
                
        public void Update()
        {
            string command = "UPDATE Flowers SET Name=@nm, Country=@cntr, Color=@clr, FlowerPrice=@flwrPrc WHERE FlowerID=@id";
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand updateCommand = new SqlCommand(command, connection);
                updateCommand.Parameters.AddRange(new SqlParameter[]
                {
                    new SqlParameter("nm", Name),
                    new SqlParameter("cntr", Country),
                    new SqlParameter("clr", Color),
                    new SqlParameter("flwrPrc", FlowerPrice),
                    new SqlParameter("id", FlowerID)
                });
                connection.Open();
                updateCommand.ExecuteNonQuery();
            }
        }

        public static void Delete(int id)
        {
            string command = "DELETE FROM Flowers WHERE FlowerID=@id";
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand deleteCommand = new SqlCommand(command, connection);
                deleteCommand.Parameters.Add(new SqlParameter("id", id));
                connection.Open();
                deleteCommand.ExecuteNonQuery();
            }
        }
    }
}
