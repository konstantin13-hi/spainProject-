using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using library;

namespace TravelAgencyWeb.Pages
{
    public partial class UserRegistration : System.Web.UI.Page
    {
        UserEN en = new UserEN();
        protected void Page_Load(object sender, EventArgs e)
        {
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

        protected void Register_Click(object sender, EventArgs e)
        {
            string email = Email.Text;
            string username = Username.Text;
            string password = Password.Text;

            UserEN existingUser = new UserEN();
            existingUser.Email = email;
            existingUser.GetIdFromEmail();

            if (existingUser.Id > 0)
            {
                ErrorLabel.Text = "User with the provided email already exists.";
                ErrorLabel.Visible = true;
                ErrorLabel.ForeColor = Color.Red; 
            }
            else
            {
                UserEN newUser = new UserEN();
                newUser.Email = email;
                newUser.Name = username;
                newUser.Password = BCrypt.Net.BCrypt.HashPassword(password);

                newUser.CreateUser();

                Session["CurrentUser"] = newUser;

                if (newUser.IsAdmin)
                {
                    Response.Redirect("AdminModification.aspx");
                }
                else
                {
                    Response.Redirect("Home.aspx");
                }
            }
        }
    }
}