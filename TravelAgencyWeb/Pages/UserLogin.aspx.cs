using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using library;


namespace TravelAgencyWeb.Pages
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        UserEN en = new UserEN();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ErrorMessage"] != null)
            {
                string errorMessage = Session["ErrorMessage"].ToString();
                Session.Remove("ErrorMessage");

                // Display the error message on the page
                ErrorLabel.Text = errorMessage;
                ErrorLabel.Visible = true;
            }
            
            en = (UserEN)Session["CurrentUser"];
            if (en == null)
            {

            }
            else if (en.IsAdmin)
            {
                Response.Redirect("~/Admin Pages/AdminUser.aspx");
            }
            else
            {
                Response.Redirect("~/Pages/Home.aspx");
            }
        }
        protected void Login_Click(object sender, EventArgs e)
        {
            string email = Email.Text;
            string password = Password.Value;
            UserCAD userCAD = new UserCAD();
            UserEN userEN = new UserEN();
            userEN.Email = email;
            if (userEN.GetIdFromEmail())
            {
                userEN.ReadUser();
                if (BCrypt.Net.BCrypt.Verify(password, userEN.Password))
                {

                    Session["CurrentUser"] = userEN;

                    if (userEN.IsAdmin)
                    {
                        Session["CurrentUser"] = userEN;
                        Response.Redirect("~/Admin Pages/AdminModification.aspx");
                    }
                    else
                    {
                        Session["CurrentUser"] = userEN;
                        Response.Redirect("~/Pages/Home.aspx");
                    }
                }
                else
                {
                    Session["ErrorMessage"] = "Invalid email or password.";
                    // Reload the page
                    Response.Redirect(Request.Url.AbsoluteUri);
                }
            }
            else
            {
                Session["ErrorMessage"] = "User does not exist.";
                Response.Redirect(Request.Url.AbsoluteUri);
            }
        }
    }
}