using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Configuration;

namespace library
{
    class LandmarkCAD
    {

        public string StatusMsg { get; set; }

        string s;

        //LandmarkCAD constructor 
        public LandmarkCAD()
        {
            //connecting string to database
            s = ConfigurationManager.ConnectionStrings["dbconnection"].ToString();
        }

        //Takes LandmarkEN object as input and add new landmark to DB; returns bool
        public bool CreateLandmark(LandmarkEN markEn)
        {
            bool inserted;
            DataSet dbvirtual = new DataSet();
            SqlConnection c = new SqlConnection(s);
            
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from landmarks", c);               
                da.Fill(dbvirtual, "landmarks");
                DataTable t = new DataTable();
                t = dbvirtual.Tables["landmarks"];
                DataRow newLandmark = t.NewRow();

                newLandmark["name"] = markEn.Name;
                newLandmark["location_id"] = markEn.Location_id;
                newLandmark["pricerange"] = markEn.Price;
                newLandmark["type"] = markEn.Type;
                newLandmark["adress"] = markEn.Address;
                newLandmark["websiteLink"] = markEn.WebsiteLink;
                t.Rows.Add(newLandmark);

                SqlCommandBuilder cbuilder = new SqlCommandBuilder(da);
                da.Update(dbvirtual, "landmarks");
                markEn.StatusMsg = "Landmark added successfully!";
                inserted = true;
            }
            catch (Exception ex) { inserted = false; markEn.StatusMsg = "Adding landmark failed"; }
            finally { c.Close(); }
            return inserted;
            
        }

        //Method to read all distinct landmark locations from DB, returns dataset of locations
        public DataSet readLandmarkLocation() 
        {
            SqlConnection c = new SqlConnection(s);
            SqlDataAdapter da = new SqlDataAdapter("select distinct landmarks.location_id, locations.id loc_id, locations.name loc_name from landmarks join locations on landmarks.location_id = locations.id", c);
            DataSet ds = new DataSet();
            da.Fill(ds, "Landmark"); //data-adapter fills dataset ds and names this table landmark
             
            return ds; 
        }

        //Admins use this method to update landmark information on database, returns bool
        public bool UpdateLandmark(LandmarkEN markEn)
        {
            bool updated = false;

            if (!Exists(markEn))
            {
                return false;
            }

            string sqlUpdate = "update landmarks set name=@name, type=@type, pricerange=@pricerange, location_id=@location_id, adress=@adress, websiteLink=@websiteLink where id=@id";
            SqlConnection c = new SqlConnection(s);
            try
            {
                c.Open();
                SqlCommand com = new SqlCommand(sqlUpdate, c);
                com.Parameters.AddWithValue("@id", markEn.Id);
                com.Parameters.AddWithValue("@name", markEn.Name);
                com.Parameters.AddWithValue("@type", markEn.Type);
                com.Parameters.AddWithValue("@pricerange", markEn.Price);
                com.Parameters.AddWithValue("@location_id", markEn.Location_id);
                com.Parameters.AddWithValue("@adress", markEn.Address);
                com.Parameters.AddWithValue("@websiteLink", markEn.WebsiteLink);
                com.ExecuteNonQuery();
                c.Close();
                markEn.StatusMsg = "Landmark successfully updated";
                updated = true;
            }
            catch (Exception ex)
            {
                markEn.StatusMsg = "Landmark wasn't updated";
                c.Close();
            }

            return updated;
        }
        //Admins can use this method to delete landmark from database, returns bool
        public bool DeleteLandmark(LandmarkEN markEn)
        {

            if (!Exists(markEn))
            {
                return false;
            }

            bool deleted = false;
            string sqlDelete = "delete from landmarks where id=@id";
            SqlConnection c = new SqlConnection(s);
            try
            {
                c.Open();
                SqlCommand com = new SqlCommand(sqlDelete, c);
                com.Parameters.AddWithValue("@id", markEn.Id);
                com.ExecuteNonQuery();
                c.Close();
                markEn.StatusMsg = "Landmark successfully deleted";
                deleted = true;
            }
            catch (Exception ex) 
            {
                markEn.StatusMsg = "Landmark wasn't deleted";
                c.Close();
            }

            return deleted;
        }


        //Method to read all landmarks from DB, returns dataset
        public DataSet ReadAllLandmarksDataSet()
        {
            DataSet dbVirtual = new DataSet();
            SqlConnection c = new SqlConnection(s);
            SqlDataAdapter da = new SqlDataAdapter("select * from landmarks", c);
            da.Fill(dbVirtual, "landmarks");
            return dbVirtual;
        }

        //Method to read only currently chosen landmark, returns boolean 
        public bool ReadCurrentLandmark(LandmarkEN en)
        {
            string sqlSelect = "select * from landmarks where id=@id";
            SqlConnection c = new SqlConnection(s);
            SqlDataReader dr;
            try
            {
                c.Open();
                SqlCommand com = new SqlCommand(sqlSelect, c);
                com.Parameters.AddWithValue("@id", en.Id);
                dr = com.ExecuteReader();
                if (!dr.Read())
                {
                    StatusMsg = $"Landmark with id {en.Id} does not exist.";
                    Console.WriteLine($"Operation failed. Error: {StatusMsg}");
                    dr.Close();
                    c.Close();
                    return false;
                }
                en.Name = dr["name"].ToString();
                en.Price = float.Parse(dr["pricerange"].ToString());
                en.Address = dr["adress"].ToString();
                en.Location_id = int.Parse(dr["location_id"].ToString());
                en.Type = dr["type"].ToString();
                en.WebsiteLink = dr["websiteLink"].ToString();
                dr.Close();

            }
            catch (Exception ex)
            {
                StatusMsg = ex.Message.ToString();
                Console.WriteLine($"Operation failed. Error: {StatusMsg}");
                c.Close();
                return false;
            }
            c.Close();
            return true;

        }

        //Method to check if landmark exists in DB
        public bool Exists(LandmarkEN en)
        {
            string sqlSelect = "select id from landmarks where id=@id";
            SqlConnection c = new SqlConnection(s);
            SqlDataReader dr;
            try
            {
                c.Open();
                SqlCommand com = new SqlCommand(sqlSelect, c);
                com.Parameters.AddWithValue("@id", en.Id);
                dr = com.ExecuteReader();
                if (!dr.Read())
                {
                    StatusMsg = $"Landmark with id {en.Id} does not exist.";
                    Console.WriteLine($"Operation failed. Error: {StatusMsg}");
                    dr.Close();
                    c.Close();
                    return false;
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                StatusMsg = ex.Message.ToString();
                Console.WriteLine($"Operation failed. Error: {StatusMsg}");
                c.Close();
                return false;
            }
            c.Close();
            return true;
        }
    }
}
