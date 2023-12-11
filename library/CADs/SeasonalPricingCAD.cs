using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;

namespace library
{
    public class SeasonalPricingCAD
    {
        private string _constring;
        private string constring
        {
            get { return _constring; }
            set { _constring = value; }
        }

        public string message;

        public SeasonalPricingCAD()
        {
            constring = ConfigurationManager.ConnectionStrings["dbconnection"].ToString();
        }

        // Creates a new user in the DB with the SeasonalPricing data represented by the parameter en
        public bool createSeasonalPricing(SeasonalPricingEN en)
        {
            SqlConnection con = new SqlConnection(constring);
            try
            {
                con.Open();
                string s = "INSERT INTO seasonal_prices (name, startDate, endDate, multiplierPrize) OUTPUT INSERTED.ID VALUES (@name, @startDate, @endDate, @multiplierPrize)";
                SqlCommand cmd = new SqlCommand(s, con);
                cmd.Parameters.AddWithValue("@name", en.NamePricing);
                cmd.Parameters.AddWithValue("@startDate", en.StartDate);
                cmd.Parameters.AddWithValue("@endDate", en.EndDate);
                cmd.Parameters.AddWithValue("@multiplierPrize", en.MultiplierPricing);
                int insertedId = (int)cmd.ExecuteScalar();
                en.Id = insertedId;
                message = "New SeasonalPricing successfully created.";
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                con.Close();
                this.message = "SeasonalPricing could not be created.";
                return false; 
            }
        }

        //Returns only the indecate SeasonalPricing read from DB
        public bool readSeasonalPricing(SeasonalPricingEN en)
        {
            SqlConnection con = new SqlConnection(constring);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from seasonal_prices where id = @SeasonalPricingID", con);
                cmd.Parameters.AddWithValue("@SeasonalPricingID", en.Id);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        en.NamePricing = (string)reader["name"];
                        en.StartDate = (DateTime)reader["startDate"]; //"Die angegebene Umwandlung ist ungültig."
                        en.EndDate = (DateTime)reader["endDate"];
                        en.MultiplierPricing = (double)reader["multiplierPrize"];
                        message = "SeasonalPricing loaded successfully.";
                        con.Close();
                        return true;
                    }
                    else
                    {
                        con.Close();
                        message = "No SeasonalPricing with the given ID exists.";
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                message = "Error occured: SeasonalPricing could not be loaded.";
                con.Close();
                return false;
            }
        }

        // Updates the data of a SeasonalPricing in the DB with hte data of the user represented by the prameter en.
        public bool updateSeasonalPricing(SeasonalPricingEN en)
        {
            SqlConnection con = new SqlConnection(constring);
            try
            {
                con.Open();
                SqlCommand update = new SqlCommand("update seasonal_prices set name = @namepricing, startDate = @startDate, enddate = @endDate, multiplierPrize = @multiplierPricing where id = @id", con);
                update.Parameters.AddWithValue("@id", en.Id);
                update.Parameters.AddWithValue("@namepricing", en.NamePricing);
                update.Parameters.AddWithValue("@startDate", en.StartDate);
                update.Parameters.AddWithValue("@endDate", en.EndDate);
                update.Parameters.AddWithValue("@multiplierPricing", en.MultiplierPricing);
               if (update.ExecuteNonQuery() == 0)
                {
                    con.Close();
                    message = "SeasonalPricing could not be updated.";
                    return false;
                } else
                {
                    con.Close();
                    message = "SeasonalPricing successfully updated.";
                    return true;
                }
            }
            catch (Exception e)
            {
                con.Close();
                message = "Error occured: SeasonalPricing could not be updated.";
                return false;
            }
        }

        // Deletes the SeasonalPricing represented by en in the DB
        public bool deleteSeasonalPricing(SeasonalPricingEN en)
        {
            SqlConnection con = new SqlConnection(constring);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("delete from seasonal_prices where id = @id", con);
                cmd.Parameters.AddWithValue("@id", en.Id);
                int affected = cmd.ExecuteNonQuery();
                if (affected > 0)
                {
                    message = "SeasonalPricing deleted successfully.";
                    con.Close();
                    return true;
                } else
                {
                    message = "SeasonalPricing does not exist and can therefore not be deleted.";
                    con.Close();
                    return false;
                }
            } catch (Exception ex)
            {
                message = "Error occured: SeasonalPricing could not be deleted.";
                con.Close();
                return false;
            }
        }

        // Returns all SeasonalPricings 
        public List<SeasonalPricingEN> readAllSeasonalPricings()
        {
            List<SeasonalPricingEN> seasonalPricings = new List<SeasonalPricingEN>();

            using (SqlConnection con = new SqlConnection(constring))
            {
                try
                {
                    con.Open();
                    string query = "SELECT * FROM seasonal_prices";
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        SeasonalPricingEN seasonalPricing = new SeasonalPricingEN();
                        int id = (int)reader["id"];
                        seasonalPricing.Id = id;
                        seasonalPricing.readSeasonalPricing();
                        seasonalPricings.Add(seasonalPricing);
                    }
                    message = "SeasonalPricings loaded successfully.";
                    reader.Close();
                }
                catch (Exception ex)
                {
                    message = "Error occured: SeasonalPricings could not be loaded.";
                }
                finally
                {
                    con.Close();
                }
            }

            return seasonalPricings;
        }

        public DataSet ReadAllSeasonalPricesDataSet()
        {
            DataSet dbVirtual = new DataSet();
            SqlConnection c = new SqlConnection(constring);
            SqlDataAdapter da = new SqlDataAdapter("select * from seasonal_prices", c);
            da.Fill(dbVirtual, "seasonal_prices");
            return dbVirtual;
        }
    }
}
