using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using library;

namespace TravelAgencyWeb.Admin_Pages
{
    public partial class AdminLocation : System.Web.UI.Page
    {
        // Initialize objects
        LocationEN en = new LocationEN();
        DataSet d = new DataSet();
        UserEN user = new UserEN();
        protected void Page_Load(object sender, EventArgs e)
        {
            // Check which user that is logged in
            user = (UserEN)Session["CurrentUser"];
            if (user == null)
            {
                Response.Redirect("~/Pages/Home.aspx");
            }
            else if (!user.IsAdmin)
            {
                Response.Redirect("~/Pages/Home.aspx");
            }
            else
            {
                if (!Page.IsPostBack)
                {
                    // Bind data to grid view
                    d = en.ReadAllLocationsDataSet();
                    AdminLocationsGridView.DataSource = d;
                    AdminLocationsGridView.DataBind();
                }
            }
        }

        // Displays modification panel
        private void DisplayLocationModification()
        {
            ButtonNewLocation.Visible = false;
            PanelLocationValues.Visible = true;
            ButtonUpdateLocation.Visible = true;
            ButtonDeleteLocation.Visible = true;
            ButtonCancel.Visible = true;
        }

        // Hides modification panel
        private void HideLocationModification()
        {
            ButtonNewLocation.Visible = true;
            PanelLocationValues.Visible = false;
            ButtonUpdateLocation.Visible = false;
            ButtonDeleteLocation.Visible = false;
            ButtonCancel.Visible = false;
        }

        // Displays creation panel
        private void DisplayLocationCreation()
        {
            ButtonNewLocation.Visible = false;
            PanelLocationValues.Visible = true;
            ButtonCreateLocation.Visible = true;
            ButtonCancel.Visible = true;
        }

        // Hides creation panel
        private void HideLocationCreation()
        {
            ButtonNewLocation.Visible = true;
            PanelLocationValues.Visible = false;
            ButtonCreateLocation.Visible = false;
            ButtonCancel.Visible = false;
        }

        // Extra function to manage panels
        protected void NewLocation(object sender, EventArgs e)
        {
            HideLocationModification();
            DisplayLocationCreation();
        }

        // Creates a new location in the database with values from fields
        // Rebinds the gridview
        protected void CreateLocation(object sender, EventArgs e)
        {
            en.Name = TextBoxLocationName.Text;
            en.Country = TextBoxLocationCountry.Text;
            en.CreateLocation();
            AdminLocationsGridView.DataSource = en.ReadAllLocationsDataSet();
            AdminLocationsGridView.DataBind();
            HideLocationCreation();
        }

        // Updates a location in the database with values from fields
        // Rebinds the gridview
        protected void UpdateLocation(object sender, EventArgs e)
        {
            en.Id = int.Parse(AdminLocationsGridView.SelectedRow.Cells[1].Text);
            en.Name = TextBoxLocationName.Text;
            en.Country = TextBoxLocationCountry.Text;
            en.UpdateLocation();
            AdminLocationsGridView.DataSource = en.ReadAllLocationsDataSet();
            AdminLocationsGridView.DataBind();
            HideLocationModification();
        }

        // Deletes the selected location
        // Rebinds gridview
        protected void DeleteLocation(object sender, EventArgs e)
        {
            en.Id = int.Parse(AdminLocationsGridView.SelectedRow.Cells[1].Text);
            en.DeleteLocation();
            AdminLocationsGridView.DataSource = en.ReadAllLocationsDataSet();
            AdminLocationsGridView.DataBind();
            HideLocationModification();
        }

        // Hides all panels
        protected void CancelChanges(object sender, EventArgs e)
        {
            HideLocationModification();
            HideLocationCreation();
        }

        // Fills in fields with data from selected location
        protected void AdminLocationsGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            HideLocationCreation();
            DisplayLocationModification();
            en.Id = int.Parse(AdminLocationsGridView.SelectedRow.Cells[1].Text);
            en.ReadLocation();
            TextBoxLocationName.Text = en.Name;
            TextBoxLocationCountry.Text = en.Country;
        }
    }
}