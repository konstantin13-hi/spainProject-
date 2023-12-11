
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
    public partial class AdminLandmark : System.Web.UI.Page
    {
        // Initialize objects
        LandmarkEN en = new LandmarkEN();
        DataSet d = new DataSet();
        UserEN user = new UserEN();
        LocationEN locationEN = new LocationEN();
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
                    // Populate dropdown list with values
                    d = en.ReadAllLandmarksDataSet();
                    AdminLandmarksGridView.DataSource = d;
                    AdminLandmarksGridView.DataBind();
                    PopulateLocations();
                }
            }
        }

        // Displays modification panel
        private void DisplayLandmarkModification()
        {
            ButtonNewLandmark.Visible = false;
            PanelLandmarkValues.Visible = true;
            ButtonUpdateLandmark.Visible = true;
            ButtonDeleteLandmark.Visible = true;
            ButtonCancel.Visible = true;
        }

        // Hides modification panel
        private void HideLandmarkModification()
        {
            ButtonNewLandmark.Visible = true;
            PanelLandmarkValues.Visible = false;
            ButtonUpdateLandmark.Visible = false;
            ButtonDeleteLandmark.Visible = false;
            ButtonCancel.Visible = false;
        }

        // Displays creation panel
        private void DisplayLandmarkCreation()
        {
            ButtonNewLandmark.Visible = false;
            PanelLandmarkValues.Visible = true;
            ButtonCreateLandmark.Visible = true;
            ButtonCancel.Visible = true;
        }

        // Hides creation panel
        private void HideLandmarkCreation()
        {
            ButtonNewLandmark.Visible = true;
            PanelLandmarkValues.Visible = false;
            ButtonCreateLandmark.Visible = false;
            ButtonCancel.Visible = false;
        }

        // Extra function to manage panels
        protected void NewLandmark(object sender, EventArgs e)
        {
            HideLandmarkModification();
            DisplayLandmarkCreation();
        }

        // Creates a new landmark in the database with values from fields
        // Rebinds the gridview
        protected void CreateLandmark(object sender, EventArgs e)
        {
            en.Name = TextBoxLandmarkName.Text;
            en.Type = TextBoxLandmarkType.Text;
            en.Location_id = int.Parse(DropDownListLocations.SelectedValue);
            en.Address = TextBoxLandmarkAdress.Text;
            en.Price = int.Parse(TextBoxLandmarkPrice.Text);
            en.WebsiteLink = TextBoxLandmarkLink.Text;

            en.CreateLandmark();
            AdminLandmarksGridView.DataSource = en.ReadAllLandmarksDataSet();
            AdminLandmarksGridView.DataBind();
            HideLandmarkCreation();
        }

        // Updates a landmark in the database with values from fields
        // Rebinds the gridview
        protected void UpdateLandmark(object sender, EventArgs e)
        {
            en.Id = int.Parse(AdminLandmarksGridView.SelectedRow.Cells[1].Text);
            en.Name = TextBoxLandmarkName.Text;
            en.Type = TextBoxLandmarkType.Text;
            en.Location_id = int.Parse(DropDownListLocations.SelectedValue);
            en.Address = TextBoxLandmarkAdress.Text;
            en.Price = int.Parse(TextBoxLandmarkPrice.Text); //float field
            en.WebsiteLink = TextBoxLandmarkLink.Text;

            en.UpdateLandmark();
            AdminLandmarksGridView.DataSource = en.ReadAllLandmarksDataSet();
            AdminLandmarksGridView.DataBind();
            HideLandmarkModification();
        }

        // Deletes the selected landmark
        // Rebinds gridview
        protected void DeleteLandmark(object sender, EventArgs e)
        {
            en.Id = int.Parse(AdminLandmarksGridView.SelectedRow.Cells[1].Text);
            en.DeleteLandmark();
            AdminLandmarksGridView.DataSource = en.ReadAllLandmarksDataSet();
            AdminLandmarksGridView.DataBind();
            HideLandmarkModification();
        }

        // Hides all panels
        protected void CancelChanges(object sender, EventArgs e)
        {
            HideLandmarkModification();
            HideLandmarkCreation();
        }

        // Fills in fields with data from selected landmark
        protected void AdminLandmarksGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            HideLandmarkCreation();
            DisplayLandmarkModification();
            en.Id = int.Parse(AdminLandmarksGridView.SelectedRow.Cells[1].Text);
            en.ReadCurrentLandmark();
            TextBoxLandmarkName.Text = en.Name;
            TextBoxLandmarkPrice.Text = en.Price.ToString();
            DropDownListLocations.SelectedValue = en.Location_id.ToString();
            TextBoxLandmarkAdress.Text = en.Address;
            TextBoxLandmarkType.Text = en.Type;
            TextBoxLandmarkLink.Text = en.WebsiteLink;
        }

        // populates the location dropdown list
        protected void PopulateLocations()
        {
            DataSet locations = locationEN.ReadAllLocationsDataSet();
            DropDownListLocations.DataSource = locations;
            DropDownListLocations.DataValueField = "id";
            DropDownListLocations.DataTextField = "name";
            DropDownListLocations.DataBind();

        }
    }
}