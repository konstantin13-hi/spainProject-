using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using library;

namespace TravelAgencyWeb.Pages
{
    public partial class UserBooking : System.Web.UI.Page
    {
        UserEN user = new UserEN();
        AccomodationEN accommodation = new AccomodationEN();
        bool isError = true;
        int numOfDays = 0;
        decimal price = 0;
        DateTime checkin;
        DateTime checkout;
        DateTime today = DateTime.Now.Date;
        protected void Page_Load(object sender, EventArgs e)
        {
            user = (UserEN)Session["CurrentUser"];
            
            if (user == null)
            {
                Response.Redirect("~/Pages/UserLogin.aspx");
            }
            else if (user.IsAdmin)
            {
                Response.Redirect("~/Admin Pages/AdminUser.aspx");
            }
            else
            {

            }
            int accommodation_id;
            if (Int32.TryParse(Request.QueryString["par1"], out accommodation_id))
            {
                accommodation.Id = accommodation_id;
                accommodation.ReadAccommodationBool();
                
                label_Location.Text = accommodation.Address;
                label_Accommodation.Text = accommodation.Name;
            }
            else
            {
                Response.Redirect("~/Pages/Home.aspx");
            }
        }

        // Check if check-in and check-out dates are valid
        //Helper method
        protected void OnClick_IsValid(object sender, EventArgs e)
        {
            isValid();
        }
        private bool isValid()
        {
            checkin = calendar_CheckIn.SelectedDate;
            checkout = calendar_CheckOut.SelectedDate;
            today = DateTime.Now.Date;
            if (checkin < today)
            {
                label_DateError.Text = "Invalid check-in date. Please choose a date that is today or later.";
            }
            else if (checkin == new DateTime(1, 1, 1, 0, 0, 0) || checkout == new DateTime(1, 1, 1, 0, 0, 0))
            {
                label_DateError.Text = "Please enter both your check-in and check-out date";
            }
            else if (checkin >= checkout)
            {
                label_DateError.Text = "Your check-out date should be later than your check-in date!";
            }
            else if (accommodation.NumberOfRooms <= 0)
            {
                label_DateError.Text = "No rooms are available for your selected period";
            }
            else
            {
                label_DateError.Text = "";
                label_pay_button_error.Text = "";
                numOfDays = DateTime.Compare(checkout,checkin);
                TimeSpan duration = checkout - checkin;
                numOfDays = duration.Days;
                label_NumberOfDays.Text = numOfDays.ToString();
                price = accommodation.Price * numOfDays;
                label_Price.Text = (price).ToString();
                return true; // Dates == valid
            }
            numOfDays = 0;
            price = 0;
            label_NumberOfDays.Text = numOfDays.ToString();
            label_Price.Text = price.ToString();
            return false; // Dates =/= valid
        }

        protected void Pay_Click(object sender, EventArgs e)
        {
            // Display a confirmation dialog box
            if (!isValid())
            {
                label_pay_button_error.Text = "You need to select a valid period!";
            }
            else
            {
                label_pay_button_error.Text = "";
                /*string confirmMessage = "Are you sure you want to proceed with the booking?";
                string script = $"if (confirm('{confirmMessage}')) {{ storeBookingInfo(); }}";
                ScriptManager.RegisterStartupScript(this, GetType(), "Confirmation", script, true);
*/
                // Store the booking information in the Booking & History Table
                BookingEN newBooking = new BookingEN();
                newBooking.Accommodation = accommodation;
                newBooking.CheckInDate = checkin;
                newBooking.CheckOutDate = checkout;
                newBooking.BookingPrice = price;
                
                if (!newBooking.CreateBooking())
                {
                    return;
                }

                HistoryEN newHistory = new HistoryEN();
                UserEN newUser = new UserEN();
                user.ReadUser();
                user.GetIdFromEmail();

                newHistory.Booking = newBooking;
                newHistory.User = user;

                if (!newHistory.CreateHistory())
                {
                    return;
                }

                Response.Redirect($"~/Pages/UserBookingHistory.aspx?booking_id={newBooking.BookingID}");
            }
        }
/*
        private void storeBookingInfo()
        {
            // Store the booking information in the Booking & History Table
            BookingEN newBooking = new BookingEN();
            newBooking.Accommodation = accommodation;
            newBooking.CheckInDate = checkin;
            newBooking.CheckOutDate = checkout;
            newBooking.BookingPrice = price;

            if (!newBooking.CreateBooking())
            {
                return;
            }

            HistoryEN newHistory = new HistoryEN();
            UserEN newUser = new UserEN();
            user.ReadUser();
            user.GetIdFromEmail();

            newHistory.Booking = newBooking;
            newHistory.User = user;

            if (!newHistory.CreateHistory())
            {

                return;
            }

            Response.Redirect("~/Pages/UserBookingHistory.aspx?par1=");
        }*/
    }

}