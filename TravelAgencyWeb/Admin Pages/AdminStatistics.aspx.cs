using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using library;

namespace TravelAgencyWeb
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        UserEN en = new UserEN();
        AccomodationEN accomodation = new AccomodationEN();

        DataSet DSHighestRankedAccomodations = new DataSet();
        DataSet DSMostReviewed = new DataSet();
        DataSet DSMostFavorited = new DataSet();
        protected void Page_Load(object sender, EventArgs e)
        {
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
        /// <summary>
        /// creates a list and calls to find the top selected stats
        /// </summary>
        protected void statsettings_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = statsettings.SelectedValue; // number of results
            DSMostReviewed = accomodation.GetMostReviewed(int.Parse(selectedValue.ToString())); // call for most reviewed accomodation query
            DSHighestRankedAccomodations = accomodation.GetHighestRanked(int.Parse(selectedValue.ToString())); // call for highest ranked accomodation query
            DSMostFavorited = accomodation.GetMostFavorited(int.Parse(selectedValue.ToString())); // call for most favorited accomodation query
            // connect datasets to gridviews
            HighestRankedAccomodations.DataSource = DSHighestRankedAccomodations;
            MostReviewed.DataSource = DSMostReviewed;
            MostFavoritedAccomodations.DataSource = DSMostFavorited;
            HighestRankedAccomodations.DataBind();
            MostReviewed.DataBind();
            MostFavoritedAccomodations.DataBind(); 
        }
    }
}