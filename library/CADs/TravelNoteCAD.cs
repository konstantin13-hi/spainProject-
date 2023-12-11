using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace library
{
    public class TravelNoteCAD
    {
        private string _constring;
        private string constring // Connectionstring to the database 
        {
            get { return _constring; }
            set { _constring = value; }
        }

        public string message; // Message that can be passed through to the interface

        public TravelNoteCAD()
        {
            constring = ConfigurationManager.ConnectionStrings["dbconnection"].ToString();
        }

        // Creates a new user in the DB with the TravelNote data represented by the parameter en
        public bool createTravelNote(TravelNoteEN en)
        {
            SqlConnection con = new SqlConnection(constring);
            try
            {
                con.Open();

                string s = "INSERT INTO travel_notes (title, user_id, accomodation_id, booking_id, noteLocation, noteAccomodation, noteGeneral) OUTPUT INSERTED.ID VALUES (@title, @user_id, @accomodation_id, @booking_id, @noteLocation, @noteAccomodation, @noteGeneral)";

                SqlCommand cmd = new SqlCommand(s, con);
                cmd.Parameters.AddWithValue("@title", en.Title);
                cmd.Parameters.AddWithValue("@user_id", en.User.Id);
                cmd.Parameters.AddWithValue("@accomodation_id", en.Accomodation.Id);
                cmd.Parameters.AddWithValue("@booking_id", en.Booking.BookingID);
                cmd.Parameters.AddWithValue("@noteLocation", en.NoteLocation);
                cmd.Parameters.AddWithValue("@noteAccomodation", en.NoteAccomodation);
                cmd.Parameters.AddWithValue("@noteGeneral", en.NoteGeneral);
                int insertedId = (int)cmd.ExecuteScalar();

                en.Id = insertedId;
                message = "New TravelNote successfully created.";
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                con.Close();
                this.message = "TravelNote could not be created.";
                return false;
            }
        }

        //Returns only the indecate TravelNote read from DB
        public bool readTravelNote(TravelNoteEN en)
        {
            SqlConnection con = new SqlConnection(constring);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from travel_notes where id = @TravelNoteID", con);
                cmd.Parameters.AddWithValue("@TravelNoteID", en.Id);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        en.Title = (string)reader["title"];
                        en.CreationDate = (DateTime)reader["created_at"];
                        en.ModificationDate = (DateTime)reader["updated_at"];
                        int accomodation = (int)reader["accomodation_id"];
                        int booking = (int)reader["booking_id"];

                        object noteGeneralValue = reader.GetValue(reader.GetOrdinal("noteGeneral"));
                        string noteGeneral = noteGeneralValue == DBNull.Value ? "" : (string)noteGeneralValue;
                        object noteAccomodationValue = reader.GetValue(reader.GetOrdinal("noteAccomodation"));
                        string noteAccomodation = noteAccomodationValue == DBNull.Value ? "" : (string)noteAccomodationValue;
                        object noteLocationValue = reader.GetValue(reader.GetOrdinal("noteLocation"));
                        string noteLocation = noteLocationValue == DBNull.Value ? "" : (string)noteLocationValue;
                        en.NoteGeneral = noteGeneral;
                        en.NoteLocation = noteAccomodation;
                        en.NoteAccomodation = noteLocation;
                        AccomodationEN accomodationEN = new AccomodationEN();
                        accomodationEN.Id = accomodation;
                        accomodationEN.ReadAccommodationBool();
                        en.Accomodation = accomodationEN;

                        LocationEN locationEN = new LocationEN();
                        locationEN.Id = en.Accomodation.locationEN.Id;
                        locationEN.ReadLocation();
                        en.TravelLocation = locationEN;

                        BookingEN bookingEN = new BookingEN();
                        bookingEN.BookingID = booking;
                        bookingEN.ReadBooking();
                        en.Booking = bookingEN;

                        message = "TravelNote loaded successfully.";
                        con.Close();
                        return true;
                    }
                    else
                    {
                        con.Close();
                        message = "No TravelNote with the given ID exists.";
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                message = "Error occured: TravelNote could not be loaded.";
                con.Close();
                return false;
            }
        }

        // Returns all TravelNotes of a User by UserID
        public List<TravelNoteEN> readAllTravelNotes(int userId)
        {

            List<TravelNoteEN> travelNotes = new List<TravelNoteEN>();

            using (SqlConnection con = new SqlConnection(constring))
            {
                try
                {
                    con.Open();
                    string query = "SELECT * FROM travel_notes where user_id=@userID";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@userID", userId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        TravelNoteEN en = new TravelNoteEN();
                        int id = (int)reader["id"];
                        en.Id = id;
                        en.readTravelNote();                        

                        travelNotes.Add(en);
                    }
                    message = "TravelNotes loaded successfully.";
                    reader.Close();
                }
                catch (Exception ex)
                {
                    message = "Error occured: TravelNotes could not be loaded.";
                }
                finally
                {
                    con.Close();
                }
            }

            return travelNotes;
        }

        // Updates the data of a TravelNote in the DB with hte data of the user represented by the prameter en.
        public bool updateTravelNote(TravelNoteEN en)
        {
            SqlConnection con = new SqlConnection(constring);
            try
            {
                con.Open();
                SqlCommand update = new SqlCommand("update travel_notes set title = @title, noteGeneral = @NoteGeneral, noteLocation = @NoteLocation, noteAccomodation = @NoteAccomodation, updated_at = @modificationDate", con);
                update.Parameters.AddWithValue("@title", en.Title);
                update.Parameters.AddWithValue("@NoteGeneral", en.NoteGeneral);
                update.Parameters.AddWithValue("@NoteLocation", en.NoteLocation);
                update.Parameters.AddWithValue("@NoteAccomodation", en.NoteAccomodation);
                update.Parameters.AddWithValue("@modificationDate", DateTime.Now);

                if (update.ExecuteNonQuery() == 0)
                {
                    message = "TravelNote could not be updated.";
                    return false;
                }
                else
                {
                    message = "TravelNote updated successfully.";
                    return true;
                }
            }
            catch (Exception ex)
            {
                message = "Error occured: TravelNote could not be updated.";
                Console.WriteLine("User operation has failed. Error: {0}", ex.Message);
                return false;
            }
            finally
            {
                con.Close();
            }
        }

        // Deletes the TravelNote represented by en in the DB
        public bool deleteTravelNote(TravelNoteEN en)
        {
            SqlConnection con = new SqlConnection(constring);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("delete from travel_notes where id = @id",con);
                cmd.Parameters.AddWithValue("@id", en.Id);
                int affected = cmd.ExecuteNonQuery();
                if (affected > 0)
                {
                    message = "TravelNote deleted successfully.";
                    con.Close();
                    return true;
                }
                else
                {
                    message = "TravelNote does not exist and can therefore not be deleted.";
                    con.Close();
                    return false;
                }
            }
            catch (Exception ex)
            {
                message = "Error occured: TravelNote could not be deleted.";
                con.Close();
                return false;
            }
        }
    }
}
