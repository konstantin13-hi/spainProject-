using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using library;

namespace TravelAgencyWeb
{
    public partial class Normal : System.Web.UI.MasterPage
    {
        UserEN user; 
        protected void Page_Load(object sender, EventArgs e)
        {
            // Checks which type of user is logged in
            // Displays appropriate navigation menu
            user = (UserEN)Session["CurrentUser"];
            if (user == null)
            {
                DisplayPublicMenu();
            }
            else if (user.IsAdmin)
            {
                DisplayAdminMenu();
            }
            else
            {
                DisplayUserMenu();
            }
        }

        // Displays the Admin navigation menu
        protected void DisplayAdminMenu()
        {
            AdminNavigationMenu.Visible = true;
            UserNavigationMenu.Visible = false;
            PublicNavigationMenu.Visible = false;
        }

        // Displays the User navigation menu
        protected void DisplayUserMenu()
        {
            AdminNavigationMenu.Visible = false;
            UserNavigationMenu.Visible = true;
            PublicNavigationMenu.Visible = false;

        }

        // Displays the public navigation menu
        protected void DisplayPublicMenu()
        {
            AdminNavigationMenu.Visible = false;
            UserNavigationMenu.Visible = false;
            PublicNavigationMenu.Visible = true;
        }

        // Logs out the user and returns to home page
        protected void LogoutButton(object sender, MenuEventArgs e)
        {
            if (e.Item.Text.ToString() == "Logout")
            {
                Session.Abandon();
                Response.Redirect("~/Pages/Home.aspx");
            }
        }
    }
}