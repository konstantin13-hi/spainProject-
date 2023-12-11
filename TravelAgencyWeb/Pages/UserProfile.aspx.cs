using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using library;

namespace TravelAgencyWeb.Pages
{
    public partial class UserProfile : System.Web.UI.Page
    {
        // Initialize user
        UserEN en = new UserEN();
        protected void Page_Load(object sender, EventArgs e)
        {
            // Check which type of user
            en = (UserEN)Session["CurrentUser"];
            if (en == null)
            {
                Response.Redirect("~/Pages/Home.aspx");
            }
            else if (en.IsAdmin)
            {
                Response.Redirect("~/Admin Pages/AdminUser.aspx");
            }
            else
            {
                TextBoxInfoName.Text = en.Name;
                TextBoxInfoEmail.Text = en.Email;
            }
        }

        // Shows the edit profile panel
        protected void ButtonEditProfile_Click(object sender, EventArgs e)
        {
            TextBoxName.Text = en.Name;
            TextBoxEmail.Text = en.Email;
            TextBoxPassword.Text = "********";
            ProfileSettings.Visible = true;
        }

        // Saves changes to the database
        protected void ButtonSaveChanges(object sender, EventArgs e)
        {
            en.Name = TextBoxName.Text;
            en.Email = TextBoxEmail.Text;
            en.Password = BCrypt.Net.BCrypt.HashPassword(TextBoxPassword.Text);
            en.UpdateUser();
            TextBoxInfoName.Text = en.Name;
            TextBoxInfoEmail.Text = en.Email;
            ProfileSettings.Visible = false;
        }

        // Hides settings panel
        protected void ButtonCancelChanges(object sender, EventArgs e)
        {
            ProfileSettings.Visible = false;
        }
    }
}