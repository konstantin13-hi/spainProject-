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
    public partial class WebForm1 : System.Web.UI.Page
    {
        // Initialize objects
        AccomodationEN en = new AccomodationEN();
        DataSet d = new DataSet();
        UserEN user = new UserEN();
        LocationEN locationEN = new LocationEN();
        CategoryEN categoryEN = new CategoryEN();
        SeasonalPricingEN spEN = new SeasonalPricingEN();

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
                    // Bind data to grid view and dropdown lists
                    d = en.ReadAllAccomodationsDataSet();
                    AdminAccomodationsGridView.DataSource = d;
                    AdminAccomodationsGridView.DataBind();
                    PopulateCategories();
                    PopulateLocations();
                    PopulateSeasonalPrices();

                    en.locationEN = locationEN;
                    en.categoryEN = categoryEN;
                    en.seasonalPriceEN = spEN;
                }

            }
        }

        // Displays modification panel
        private void DisplayAccomodationModification()
        {
            ButtonNewAccomodation.Visible = false;
            PanelAccomodationValues.Visible = true;
            ButtonUpdateAccomodation.Visible = true;
            ButtonDeleteAccomodation.Visible = true;
            ButtonCancel.Visible = true;
        }

        // Hides modification panel
        private void HideAccomodationModification()
        {
            ButtonNewAccomodation.Visible = true;
            PanelAccomodationValues.Visible = false;
            ButtonUpdateAccomodation.Visible = false;
            ButtonDeleteAccomodation.Visible = false;
            ButtonCancel.Visible = false;
        }

        // Displays creation panel
        private void DisplayAccomodationCreation()
        {
            ButtonNewAccomodation.Visible = false;
            PanelAccomodationValues.Visible = true;
            ButtonCreateAccomodation.Visible = true;
            ButtonCancel.Visible = true;
        }

        // Hides creation panel
        private void HideAccomodationCreation()
        {
            ButtonNewAccomodation.Visible = true;
            PanelAccomodationValues.Visible = false;
            ButtonCreateAccomodation.Visible = false;
            ButtonCancel.Visible = false;
        }

        // Fills in fields with data from selected accomodation
        protected void AdminAccomodationsGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            HideAccomodationCreation();
            DisplayAccomodationModification();
            en.Id = int.Parse(AdminAccomodationsGridView.SelectedRow.Cells[1].Text);
            en.ReadAccommodationBool();
            TextBoxAccomodationName.Text = en.Name;
            TextBoxAccomodationAdress.Text = en.Address;
            TextBoxAccomodationRooms.Text = en.NumberOfRooms.ToString();
            TextBoxAccomodationArea.Text = en.Area.ToString();
            TextBoxAccomodationPrice.Text = en.Price.ToString();
            DropDownListAccomodationSwimmingPool.SelectedValue = en.HasSwimmingPool.ToString();
            DropDownAccomodationListGym.SelectedValue = en.HasGym.ToString();
            DropDownAccomodationListParking.SelectedValue = en.HasParking.ToString();
            DropDownAccomodationListPets.SelectedValue = en.IsPetFriendly.ToString();
            TextBoxAccomodationDescription.Text = en.Description;
            DropDownListCategories.SelectedValue = en.categoryEN.Id.ToString();
            DropDownListLocations.SelectedValue = en.locationEN.Id.ToString();
            DropDownListSeasonalPrices.SelectedValue = en.seasonalPriceEN.Id.ToString();
        }

        // Extra function to manage panels
        protected void NewAccomodation(object sender, EventArgs e)
        {
            HideAccomodationModification();
            DisplayAccomodationCreation();
        }

        // Creates a new accomodation in the database with values from fields
        // Rebinds the gridview
        protected void CreateAccomodation(object sender, EventArgs e)
        {
            en.Name = TextBoxAccomodationName.Text;
            en.Address = TextBoxAccomodationAdress.Text;
            en.NumberOfRooms = int.Parse(TextBoxAccomodationRooms.Text);
            en.Price = decimal.Parse(TextBoxAccomodationPrice.Text);
            en.HasSwimmingPool = bool.Parse(DropDownListAccomodationSwimmingPool.SelectedValue);
            en.HasGym = bool.Parse(DropDownAccomodationListGym.SelectedValue);
            en.HasParking = bool.Parse(DropDownAccomodationListParking.SelectedValue);
            en.IsPetFriendly = bool.Parse(DropDownAccomodationListPets.SelectedValue);
            en.Description = TextBoxAccomodationDescription.Text;
            en.categoryEN = new CategoryEN();
            en.locationEN = new LocationEN();
            en.seasonalPriceEN = new SeasonalPricingEN();
            en.categoryEN.Id = int.Parse(DropDownListCategories.SelectedValue);
            en.locationEN.Id = int.Parse(DropDownListLocations.SelectedValue);
            en.seasonalPriceEN.Id = int.Parse(DropDownListSeasonalPrices.SelectedValue);

            en.CreateAccommodationBool();
            AdminAccomodationsGridView.DataSource = en.ReadAllAccomodationsDataSet();
            AdminAccomodationsGridView.DataBind();
            HideAccomodationCreation();
        }

        // Updates a accomodation in the database with values from fields
        // Rebinds the gridview
        protected void UpdateAccomodation(object sender, EventArgs e)
        {
            DisplayAccomodationModification();
            en.Id = int.Parse(AdminAccomodationsGridView.SelectedRow.Cells[1].Text);
            en.Name = TextBoxAccomodationName.Text;
            en.Address = TextBoxAccomodationAdress.Text;
            en.NumberOfRooms = int.Parse(TextBoxAccomodationRooms.Text);
            en.Price = decimal.Parse(TextBoxAccomodationPrice.Text);
            en.HasSwimmingPool = bool.Parse(DropDownListAccomodationSwimmingPool.SelectedValue);
            en.HasGym = bool.Parse(DropDownAccomodationListGym.SelectedValue);
            en.HasParking = bool.Parse(DropDownAccomodationListParking.SelectedValue);
            en.IsPetFriendly = bool.Parse(DropDownAccomodationListPets.SelectedValue);
            en.Description = TextBoxAccomodationDescription.Text;
            en.categoryEN = new CategoryEN();
            en.locationEN = new LocationEN();
            en.seasonalPriceEN = new SeasonalPricingEN();
            en.categoryEN.Id = int.Parse(DropDownListCategories.SelectedValue);
            en.locationEN.Id = int.Parse(DropDownListLocations.SelectedValue);
            en.seasonalPriceEN.Id = int.Parse(DropDownListSeasonalPrices.SelectedValue);

            en.UpdateAccommodationBool();
            AdminAccomodationsGridView.DataSource = en.ReadAllAccomodationsDataSet();
            AdminAccomodationsGridView.DataBind();
            HideAccomodationModification();
        }

        // Deletes the selected accomodation
        // Rebinds gridview
        protected void DeleteAccomodation(object sender, EventArgs e)
        {
            en.Id = int.Parse(AdminAccomodationsGridView.SelectedRow.Cells[1].Text);
            en.DeleteAccommodationBool();
            AdminAccomodationsGridView.DataSource = en.ReadAllAccomodationsDataSet();
            AdminAccomodationsGridView.DataBind();
            HideAccomodationModification();
        }

        // Hides all panels
        protected void CancelChanges(object sender, EventArgs e)
        {
            HideAccomodationModification();
            HideAccomodationCreation();
        }

        // Populates the dropdown list for categories
        protected void PopulateCategories()
        {
            DataSet categories = categoryEN.ReadAllCategoriesDataSet();
            DropDownListCategories.DataSource = categories;
            DropDownListCategories.DataValueField = "id";
            DropDownListCategories.DataTextField = "name";
            DropDownListCategories.DataBind();
        }

        // Populates the dropdown list for locations
        protected void PopulateLocations()
        {
            DataSet locations = locationEN.ReadAllLocationsDataSet();
            DropDownListLocations.DataSource = locations;
            DropDownListLocations.DataValueField = "id";
            DropDownListLocations.DataTextField = "name";
            DropDownListLocations.DataBind();
        }

        // Populates the dropdown list for seasonal prices
        protected void PopulateSeasonalPrices()
        {
            DataSet seasonalPrices = spEN.ReadAllSeasonalPricesDataSet();
            DropDownListSeasonalPrices.DataSource = seasonalPrices;
            DropDownListSeasonalPrices.DataValueField = "id";
            DropDownListSeasonalPrices.DataTextField = "name";
            DropDownListSeasonalPrices.DataBind();
        }

    }
}