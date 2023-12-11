
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
    class WeatherCAD
    {
        private string constring { get; set; }
        public string ExitMsg;

        public WeatherCAD()
        {
            // Set connection to database
            constring = ConfigurationManager.ConnectionStrings["dbconnection"].ToString();
        }

        // Creates a new weather in the DB
        public DataSet CreateWeather(WeatherEN en)
        {
            DataSet dbVirtual = new DataSet();
            SqlConnection c = new SqlConnection(constring);
            SqlDataAdapter da = new SqlDataAdapter("select * from weathers", c);
            da.Fill(dbVirtual, "weathers");
            DataTable t = new DataTable();
            t = dbVirtual.Tables["weathers"];
            DataRow newWeather = t.NewRow();
            newWeather["temperature_avg"] = en.TemperatureAvg;
            newWeather["temperature_max"] = en.TemperatureMax;
            newWeather["temperature_min"] = en.TemperatureMin;
            newWeather["rain"] = en.Rain;
            newWeather["wind"] = en.Wind;
            newWeather["location_id"] = en.Location.Id;
            newWeather["date"] = en.Date;

            t.Rows.Add(newWeather);

            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(da);
            da.Update(dbVirtual, "weathers");
            return dbVirtual;
        }

        // Reads a weather from the DB
        public bool ReadWeather(WeatherEN en)
        {
            string sqlSelect = "select * from weathers where id=@id";
            SqlConnection c = new SqlConnection(constring);
            SqlDataReader dr;
            try
            {
                c.Open();
                SqlCommand com = new SqlCommand(sqlSelect, c);
                com.Parameters.AddWithValue("@id", en.Id);
                dr = com.ExecuteReader();
                if (!dr.Read()) // checks if weather exists in the db
                {
                    ExitMsg = $"Weather with id {en.Id} does not exist.";
                    Console.WriteLine($"Operation failed. Error: {ExitMsg}");
                    dr.Close();
                    c.Close();
                    return false;
                }
                en.TemperatureAvg = int.Parse(dr["temperatuerAvg"].ToString());
                en.TemperatureMin = int.Parse(dr["temperatuerMin"].ToString());
                en.TemperatureMax = int.Parse(dr["temperatuerMax"].ToString());
                en.Rain = int.Parse(dr["rain"].ToString());
                en.Wind = int.Parse(dr["wind"].ToString());
                en.Location.Id = int.Parse(dr["location_id"].ToString());
                en.Date = DateTime.Parse(dr["date"].ToString());
                dr.Close();
            }
            catch (Exception ex)
            {
                ExitMsg = ex.Message.ToString();
                Console.WriteLine($"Operation failed. Error: {ExitMsg}");
                c.Close();
                return false;
            }
            c.Close();
            return true;
        }

        // Returs a dataset of weathers of a location
        public DataSet ReadWeathersOfLocation(int location_id)
        {
            DataSet dbVirtual = new DataSet();
            SqlConnection c = new SqlConnection(constring);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("select * from weathers where location_id=@location_id", c);
            da.SelectCommand.Parameters.Add("@location_id", SqlDbType.Int).Value=location_id;
            da.Fill(dbVirtual, "weathers");
            return dbVirtual;
        }

        // Updates the values of a weather in the DB
        public bool UpdateWeather(WeatherEN en, int id)
        {
            // check if wheater exists
            if (!Exists(en))
            {
                return false;
            }

            string sqlUpdate = "update weathers set temperature_avg=@avg, temperature_max=@max, temperature_min=@min, rain=@rain, wind=@wind, location_id=@location_id, date=@date where id=@id";
            SqlConnection c = new SqlConnection(constring);

            try
            {
                c.Open();
                SqlCommand com = new SqlCommand(sqlUpdate, c);
                com.Parameters.AddWithValue("@id", en.Id);
                com.Parameters.AddWithValue("@avg", en.TemperatureAvg);
                com.Parameters.AddWithValue("@max", en.TemperatureMax);
                com.Parameters.AddWithValue("@min", en.TemperatureMin);
                com.Parameters.AddWithValue("@rain", en.Rain);
                com.Parameters.AddWithValue("@wind", en.Wind);
                com.Parameters.AddWithValue("@location_id", en.Location.Id);
                com.Parameters.AddWithValue("@date", en.Date);
                com.ExecuteNonQuery();
                c.Close();
                ExitMsg = "Weather successfully updated";
                return true;
            }
            catch (Exception ex)
            {
                ExitMsg = ex.Message.ToString();
                Console.WriteLine($"Operation failed. Error: {ExitMsg}");
                c.Close();
                return false;
            }
        }

        // Deletes a weather from the DB
        public bool DeleteWeather(WeatherEN en, int id)
        {
            // check if wheater exists
            if (!Exists(en))
            {
                return false;
            }

            string sqlDelete = "delete from weathers where id=@id";
            SqlConnection c = new SqlConnection(constring);

            try
            {
                c.Open();
                SqlCommand com = new SqlCommand(sqlDelete, c);
                com.Parameters.AddWithValue("@id", en.Id);
                com.ExecuteNonQuery();
                c.Close();
                ExitMsg = "Weather successfully deleted.";
                return true;
            }
            catch (Exception ex)
            {
                ExitMsg = ex.Message.ToString();
                Console.WriteLine($"Operation has failed. Error: {ExitMsg}");
                c.Close();
                return false;
            }
        }

        // Checks if a weather exists in the Database
        private bool Exists(WeatherEN en)
        {
            string sqlSelect = "select id from weathers where id=@id";
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
                    ExitMsg = $"Weather with id {en.Id} does not exist.";
                    Console.WriteLine($"Operation failed. Error: {ExitMsg}");
                    dr.Close();
                    c.Close();
                    return false;
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                ExitMsg = ex.Message.ToString();
                Console.WriteLine($"Operation failed. Error: {ExitMsg}");
                c.Close();
                return false;
            }
            c.Close();
            return true;
        }

        // Returns a dataset of all weathers
        public DataSet ReadAllWeathersDataSet()
        {
            DataSet dbVirtual = new DataSet();
            SqlConnection c = new SqlConnection(constring);
            SqlDataAdapter da = new SqlDataAdapter("select * from weathers", c);
            da.Fill(dbVirtual, "weathers");
            return dbVirtual;
        }
    }
}
