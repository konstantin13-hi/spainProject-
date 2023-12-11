using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using library;
using System.Data;

namespace TravelAgencyWeb.Pages
{
    public partial class Locations : System.Web.UI.Page
    {
        protected LocationEN Location { get; set; }
        protected WeatherEN Weather { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string locationId = Request.QueryString["par1"]; // gets the location id from the url

                if (!string.IsNullOrEmpty(locationId))
                {
                    Location = new LocationEN
                    {
                        Id = int.Parse(locationId) // sets the id to the location instance
                    };
                    Location.ReadLocation();
                    DataBind();

                    // Call the method from the EN layer to retrieve the popular locations
                    DataSet Accomodations = Location.ReadAccomodations();
                    DataSet Landmarks = Location.ReadLandmarks();
                    Weather = new WeatherEN();
                    DataSet DSWeather = Weather.ReadWeathersOfLocation(int.Parse(locationId));
                    WeatherGridView.DataSource = DSWeather;
                    WeatherGridView.DataBind();
                    // Check if the dataset contains any data
                    if (Accomodations != null && Accomodations.Tables.Count > 0 && Accomodations.Tables[0].Rows.Count > 0)
                    {
                        // Bind the dataset to the Repeater control
                        AccomodationRepeater.DataSource = Accomodations.Tables[0];
                        AccomodationRepeater.DataBind();
                    }
                    else
                    {
                        // When there are no accomodations
                        AccomodationRepeater.Visible = false;
                        AccomodationLabel.Text = $"No accomodations in {Location.Name}";
                    }
                    if (Landmarks != null && Landmarks.Tables.Count > 0 && Landmarks.Tables[0].Rows.Count > 0)
                    {
                        // Bind the dataset to the Repeater control
                        LandmarkRepeater.DataSource = Landmarks.Tables[0];
                        LandmarkRepeater.DataBind();
                    }
                    else
                    {
                        // When there are no landmarks
                        LandmarkRepeater.Visible = false;
                        LandmarkLabel.Text = $"No landmarks in {Location.Name}";
                    }
                }

                else
                {
                    // when the location ID is missing or invalid
                    ErrorMessageLabel.Text = "Invalid location ID.";
                }
            }
        }

        /// <summary>
        /// directs you too the accomodation page of the clicked button
        /// </summary>
        protected void seeAccom(object sender, EventArgs e)
        {
            Button locationButton = (Button)sender; // Get the button that triggered the event
            RepeaterItem repeaterItem = (RepeaterItem)locationButton.NamingContainer; // Get the parent RepeaterItem

            HiddenField hiddenId = (HiddenField)repeaterItem.FindControl("AccomRepeaterIdHiddenField"); // Find the HiddenField control

            if (hiddenId != null)
            {
                if (int.TryParse(hiddenId.Value, out int accomodationId))
                {
                    Response.Redirect($"~/Pages/Accomodation.aspx?par1=" + accomodationId);
                }
                else
                {
                    // when the value is not a valid integer
                    ErrorMessageLabel.Text = "Invalid accomodation ID.";
                }
            }
            else
            {
                // when the HiddenField control is not found
                ErrorMessageLabel.Text = "Accomodation ID not found.";
            }

        }
        /// <summary>
        /// directs you too the the landmark page of the clicked button
        /// </summary>
        protected void seeLandmark(object sender, EventArgs e)
        {
            Button locationButton = (Button)sender; // Get the button that triggered the event
            RepeaterItem repeaterItem = (RepeaterItem)locationButton.NamingContainer; // Get the parent RepeaterItem

            HiddenField hiddenId = (HiddenField)repeaterItem.FindControl("LandmarkRepeaterIdHiddenField"); // Find the HiddenField control

            if (hiddenId != null)
            {
                if (int.TryParse(hiddenId.Value, out int landmarkId))
                {
                    Response.Redirect($"~/Pages/Landmark.aspx?par1=" + landmarkId);
                }
                else
                {
                    // when the value is not a valid integer
                    ErrorMessageLabel.Text = "Invalid landmark ID.";
                }
            }
            else
            {
                // Handle the case when the HiddenField control is not found
                ErrorMessageLabel.Text = "Landmark ID not found.";
            }
        }
    }
}