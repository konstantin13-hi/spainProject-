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
    public class ReviewCAD
    {

        string s;

        //ReviewCAD constructor, include construction string
        public ReviewCAD()
        {
            s = ConfigurationManager.ConnectionStrings["dbconnection"].ToString();
        }

        //method to create new review
        //Takes ReviewEN object as input and add values to database
        public bool createReview(ReviewEN rewEn)
        {
            bool inserted;
            DataSet dbvirtual = new DataSet();
            SqlConnection c = new SqlConnection(s);
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from reviews", c);
                da.Fill(dbvirtual, "reviews");
                DataTable t = new DataTable();
                t = dbvirtual.Tables["reviews"];
                DataRow newRev = t.NewRow();
                newRev["accomodation_id"] = rewEn.Accommodation_id;
                newRev["user_id"] = rewEn.User_id;
                newRev["pointsAvg"] = rewEn.AveragePoints;
                newRev["pointsOverall"] = rewEn.OverallPoints;
                newRev["pointsArea"] = rewEn.AreaPoints;
                newRev["pointsTidiness"] = rewEn.TidinessPoints;
                newRev["reviewText"] = rewEn.Review;
                t.Rows.Add(newRev);

                SqlCommandBuilder cbuilder = new SqlCommandBuilder(da);
                da.Update(dbvirtual, "reviews");
                rewEn.StatusMsg = "Review added successfully!";
                inserted = true;

            }
            catch (Exception ex) {  inserted = false; } 
            finally { c.Close(); }
            
            return inserted;
        }

        //reads average grades of one accommodation
        //Returns dataset where values are
        public DataSet readAvgReview(ReviewEN rewEn)
        {
            DataSet ds = new DataSet();
            SqlConnection c = new SqlConnection(s);
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("Select round(avg(pointsAvg),1), round(avg(pointsOverall),1), round(avg(pointsArea),1), round(avg(pointsTidiness),1)" +
                                                        "from reviews where accomodation_id ='" + rewEn.Accommodation_id + "' group by accomodation_id", c);

                da.Fill(ds, "Averages");
            }
            catch (Exception ex) { }
            finally { c.Close(); }
            
            return ds;
        }

        //Reads the name of that accommodation user was browsing when clicked Give Review -button. 
        //Checks that user haven't given review to this accommodation yet
        //If user haven't given review to this acc, returns accommodation name as a string (it will be displayed at the page)
        public string readAccommodation(ReviewEN rewEn)
        {
            string name;
            SqlConnection c = new SqlConnection(s);

            c.Open();
            SqlCommand query = new SqlCommand("Select * from accomodations where id ='"+ rewEn.Accommodation_id +"'" +
                                                "and '"+rewEn.Accommodation_id+"' not in (select accomodation_id from reviews)", c);
            SqlDataReader dr = query.ExecuteReader();
            if (dr.Read())
            {
                name = dr["name"].ToString();
            } else
            {
                name = "";
            }
            dr.Close();
            c.Close();

            return name;
        }

        //Reads all accommodations which user has booked at some point but haven't reviewed yet
        //returns dataset of these accommodations names and id:s
        public DataSet readNonReviewed(ReviewEN rewEn)
        {
            //TODO tsekkaa, miten kirjautuneen käyttäjän id:tä voi käyttää kyselyissä
            string query = "SELECT distinct a.name, a.id FROM accomodations a " +
                            "JOIN bookings b ON a.id = b.accomodation_id " +
                            "JOIN history h ON b.id = h.booking_id " +
                            "JOIN reviews r ON h.user_id = r.user_id " +
                            "WHERE r.user_id = '" + rewEn.User_id + "' " +
                                "AND a.id not in (Select distinct accomodation_id from reviews where user_id = '"+ rewEn.User_id +"') ";
            DataSet ds = new DataSet();
            SqlConnection c = new SqlConnection(s);
            SqlDataAdapter da = new SqlDataAdapter(query, c);
            da.Fill(ds, "Accommodation");

            return ds;
        }

        //Using read, checks if current user ever booked this accommodation (Accommodation-page uses this)
        //returns true if user has booked this at some point, and false if hasn't.
        public bool readAccHistory(UserEN user, int accId)
        {
            bool found = false;
            string query = "Select distinct accomodation_id from bookings " +
                            "join history on bookings.id = history.booking_id " +
                            "where user_id = '" + user.Id + "'" +
                            "and accomodation_id = '"+ accId +"'";
            DataSet ds = new DataSet();
            SqlConnection c = new SqlConnection(s);
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(query, c);
                da.Fill(ds, "history");
                if (ds.Tables["history"].Rows.Count == 0)
                {
                    found = false;
                }
                else
                {
                    found = true;
                }
            } 
            catch (Exception ex) { found = false; }
            finally { c.Close(); }

            return found;
        }

        //Reads all reviews and accommodation names, returns these as dataset
        public DataSet readReview(ReviewEN rev)
        {
            string query = "Select reviews.id, accomodation_id, accomodations.name, pointsAvg, pointsOverall, pointsArea, pointsTidiness, reviewText " +
                            "from reviews inner join accomodations on accomodations.id = reviews.accomodation_id " +
                            "where user_id = '" + rev.User_id + "'";
            
            DataSet ds = new DataSet();
            SqlConnection c = new SqlConnection(s);
            SqlDataAdapter da = new SqlDataAdapter(query, c);
            da.Fill(ds, "Reviews");

            return ds;
        }

        //Updates one review using connected environment
        public bool updateReview(ReviewEN rewEn)
        {
            bool updated = false;

            SqlConnection c = new SqlConnection(s);
            string sqlUpdate = "update reviews set pointsAvg=@avg, pointsOverall=@overall, pointsArea=@area, pointsTidiness=@tidiness, reviewText=@review where id=@id";

            try
            {
                c.Open();
                SqlCommand com = new SqlCommand(sqlUpdate, c);
                com.Parameters.AddWithValue("@id", rewEn.Id);
                com.Parameters.AddWithValue("@overall", rewEn.OverallPoints);
                com.Parameters.AddWithValue("@area", rewEn.AreaPoints);
                com.Parameters.AddWithValue("@tidiness", rewEn.TidinessPoints);
                com.Parameters.AddWithValue("@avg", rewEn.AveragePoints);
                com.Parameters.AddWithValue("@review", rewEn.Review);
                com.ExecuteNonQuery();
                c.Close();
                rewEn.StatusMsg = "Review successfully updated";
                updated = true;
            }

            catch (Exception ex) { rewEn.StatusMsg = "Review wasn't updated"; }

            return updated;

        }

        //Deletes one review using connected environment
        public bool deleteReview(ReviewEN rewEn)
        {
            bool deleted = false;
            SqlConnection c = new SqlConnection(s);
            try
            {
                c.Open();
                SqlCommand com = new SqlCommand("Delete from reviews where id=@id", c);
                com.Parameters.AddWithValue("@id", rewEn.Id);
                com.ExecuteNonQuery();
                deleted = true;
            }
            catch (Exception ex) { }
            finally { c.Close(); }
            return deleted;
        }
        
    }
}