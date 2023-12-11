using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using library;

namespace TravelAgencyWeb.Pages
{
    /// To display the booking history of a user
    public partial class UserBookingHistory : System.Web.UI.Page
    {
        UserEN user = new UserEN();
        //Handles page loading
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
                user.ReadUser();
                user.GetIdFromEmail();
                HistoryEN userHistory = new HistoryEN();
                userHistory.UserID = user.Id;
                List<HistoryEN> historyList = HistoryCAD.ReadHistoryByUserID(user.Id);

                foreach (HistoryEN history in historyList)
                {
                    if (history.Booking != null)
                    {
                        TableRow row = new TableRow();

                        TableCell accoCell = new TableCell();
                        accoCell.Text = history.Booking.Accommodation != null ? history.Booking.Accommodation.Name : "";
                        accoCell.Style["background-color"] = "lightblue";
                        accoCell.Style["padding"] = "5px";
                        row.Cells.Add(accoCell);

                        TableCell checkinCell = new TableCell();
                        checkinCell.Text = history.Booking.CheckInDate != null ? history.Booking.CheckInDate.ToShortDateString() : "";
                        checkinCell.Style["background-color"] = "lightblue";
                        checkinCell.Style["padding"] = "5px";
                        row.Cells.Add(checkinCell);

                        TableCell checkoutCell = new TableCell();
                        checkoutCell.Text = history.Booking.CheckOutDate != null ? history.Booking.CheckOutDate.ToShortDateString() : "";
                        checkoutCell.Style["background-color"] = "lightblue";
                        checkoutCell.Style["padding"] = "5px";
                        row.Cells.Add(checkoutCell);

                        TableCell priceCell = new TableCell();
                        priceCell.Text = history.Booking.BookingPrice.ToString();
                        priceCell.Style["background-color"] = "lightblue";
                        priceCell.Style["padding"] = "5px";
                        row.Cells.Add(priceCell);
                        var successContainer = FindControl("success-container") as Panel;
                        if (Request.QueryString["booking_id"] != null && history.Booking.BookingID == int.Parse(Request.QueryString["booking_id"]))
                        {
                            label_success1.Text = $"Successful Reservation at {history.Booking.Accommodation.Name} from {history.Booking.CheckInDate.ToShortDateString()} to {history.Booking.CheckOutDate.ToShortDateString()}!";
                            label_success2.Text = $"Don't forget to pay for the accommodation fee when you arrive at your accommodation.";
                            
                        }
                        else
                        {
                            
                        }
                        HistoryTable.Rows.Add(row);
                    }
                }

            }
        }
    }
}