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
    public class FavoriteCAD
    {
        public string ExitMsg { get; set; }
        private readonly string constring ;

        public FavoriteCAD()
        {
             constring = ConfigurationManager.ConnectionStrings["dbconnection"].ToString(); ;
        }

        public DataSet CreateFavorite(int userId, int accomodationId)
        {
            DataSet dbVirtual = new DataSet();
            SqlConnection c = new SqlConnection(constring);
            SqlDataAdapter da = new SqlDataAdapter("select * from favorites", c);
            da.Fill(dbVirtual, "favorites");
            DataTable t = dbVirtual.Tables["favorites"];

            DataRow newRow = t.NewRow();
            newRow["user_id"] = userId;
            newRow["accomodation_id"] = accomodationId;
            t.Rows.Add(newRow);

            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(da);
            da.Update(dbVirtual, "favorites");

            return dbVirtual;
        }

        public DataSet DeleteFavorite(int userId, int accomodationId)
        {
            DataSet dbVirtual = new DataSet();
            SqlConnection c = new SqlConnection(constring);
            SqlDataAdapter da = new SqlDataAdapter($"select * from favorites where user_id={userId} and accomodation_id={accomodationId}", c);
            da.Fill(dbVirtual, "favorites");
            DataTable t = dbVirtual.Tables["favorites"];

            DataRow[] rowsToDelete = t.Select();
            foreach (DataRow row in rowsToDelete)
            {
                row.Delete();
            }

            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(da);
            da.Update(dbVirtual, "favorites");

            return dbVirtual;
        }

        public DataSet ReadFavorite(int userId, int accomodationId)
        {
            DataSet dbVirtual = new DataSet();
            SqlConnection c = new SqlConnection(constring);
            SqlDataAdapter da = new SqlDataAdapter($"select * from favorites where user_id={userId} and accomodation_id={accomodationId}", c);
            da.Fill(dbVirtual, "favorites");
            return dbVirtual;
        }


        public DataSet ReadFavoriteAll(int userId)
        {
            DataSet dbVirtual = new DataSet();
            SqlConnection c = new SqlConnection(constring);
            SqlDataAdapter da = new SqlDataAdapter($"SELECT * FROM favorites JOIN accommodations ON favorites.accommodation_id = accommodations.id WHERE user_id={userId}", c);
            da.Fill(dbVirtual, "favorites");
            return dbVirtual;
        }

        public bool CreateFavoriteBool(int userId, int accomodationId)
        {
            
            if (this.ReadFavoriteBool(userId, accomodationId))
            {
                ExitMsg = "Already exists ";
                return false;

            }

            string sqlInsert = "INSERT INTO favorites (user_id, accomodation_id) VALUES (@userId, @accomodationId)";

            using (SqlConnection c = new SqlConnection(constring))
            {
                try
                {
                    c.Open();
                    SqlCommand com = new SqlCommand(sqlInsert, c);
                    com.Parameters.AddWithValue("@userId", userId);
                    com.Parameters.AddWithValue("@accomodationId", accomodationId);
                    com.ExecuteNonQuery();
                    ExitMsg = "Favorite created successfully.";
                    c.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    c.Close();
                    ExitMsg = $"Error creating favorite: {ex.Message}";
                    return false;
                }
            }


        }

        public bool ReadFavoriteBool(int userId, int accomodationId)
        {
            string sqlRequest = "SELECT * FROM favorites WHERE user_id = @userId AND accomodation_id = @accomodationId";
            using (SqlConnection c = new SqlConnection(constring))
            {
                try
                {
                    c.Open();
                    SqlCommand com = new SqlCommand(sqlRequest, c);
                    com.Parameters.AddWithValue("@userId", userId);
                    com.Parameters.AddWithValue("@accomodationId", accomodationId);
                    // хранит результаты выполненного SQL-запроса в виде результирующего набора. 
                    SqlDataReader reader = com.ExecuteReader();
                    bool isFavorite = reader.HasRows;

                    c.Close();

                    return isFavorite;
                }
                catch (Exception ex)
                {
                    c.Close();
                    ExitMsg = $"Read error: {ex.Message}";
                    return false;
                }
            }
        }


        public bool DeleteFavoriteBool(int userId, int accomodationId)
        {
            if (!ReadFavoriteBool(userId, accomodationId))
            {
                ExitMsg = "Record does not exist.";
                return false;
            }

            string sqlDelete = "DELETE FROM favorites WHERE user_id = @userId AND accomodation_id = @accomodationId";

            using (SqlConnection c = new SqlConnection(constring))
            {
                try
                {
                    c.Open();
                    SqlCommand com = new SqlCommand(sqlDelete, c);
                    com.Parameters.AddWithValue("@userId", userId);
                    com.Parameters.AddWithValue("@accomodationId", accomodationId);
                    int rowsAffected = com.ExecuteNonQuery();
                    c.Close();

                    if (rowsAffected > 0)
                    {
                        ExitMsg = "Favorite deleted successfully.";
                        return true;
                    }
                    else
                    {
                        ExitMsg = "Favorite not found or delete operation failed.";
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    c.Close();
                    ExitMsg = $"Error deleting favorite: {ex.Message}";
                    return false;
                }
            }
        }
    }
}
