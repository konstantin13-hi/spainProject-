using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using library;

namespace TravelAgencyWeb.Pages
{
    public partial class Welcome : System.Web.UI.Page
    {
        // Initialize objects and datasets
        LocationEN enLocation = new LocationEN();
        AccomodationEN enAccomodation = new AccomodationEN();
        LandmarkEN enLandmark = new LandmarkEN();

        DataSet DSLocations = new DataSet();
        DataSet DSAccomodations = new DataSet();
        DataSet DSLandmarks = new DataSet();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // Bind datasets to repeaters
                DSLocations = enLocation.ReadAllLocationsDataSet();
                HomeLocationsRepeater.DataSource = DSLocations;
                HomeLocationsRepeater.DataBind();

                DSAccomodations = enAccomodation.ReadAllAccomodationsDataSet();
                HomeAccomodationsRepeater.DataSource = DSAccomodations;
                HomeAccomodationsRepeater.DataBind();

                DSLandmarks = enLandmark.ReadAllLandmarksDataSet();
                HomeLandmarksRepeater.DataSource = DSLandmarks;
                HomeLandmarksRepeater.DataBind();
            }
        }

        // Button event redirects to chosen location
        protected void GoToLocation(object sender, RepeaterCommandEventArgs e)
        {
            var IdField = e.Item.FindControl("FieldHiddenId") as HiddenField;
            int location = int.Parse(IdField.Value);
            Response.Redirect("~/Pages/Location.aspx?par1=" + location);
        }

        // Button event redirects to chosen accomodation
        protected void GoToAccomodation(object sender, RepeaterCommandEventArgs e)
        {
            var IdField = e.Item.FindControl("FieldHiddenId") as HiddenField;
            int accomodation = int.Parse(IdField.Value);
            Response.Redirect("~/Pages/Accomodation.aspx?par1=" + accomodation);
        }

        // Button event redirects to chosen landmark
        protected void GoToLandmark(object sender, RepeaterCommandEventArgs e)
        {
            var IdField = e.Item.FindControl("FieldHiddenId") as HiddenField;
            int landmark = int.Parse(IdField.Value);
            Response.Redirect("~/Pages/Landmark.aspx?par1=" + landmark);
        }
    }
}