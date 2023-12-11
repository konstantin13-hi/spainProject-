using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using library;

namespace TravelAgencyWeb
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        // Initialize objects
        UserEN en = new UserEN();
        protected void Page_Load(object sender, EventArgs e)
        {
            // Check which user is logged in
            en = (UserEN)Session["CurrentUser"];
            if (en == null)
            {
                Response.Redirect("~/Pages/Home.aspx");
            }
            else if (!en.IsAdmin)
            {
                Response.Redirect("~/Pages/Home.aspx");
            }
            else
            {

            }

        }
    }
}