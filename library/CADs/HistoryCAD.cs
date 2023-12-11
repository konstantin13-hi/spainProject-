using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace library
{
    /// <summary>
    /// The HistoryCAD (History Control and Data Access) class provides functionality for creating, reading, updating, and deleting history records in a library system.
    /// </summary>
    public class HistoryCAD
    {
        private static string _constring; //readonly
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
        private BookingEN b;
        private UserEN u;
        /// <summary>
        /// Gets or sets the exit message after performing an operation.
        /// </summary>
        public string ExitMsg { get; set; }
        /// <summary>
        /// Initializes a new instance of the HistoryCAD class with the default connection string from the configuration file.
        /// </summary>
        public HistoryCAD()
        {
            Constring = ConfigurationManager.ConnectionStrings["dbconnection"].ToString();
        }

        /// <summary>
        /// Creates a new history record.
        /// The HistoryEN object containing the data for the new history record.
        /// </summary>
        /// <returns>True if the history record is created successfully, otherwise false.</returns>
        public bool CreateHistory(HistoryEN en)
        {
            string query = "SELECT * FROM history WHERE 1=0";
            bool isCreated = false;
            SqlDataAdapter dataAdapter = null;
            DataSet dbVirtual = null;

            using (SqlConnection connection = new SqlConnection(Constring))
            {

                connection.Open();

                SqlCommand cmd = new SqlCommand(query, connection);
                dataAdapter = new SqlDataAdapter(cmd);

                dbVirtual = new DataSet();
                dataAdapter.Fill(dbVirtual, "history");

                DataTable table = dbVirtual.Tables["history"];
                DataRow row = table.NewRow();

                row["booking_id"] = en.Booking.BookingID;
                row["user_id"] = en.User.Id;

                table.Rows.Add(row);

                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                dataAdapter.InsertCommand = commandBuilder.GetInsertCommand();
                int rowsAffected = dataAdapter.Update(dbVirtual, "history");

                if (rowsAffected > 0)
                {
                    isCreated = true;
                }
            }

            return isCreated;
        }
        /// <summary>
        /// Reads a history record using the specified HistoryEN object.
        /// The HistoryEN object containing the history ID to read.
        /// </summary>
        /// <returns>True if the history record is found and read successfully, otherwise false.</returns>
        public bool ReadHistory(HistoryEN en)
        {
            DataSet dbVirtual = null;
            SqlConnection connection = null;
                dbVirtual = new DataSet();
                using (connection = new SqlConnection(Constring))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("SELECT * FROM history WHERE id = @id", connection);
                    command.Parameters.AddWithValue("@id", en.HistoryID);

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    dataAdapter.Fill(dbVirtual, "history");
                }

                if (dbVirtual.Tables["history"].Rows.Count > 0)
                {
                    DataRow row = dbVirtual.Tables["history"].Rows[0];
                    BookingEN booking = new BookingEN();
                    booking.BookingID = (int)row["booking_id"];
                    (booking).ReadBooking();
                    en.Booking = booking;

                    UserEN user = new UserEN();
                    user.Id = (int)row["user_id"];
                    user.ReadUser();
                    en.User = user;
                }
                else
                {
                    ExitMsg = "Booking not found.";
                    return false;
                }

            return true;
        }
        /// <summary>
        /// Reads all history records.
        /// </summary>
        /// <returns>A list of HistoryEN objects representing all history records.</returns>
        public List<HistoryEN> ReadAllHistory()
        {
            List<HistoryEN> historyList = new List<HistoryEN>();
            DataSet dataSet = null;
            SqlConnection connection = null;
            using (connection = new SqlConnection(Constring))
            {
                dataSet = new DataSet();
                connection = new SqlConnection(Constring);
                SqlCommand command = new SqlCommand("SELECT * FROM history", connection);

                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                dataAdapter.Fill(dataSet, "history");

                DataTable table = dataSet.Tables["history"];
                foreach (DataRow row in table.Rows)
                {
                    HistoryEN history = new HistoryEN();
                    BookingEN booking = new BookingEN();
                    booking.BookingID = (int)row["booking_id"];
                    (booking).ReadBooking();
                    history.Booking = booking;

                    UserEN user = new UserEN();
                    user.Id = (int)row["user_id"];
                    user.ReadUser();
                    history.User = user;

                    historyList.Add(history);
                }

                ExitMsg = "";
            }
            return historyList;
        }
        /// <summary>
        /// Reads all history records associated with a specific user ID.
        /// The HistoryEN object containing the user ID to retrieve history records for.
        /// </summary>
        /// <returns>A list of HistoryEN objects representing the history records associated with the specified user ID.</returns>
        public List<HistoryEN> ReadHistoryByUserID(HistoryEN en)
        {
            DataSet dataSet = null;
            SqlConnection connection = null;
            List<HistoryEN> historyList = null;
            dataSet = new DataSet();
                historyList = new List<HistoryEN>();
                connection = new SqlConnection(Constring);
                SqlCommand command = new SqlCommand("SELECT * FROM History WHERE user_id = @user_id", connection);
                command.Parameters.AddWithValue("@user_id", en.User.Id);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                dataAdapter.Fill(dataSet, "history");

                DataTable table = dataSet.Tables["history"];
                foreach (DataRow row in table.Rows)
                {
                    HistoryEN history = new HistoryEN();
                    BookingEN booking = new BookingEN();
                    booking.BookingID = (int)row["booking_id"];
                    history.BookingID = (int)row["booking_id"];
                    (booking).ReadBooking();
                    history.Booking = booking;

                    UserEN user = new UserEN();
                    history.UserID = (int)row["user_id"];
                    user.Id = (int)row["user_id"];
                    user.ReadUser();
                    history.User = user;

                    historyList.Add(history);
                }

            ExitMsg = "";

            return historyList;
        }

        public List<BookingEN> getHistoryBookingsById (int userID)
        {
            HistoryEN en = new HistoryEN();
            en.User = new UserEN();
            en.User.Id = userID;
            List<HistoryEN> historyList = ReadHistoryByUserID(en);
            List<BookingEN> bookings = new List<BookingEN>();
            foreach (HistoryEN h in historyList)
            {
                bookings.Add(h.Booking);
            }
            return bookings;
        }

        public static List<HistoryEN> ReadHistoryByUserID(int user_id)
        {
            DataSet dataSet = null;
            SqlConnection connection = null;
            List<HistoryEN> historyList = null;
            dataSet = new DataSet();
            historyList = new List<HistoryEN>();
            connection = new SqlConnection(_constring);
            SqlCommand command = new SqlCommand("SELECT * FROM History WHERE user_id = @user_id", connection);
            command.Parameters.AddWithValue("@user_id", user_id);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            dataAdapter.Fill(dataSet, "history");

            DataTable table = dataSet.Tables["history"];
            foreach (DataRow row in table.Rows)
            {
                HistoryEN history = new HistoryEN();
                BookingEN booking = new BookingEN();
                booking.BookingID = (int)row["booking_id"];
                history.BookingID = (int)row["booking_id"];
                (booking).ReadBooking();
                history.Booking = booking;
                

                UserEN user = new UserEN();
                user.Id = (int)row["user_id"];
                user.ReadUser();
                history.User = user;

                historyList.Add(history);
            }

            /*ExitMsg = "";*/

            return historyList;
        }
        /// <summary>
        /// Updates an existing history record.
        /// The HistoryEN object containing the updated data for the history record.
        /// </summary>
        /// <returns>True if the history record is updated successfully, otherwise false.</returns>
        public bool UpdateHistory(HistoryEN en)
        {
            DataSet dbVirtual = new DataSet();
            SqlConnection connection = null;

                connection = new SqlConnection(Constring);
                SqlCommand command = new SqlCommand("SELECT * FROM History WHERE id = @id", connection);
                command.Parameters.AddWithValue("@id", en.HistoryID);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);

                dataAdapter.Fill(dbVirtual, "History");

                DataTable historyTable = dbVirtual.Tables["History"];

                DataRow[] foundRows = historyTable.Select($"id = {en.HistoryID}");
                foreach (DataRow row in foundRows)
                {
                    row["booking_id"] = en.Booking.BookingID;
                    row["user_id"] = en.User.Id;
                }

                if (dataAdapter.Update(dbVirtual, "History") < 1)
                {
                    return false;
                }
            dataAdapter.UpdateCommand = commandBuilder.GetUpdateCommand();

            int rowsAffected = dataAdapter.Update(dbVirtual, "history");
            ExitMsg = "";

            return true;
        }
        /// <summary>
        /// Deletes an existing history record.
        /// The HistoryEN object containing the history ID to delete.
        /// </summary>
        /// <returns>True if the history record is deleted successfully, otherwise false.</returns>
        public bool DeleteHistory(HistoryEN en)
        {
            using (SqlConnection connection = new SqlConnection(Constring))
            {

                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM history WHERE id = @id", connection);
                command.Parameters.AddWithValue("@id", en.HistoryID);

                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                DataSet dbVirtual = new DataSet();
                dataAdapter.Fill(dbVirtual, "history");

                if (dbVirtual.Tables["history"].Rows.Count > 0)
                {
                    foreach (DataRow row in dbVirtual.Tables["history"].Rows)
                    {
                        row.Delete();
                    }

                    
                }
                else
                {
                    ExitMsg = "History not found.";
                    return false;
                }
                dataAdapter.UpdateCommand = commandBuilder.GetUpdateCommand();
                int rowsAffected = dataAdapter.Update(dbVirtual, "history");
            }

            return true;
        }
    }
}
