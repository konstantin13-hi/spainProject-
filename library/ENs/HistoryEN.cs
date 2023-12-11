using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace library
{
    /// <summary>
    /// The HistoryEN (History Entity) class represents a history record in a library system.
    /// </summary>
    public class HistoryEN
    {
        private HistoryCAD cad = new HistoryCAD();
        /// <summary>
        /// Gets or sets the ID of the history record.
        /// </summary>
        public int HistoryID { get; set; }
        /// <summary>
        /// Gets or sets the ID of the user associated with the history record.
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// Gets or sets the ID of the booking associated with the history record.
        /// </summary>
        public int BookingID { get; set; }
        /// <summary>
        /// Gets or sets the UserEN object associated with the history record.
        /// </summary>
        public UserEN User { get; set; }
        /// <summary>
        /// Gets or sets the BookingEN object associated with the history record.
        /// </summary>
        public BookingEN Booking { get; set; }
        /// <summary>
        /// Gets or sets the exit message after performing an operation.
        /// </summary>
        public string ExitMsg { get; set; }
        /*public HistoryEN(int user_id,int booking_id) {
            UserID = user_id;
            BookingID = booking_id;
        }*/
        /// <summary>
        /// Initializes a new instance of the HistoryEN class.
        /// </summary>
        public HistoryEN() { }
        public HistoryEN(UserEN u, BookingEN b)
        {
            User = u;
            Booking = b;
        }
        /// <summary>
        /// Creates a new history record.
        /// </summary>
        /// <returns>True if the history record is created successfully, otherwise false.</returns>
        public bool CreateHistory()
        {
            bool status = cad.CreateHistory(this);
            ExitMsg = cad.ExitMsg;
            return status;
        }
        /// <summary>
        /// Updates an existing history record.
        /// </summary>
        /// <returns>True if the history record is updated successfully, otherwise false.</returns>
        public bool UpdateHistory()
        {
            bool status = cad.UpdateHistory(this);
            ExitMsg = cad.ExitMsg;
            return status;
        }
        /// <summary>
        /// Reads the history record using the specified history ID.
        /// </summary>
        /// <returns>True if the history record is found and read successfully, otherwise false.</returns>
        public bool ReadHistory()
        {
            bool status = cad.ReadHistory(this);
            ExitMsg = cad.ExitMsg;
            return status;
        }
        /// <summary>
        /// Reads all history records.
        /// </summary>
        /// <returns>A list of HistoryEN objects representing all history records.</returns>
        public static List<HistoryEN> ReadAllHistory() //TODO
        {
            HistoryCAD cad = new HistoryCAD();
            List<HistoryEN> historyList = cad.ReadAllHistory();
            string ExitMsg = cad.ExitMsg;
            return historyList;
        }
        /// <summary>
        /// Reads all history records associated with the specified user ID.
        /// </summary>
        /// <returns>A list of HistoryEN objects representing the history records associated with the specified user ID.</returns>
        public List<HistoryEN> ReadHistoryByUserID() //Set history.user_id = userEN.Id; b4 use
        {
            List<HistoryEN> historyList = cad.ReadHistoryByUserID(this);
            string ExitMsg = cad.ExitMsg;
            return historyList;
        }
        /// <summary>
        /// Deletes an existing history record.
        /// </summary>
        /// <returns>True if the history record is deleted successfully, otherwise false.</returns>
        public bool DeleteHistory()
        {
            bool status = cad.DeleteHistory(this);
            ExitMsg = cad.ExitMsg;
            return status;
        }
    }
}
