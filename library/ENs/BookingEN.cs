using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace library
{
    /// <summary>
    /// The BookingEN (Booking Entity) class represents a booking in a library system.
    /// </summary>
    public class BookingEN
    {
        private BookingCAD cad = new BookingCAD();
        /// <summary>
        /// Gets or sets the AccomodationEN object associated with the booking.
        /// </summary>
        public AccomodationEN Accommodation { get; set; }
        /// <summary>
        /// Gets or sets the ID of the accommodation associated with the booking.
        /// </summary>
        public int AccommodationID { get; set; }
        /// <summary>
        /// Gets or sets the ID of the booking.
        /// </summary>
        public int BookingID { get; set; }
        /// <summary>
        /// Gets or sets the price of the booking.
        /// </summary>
        public decimal BookingPrice { get; set; }
        /// <summary>
        /// Gets or sets the check-in date of the booking.
        /// </summary>
        public DateTime CheckInDate { get; set; }
        /// <summary>
        /// Gets or sets the check-out date of the booking.
        /// </summary>
        public DateTime CheckOutDate { get; set; }
        /// <summary>
        /// Gets or sets the exit message after performing an operation.
        /// </summary>
        public string ExitMsg { get; set; }
        /// <summary>
        /// Initializes a new instance of the BookingEN class.
        /// </summary>
        public BookingEN(AccomodationEN accommodation,decimal booking_price,DateTime checkInDate,DateTime checkOutDate) {
            Accommodation = accommodation;
            BookingPrice = booking_price;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;    
        }
        public BookingEN() { }

        /// <summary>
        /// Creates a new booking.
        /// </summary>
        /// <returns>True if the booking is created successfully, otherwise false.</returns>
        public bool CreateBooking()
        {
            bool status = cad.CreateBooking(this);
            ExitMsg = cad.ExitMsg;
            return status;
        }
        /// <summary>
        /// Updates an existing booking.
        /// </summary>
        /// <returns>True if the booking is updated successfully, otherwise false.</returns>
        public bool UpdateBooking()
        {
            bool status = cad.UpdateBooking(this);
            ExitMsg = cad.ExitMsg;
            return status;
        }
        /// <summary>
        /// Reads the booking using the specified booking ID.
        /// </summary>
        /// <returns>True if the booking is found and read successfully, otherwise false.</returns>
        public bool ReadBooking()
        {
            bool status = cad.ReadBooking(this);
            ExitMsg = cad.ExitMsg;
            return status;
        }
        /// <summary>
        /// Deletes an existing booking.
        /// </summary>
        /// <returns>True if the booking is deleted successfully, otherwise false.</returns>
        public bool DeleteBooking()
        {
            bool status = cad.DeleteBooking(this);
            ExitMsg = cad.ExitMsg;
            return status;
        }
        /// <summary>
        /// Code for testing.
        /// Returns a string representation of the BookingEN object.
        /// </summary>
        /// <returns>A string representation of the BookingEN object.</returns>
        public string ToString()
        {
            string str = "";
            str += ($"BookingID: {this.BookingID}\n");
            if (this.Accommodation != null)
            {
                str += ($"AccommodationID: {this.Accommodation.Id}\n");
                str += ($"Check-in Date: {this.CheckInDate}\n");
                str += ($"Check-out Date: {this.CheckOutDate}\n");
                str += ($"Price: {this.BookingPrice}\n");
            }
            else
            {
                str += ($"AccommodationID: null\n");
            }
            return str;
        }
    }
}
