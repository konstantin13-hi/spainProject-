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
    /// <summary>
    /// The BookingCAD (Booking CRUD Access Data) class is responsible for performing database operations related to bookings.
    /// </summary>
    public class BookingCAD
    {
        private string _constring; //readonly
        /// <summary>
        /// Gets or sets the connection string for the database.
        /// </summary>
        public string Constring
        {
            get { return _constring; }
            set
            {
                _constring = value;
            }
        }
        /// <summary>
        /// Gets or sets the exit message after performing an operation.
        /// </summary>
        public string ExitMsg { get; set; }
        /// <summary>
        /// Initializes a new instance of the BookingCAD class and sets the connection string from the configuration file.
        /// </summary>
        public BookingCAD()
        {
            Constring = ConfigurationManager.ConnectionStrings["dbconnection"].ToString();
        }
        /// <summary>
        /// Creates a new booking in the database.
        /// The BookingEN object representing the booking to be created.
        /// </summary>
        /// <returns>True if the booking is created successfully, otherwise false.</returns>
        public bool CreateBooking(BookingEN en)
        {
            string query = "INSERT INTO Bookings (accomodation_id, bookingprice, checkin, checkout) VALUES (@accommodationId, @bookingPrice, @checkInDate, @checkOutDate); SELECT SCOPE_IDENTITY();";
            bool isCreated = false;

            using (SqlConnection connection = new SqlConnection(Constring))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@accommodationId", en.Accommodation.Id);
                cmd.Parameters.AddWithValue("@bookingPrice", en.BookingPrice);
                cmd.Parameters.AddWithValue("@checkInDate", en.CheckInDate);
                cmd.Parameters.AddWithValue("@checkOutDate", en.CheckOutDate);

                // Execute the query and retrieve the booking ID
                int bookingId = Convert.ToInt32(cmd.ExecuteScalar());

                if (bookingId > 0)
                {
                    isCreated = true;
                    en.BookingID = bookingId;
                }
            }

            return isCreated;
        }
        /// <summary>
        /// Reads a booking from the database using the specified booking ID and updates the BookingEN object with the retrieved data.
        /// The BookingEN object representing the booking to be read.
        /// </summary>
        /// <returns>True if the booking is found and read successfully, otherwise false.</returns>
        public bool ReadBooking(BookingEN en)
        {
            DataSet dbVirtual = null;
            SqlConnection connection = null;
                dbVirtual = new DataSet();
                using (connection = new SqlConnection(Constring))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("SELECT * FROM Bookings WHERE id = @id", connection);
                    command.Parameters.AddWithValue("@id", en.BookingID);

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    dataAdapter.Fill(dbVirtual, "bookings");
                }

                if (dbVirtual.Tables["bookings"].Rows.Count > 0)
                {
                    DataRow bookingRow = dbVirtual.Tables["bookings"].Rows[0];
                    en.AccommodationID = (int)bookingRow["accomodation_id"];
                    en.Accommodation = (new AccomodationEN(en.AccommodationID));
                    en.Accommodation.ReadAccommodationBool();
                    en.BookingPrice = (decimal)bookingRow["bookingprice"];
                    en.CheckInDate = (DateTime)bookingRow["checkin"];
                    en.CheckOutDate = (DateTime)bookingRow["checkout"];
                }
                else
                {
                    ExitMsg = "Booking not found.";
                    return false;
                }

                return true;

            return false;
        }
        /// <summary>
        /// Updates an existing booking in the database with the data from the provided BookingEN object.
        /// The BookingEN object representing the booking to be updated.
        /// </summary>
        /// <returns>True if the booking is updated successfully, otherwise false.</returns>
        public bool UpdateBooking(BookingEN en)
        {
            DataSet dbVirtual = null;
            SqlConnection connection = null;
            try
            {
                dbVirtual = new DataSet();
                using (connection = new SqlConnection(Constring))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("SELECT * FROM Bookings WHERE id = @id", connection);
                    command.Parameters.AddWithValue("@id", en.BookingID);

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    dataAdapter.Fill(dbVirtual, "bookings");
                

                if (dbVirtual.Tables["bookings"].Rows.Count > 0)
                {
                    DataRow bookingRow = dbVirtual.Tables["bookings"].Rows[0];
                    bookingRow["accomodation_id"]= en.Accommodation.Id;
                    bookingRow["bookingprice"] = en.BookingPrice;
                    bookingRow["checkin"] = en.CheckInDate;
                    bookingRow["checkout"] = en.CheckOutDate;
                }
                else
                {
                    ExitMsg = "Booking not found.";
                    return false;
                }
                    SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                    dataAdapter.UpdateCommand = commandBuilder.GetUpdateCommand();

                    int rowsAffected = dataAdapter.Update(dbVirtual, "Bookings");
                }

                return true;
            }
            catch (SqlException ex)
            {
                ExitMsg = "SQL Exception caught: " + ex.Message;
            }
            catch (Exception ex)
            {
                ExitMsg = "Exception caught: " + ex.Message;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }

            return false;
        }
        /// <summary>
        /// Deletes a booking from the database using the specified booking ID.
        /// </summary>
        /// <param name="en">The BookingEN object representing the booking to be deleted.</param>
        /// <returns>True if the booking is deleted successfully, otherwise false.</returns>
        public bool DeleteBooking(BookingEN en)
        {
            using (SqlConnection connection = new SqlConnection(Constring))
            {
                
                    connection.Open();
                    SqlCommand command = new SqlCommand("SELECT * FROM Bookings WHERE id = @id", connection);
                    command.Parameters.AddWithValue("@id", en.BookingID);

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    SqlCommandBuilder builder = new SqlCommandBuilder(dataAdapter);

                    DataSet dbVirtual = new DataSet();
                    dataAdapter.Fill(dbVirtual, "bookings");

                    if (dbVirtual.Tables["bookings"].Rows.Count > 0)
                    {
                        foreach (DataRow row in dbVirtual.Tables["bookings"].Rows)
                        {
                            row.Delete();
                        }

                        // Update the database with the changes
                        dataAdapter.Update(dbVirtual, "bookings");
                    }
                    else
                    {
                        ExitMsg = "Booking not found.";
                        return false;
                    }
            }

            return true;
        }
    }
}
