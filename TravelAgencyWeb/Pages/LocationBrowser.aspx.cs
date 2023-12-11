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
    public partial class LocationBrowser : System.Web.UI.Page
    {
        protected LocationEN Location { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Call the method from the EN layer to retrieve the popular locations
                DataSet popularLocations = getPopularLocations();

                // Check if the dataset contains any data
                if (popularLocations != null && popularLocations.Tables.Count > 0 && popularLocations.Tables[0].Rows.Count > 0)
                {
                    // Bind the dataset to the Repeater control
                    CardRepeater.DataSource = popularLocations.Tables[0];
                    CardRepeater.DataBind();
                }
                else
                {
                    searchLocationsLabel.Text = "No data to display.";
                }
            }
        }

        private DataSet getPopularLocations()
        {
            LocationEN en = new LocationEN(); // Create an instance of Location EN class
            return en.ReadPopularDataSet(); // Call the method from the EN class to retrieve the dataset
        }



        /// <summary>
        /// Creates a Location object and calls to read the objects data. If data exists for the object it will
        /// show the data of the Location. If there is found no data it will display that there is no location with that name.
        /// </summary>
        protected void searchLocationsButton_click(object Sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(searchLocationsBox.Text)) // check that textbox isnt empty
            {
                searchLocationsLabel.Text = "Please fill out the textbox to search for a location.";
            }
            else
            {
                LocationEN searchedLocation = new LocationEN(); // create a new instance of location
                DataSet searchResults = searchedLocation.ReadLocationName(searchLocationsBox.Text); // retrieve the dataset
                if (searchResults != null && searchResults.Tables.Count > 0 && searchResults.Tables[0].Rows.Count > 0)
                {
                    searchLocationsLabel.Text = "Showing search results:";
                    // Bind the dataset to the Repeater control
                    ResultRepeater.DataSource = searchResults.Tables[0];
                    ResultRepeater.DataBind();
                }
                else
                {
                    searchLocationsLabel.Text = $"No location with name {searchLocationsBox.Text} found.";
                }
            }
        }
        /// <summary>
        /// Click on see more button. Takes you too the page of the displayed location.
        /// </summary>
        protected void seeLocation(object sender, EventArgs e)
        {
            Button locationButton = (Button)sender; // Get the button that triggered the event
            RepeaterItem repeaterItem = (RepeaterItem)locationButton.NamingContainer; // Get the parent RepeaterItem

            HiddenField repeaterIdHiddenField = (HiddenField)repeaterItem.FindControl("RepeaterIdHiddenField"); // Find the HiddenField control

            if (repeaterIdHiddenField != null)
            {
                if (int.TryParse(repeaterIdHiddenField.Value, out int locationId))
                {
                    Response.Redirect($"~/Pages/Location.aspx?par1=" + int.Parse(repeaterIdHiddenField.Value));
                }
                else
                {
                    searchLocationsLabel.Text = "Error loading page.";
                }
            }
            else
            {
                searchLocationsLabel.Text = "Error loading page.";
            }
        }

    }
}