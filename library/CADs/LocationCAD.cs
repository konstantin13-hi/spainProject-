using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace library
{
    class LocationCAD
    {
        private string constring;
        public int Id;
        public string Name;
        public string Country;
        public LocationCAD()
        {
            constring = ConfigurationManager.ConnectionStrings["dbconnection"].ToString();
        }
        public string Statusmessage { get; set; }
        /// <summary>
        /// Creates a new Location in the database
        /// </summary>
        public bool CreateLocation(LocationEN en)
        {
            try
            {
                DataSet dbVirtual = new DataSet();
                SqlConnection c = new SqlConnection(constring);
                SqlDataAdapter da = new SqlDataAdapter("select * from locations", c);
                da.Fill(dbVirtual, "locations");
                DataTable t = new DataTable();
                t = dbVirtual.Tables["locations"];
                DataRow newLocation = t.NewRow();
                newLocation["name"] = en.Name;
                newLocation["country"] = en.Country;

                t.Rows.Add(newLocation);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(da);
                da.Update(dbVirtual, "locations");
                Statusmessage = "Location successfully created.";
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                Statusmessage = e.Message;
                return false;
            }
        }
    
        /// <summary>
        /// Reads a location from the database
        /// </summary>
        public bool ReadLocation(LocationEN en)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(constring))
                {
                    connection.Open();
                    using (SqlCommand com = new SqlCommand("SELECT * FROM locations WHERE id = @id", connection))
                    {
                        com.Parameters.AddWithValue("@id", en.Id);
                        using (SqlDataReader dr = com.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                int.TryParse(dr["id"].ToString(), out Id);
                                Name = dr["name"].ToString();
                                Country = dr["country"].ToString();
                                connection.Close();
                                dr.Close();
                                Statusmessage = "Location read successfully.";
                                return true;
                            }
                            else
                            {
                                Statusmessage = "Location doesnt exist.";
                                connection.Close();
                                dr.Close();
                                return false;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                Statusmessage = e.Message;
                return false;
            }
        }
        public DataSet ReadLocationName(string search)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(constring))
                {
                    string sqlQuery = "SELECT * FROM locations WHERE (LOWER(name) = @name) OR (LOWER(country) = @name)";
                    SqlCommand command = new SqlCommand(sqlQuery, connection);
                    command.Parameters.AddWithValue("@name", search.ToLower());
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataSet dataset = new DataSet();
                    adapter.Fill(dataset, "searchResults");
                    return dataset;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                Statusmessage = e.Message;
                return null;
            }
        }

        /// <summary>
        /// Updates a location from the database
        /// </summary>
        public bool UpdateLocation(LocationEN en)
        {
            if (!Exists(en))
            {
                return false;
            }

            string sqlUpdate = "update locations set name=@name, country=@country where id=@id";
            SqlConnection c = new SqlConnection(constring);
            try
            {
                c.Open();
                SqlCommand com = new SqlCommand(sqlUpdate, c);
                com.Parameters.AddWithValue("@id", en.Id);
                com.Parameters.AddWithValue("@name", en.Name);
                com.Parameters.AddWithValue("@country", en.Country);
                com.ExecuteNonQuery();
                c.Close();
                Statusmessage = "Location successfully updated";
                return true;
            }
            catch (Exception e)
            {
                c.Close();
                Console.WriteLine("{0} Exception caught.", e);
                Statusmessage = e.Message;
                return false;
            }
        }
        /// <summary>
        /// Deletes a location from the database
        /// </summary>
        public bool DeleteLocation(LocationEN en)
        {
            if (!Exists(en))
            {
                return false;
            }

            string sqlDelete = "delete from locations where id=@id";
            SqlConnection c = new SqlConnection(constring);
            try
            {
                c.Open();
                SqlCommand com = new SqlCommand(sqlDelete, c);
                com.Parameters.AddWithValue("@id", en.Id);
                com.ExecuteNonQuery();
                c.Close();
                Statusmessage = "Location successfully deleted.";
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                Statusmessage = e.Message.ToString(); ;
                c.Close();
                return false;
            }
        }
        /// <summary>
        /// Gets a dataset of all locations
        /// </summary>
        public DataSet ReadAllLocationsDataSet()
        {
            DataSet dbVirtual = new DataSet();
            SqlConnection c = new SqlConnection(constring);
            SqlDataAdapter da = new SqlDataAdapter("select * from locations", c);
            da.Fill(dbVirtual, "locations");
            return dbVirtual;
        }
        /// <summary>
        /// Gets a dataset of 8 most popular locations
        /// </summary>
        public DataSet ReadPopularDataSet()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(constring))
                {
                    string sqlQuery = @"
                    SELECT TOP 8 l.*
                    FROM locations l
                    JOIN accomodations a ON l.id = a.location_id
                    JOIN reviews r ON r.accomodation_id = a.id
                    ORDER BY r.pointsOverall DESC";

                    SqlCommand command = new SqlCommand(sqlQuery, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataSet dataset = new DataSet();
                    adapter.Fill(dataset, "Locations");
                    return dataset;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                Statusmessage = e.Message;
                return null;
            }
        }
        /// <summary>
        /// Gets a dataset of top 8 accomodations with the same location_id as the 
        /// entered location page 
        /// </summary>
        public DataSet ReadAccomodations(LocationEN en)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(constring))
                {
                    string sqlQuery = @"
                    SELECT TOP 8 * FROM 
                    accomodations a
                    JOIN locations ON (a.location_id = locations.id) 
                    WHERE a.location_id = @id";
                    SqlCommand command = new SqlCommand(sqlQuery, connection);
                    command.Parameters.AddWithValue("@id", en.Id);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataSet dataset = new DataSet();
                    adapter.Fill(dataset, "Accomodations");
                    return dataset;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                Statusmessage = e.Message;
                return null;
            }
        }
        /// <summary>
        /// Gets a dataset of top 8 of the landmarks where the location_id is 
        /// same as the entered location page
        /// </summary>
        public DataSet ReadLandmarks(LocationEN en)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(constring))
                {
                    string sqlQuery = @"
                    SELECT TOP 8 * 
                    FROM landmarks 
                    JOIN locations ON (landmarks.location_id = locations.id) 
                    where location_id = @id";
                    SqlCommand command = new SqlCommand(sqlQuery, connection);
                    command.Parameters.AddWithValue("@id", en.Id);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataSet dataset = new DataSet();
                    adapter.Fill(dataset, "Landmarks");
                    return dataset;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                Statusmessage = e.Message;
                return null;
            }
        }
        public bool Exists(LocationEN en)
        {
            string sqlSelect = "select id from locations where id=@id";
            SqlConnection c = new SqlConnection(constring);
            SqlDataReader dr;
            try
            {
                c.Open();
                SqlCommand com = new SqlCommand(sqlSelect, c);
                com.Parameters.AddWithValue("@id", en.Id);
                dr = com.ExecuteReader();
                if (!dr.Read())
                {
                    Statusmessage = $"Location with id {en.Id} does not exist.";
                    Console.WriteLine($"Operation failed. Error: {Statusmessage}");
                    dr.Close();
                    c.Close();
                    return false;
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                Statusmessage = ex.Message.ToString();
                Console.WriteLine($"Operation failed. Error: {Statusmessage}");
                c.Close();
                return false;
            }
            c.Close();
            return true;
        }
    }
}
