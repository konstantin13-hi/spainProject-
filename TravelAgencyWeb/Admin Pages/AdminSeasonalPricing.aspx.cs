using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using library;

namespace TravelAgencyWeb.Admin_Pages
{
    public partial class AdminSeasonalPricing : System.Web.UI.Page
    {

        /*
         * ToDo:
         * - prüfen ob angegenbenes Datum nicht mit anderem Daten Korrespondiert.
         * - Accomodation meit einbringen.
         * - Prüfen ob angegebenes Startdatum in der Zukunft liegt.
         * - Kann ich im Calendar Element bestimmte Daten ausgrauen, sodass sie nicht mehr wählbar sind? Um Überscheidungen auszuschließen.
         * 
         */



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadSeasonalPricings();
            }
        }

        // Actions if User pushes Create Button
        protected void Create_Click(object sender, EventArgs e)
        {
            double multiplier;
            feedback.Visible = true;
            if (NameTextBox.Text.Equals("") || MultiplierTextBox.Text.Equals("")) // Check if Multiplier and Name are given
            {
                feedback.ForeColor = Color.Red;
                feedback.Text = "Not all fields are filled correctly.";
            }
            else
            {
                if (StartDateCalendar.SelectedDate > DateTime.Now || StartDateCalendar.SelectedDate > EndDateCalendar.SelectedDate) // Check if realistic dates are given
                {
                    if (double.TryParse(MultiplierTextBox.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out multiplier) && multiplier > 0) // Check if it is possible to make double out of input NumberStyle and CultureInfo necessary because of different Browser language settings
                    {
                        // All information given is valid
                        SeasonalPricingEN pricing = new SeasonalPricingEN();
                        pricing.NamePricing = NameTextBox.Text;
                        pricing.MultiplierPricing = multiplier;
                        pricing.StartDate = StartDateCalendar.SelectedDate;
                        pricing.EndDate = EndDateCalendar.SelectedDate;

                        if (pricing.createSeasonalPricing())
                        {
                            // if creting worked show success message reload dropdown and Gridview and empty all other fields
                            feedback.Text = pricing.message;
                            feedback.ForeColor = Color.Green;
                            LoadSeasonalPricings();
                            ClearFields();
                        }
                        else
                        {
                            // Creation failed, show error message
                            feedback.Text = pricing.message;
                            feedback.ForeColor = Color.Red;
                        }
                    }
                    else
                    {
                        // Multiplier is not valid
                        feedback.ForeColor = Color.Red;
                        feedback.Text = "Multiplier has to be greater than 0.";
                    }
                
                }
                else
                {
                    // Selected dates are not valid
                    feedback.ForeColor = Color.Red;
                    feedback.Text = "Invalid dates selected.";
                }
            }
        }


        // Actions if User pushes update button
        protected void Update_Click(object sender, EventArgs e)
        {
            double multiplier;
            int pricingID;
            feedback.Visible = true;
            if (!IDTextBox.Text.Equals("") && !NameTextBox.Text.Equals("") && !MultiplierTextBox.Text.Equals(""))
            {
                if (StartDateCalendar.SelectedDate > DateTime.Now || StartDateCalendar.SelectedDate < EndDateCalendar.SelectedDate)
                {
                    if (double.TryParse(MultiplierTextBox.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out multiplier) && multiplier > 0 && int.TryParse(IDTextBox.Text, out pricingID))
                    {
                        SeasonalPricingEN pricing = new SeasonalPricingEN();
                        pricing.Id = pricingID;
                        pricing.NamePricing = NameTextBox.Text;
                        pricing.MultiplierPricing = multiplier;
                        pricing.StartDate = StartDateCalendar.SelectedDate;
                        pricing.EndDate = EndDateCalendar.SelectedDate;

                        if (pricing.updateSeasonalPricing())
                        {
                            feedback.Text = pricing.message;
                            feedback.ForeColor = Color.Green;
                            LoadSeasonalPricings();
                            ClearFields();
                        }
                        else
                        {
                            feedback.Text = pricing.message;
                            feedback.ForeColor = Color.Red;
                        }
                    }
                    else
                    {
                        // Multiplier is not valid
                        feedback.ForeColor = Color.Red;
                        feedback.Text = "Multiplier has to be greater than 0.";
                    }
                } else
                {
                // Selected dates are not valid
                    feedback.ForeColor = Color.Red;
                    feedback.Text = "Invalid dates selected.";
                }
            }
            else
            {
                // ID, Name, and Multiplier Textboxes are not valid
                feedback.ForeColor = Color.Red;
                feedback.Text = "Not all fields are filled correctly.";
            }

        }

        // Actions if User pushes Create Button
        protected void Delete_Click(object sender, EventArgs e)
        {
            int pricingID;
            feedback.Visible = true;
            if (int.TryParse(IDTextBox.Text, out pricingID))
            {
                // if ID is valid, which means Seasonalpricing is selected
                SeasonalPricingEN pricing = new SeasonalPricingEN();
                pricing.Id = pricingID;

                if (pricing.deleteSeasonalPricing())
                {
                    // show success message and empty fields, clear selection
                    feedback.ForeColor = Color.Green;
                    feedback.Text = pricing.message;
                    LoadSeasonalPricings();
                    ClearFields();
                }
                else
                {
                    // show error message of deleteSeasonalpricing method
                    feedback.ForeColor = Color.Red;
                    feedback.Text = pricing.message;
                }
            }
            else
            {
                // show error message if no Seasonalpricing is selected
                feedback.ForeColor = Color.Red;
                feedback.Text = "No SeasonalPricing ID provided.";
            }

        }

        // Filling of all Textboxes and Fields with the corresponding data to the SeasonalPricing selected
        protected void AllSeasonalPricing_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow selectedRow = AllSeasonalPricing.SelectedRow;

            SeasonalPricingEN pricing = new SeasonalPricingEN();
            pricing.Id = int.Parse(selectedRow.Cells[1].Text);
            pricing.readSeasonalPricing();


            IDTextBox.Text = pricing.Id.ToString();
            NameTextBox.Text = pricing.NamePricing;
            MultiplierTextBox.Text = pricing.MultiplierPricing.ToString();
            StartDateCalendar.SelectedDate = pricing.StartDate;
            StartDateCalendar.VisibleDate = pricing.StartDate;
            EndDateCalendar.SelectedDate = pricing.EndDate;
            EndDateCalendar.VisibleDate = pricing.EndDate;
            feedback.Visible = false;
        }

        // Ensures that all fields are empty and no selections are taken. Creates virgin appearance of the Interface
        // With reloading of the Seasonalpricing Gridview
        protected void Clear_Click(object sender, EventArgs e)
        {
            IDTextBox.Text = string.Empty;
            NameTextBox.Text = string.Empty;
            MultiplierTextBox.Text = string.Empty;
            StartDateCalendar.SelectedDate = DateTime.MinValue;
            EndDateCalendar.SelectedDate = DateTime.MinValue;
            LoadSeasonalPricings();
            feedback.Visible = false;
        }

        // Load all Seasonalpricings in the application and display them in the AllSeasonalpricings GridView
        private void LoadSeasonalPricings()
        {
            SeasonalPricingCAD spCAD = new SeasonalPricingCAD();
            List<SeasonalPricingEN> seasonalPricings = spCAD.readAllSeasonalPricings();

            if (seasonalPricings.Count == 0)
            {
                NoSeasonalPricesLoaded.Visible = true;
                NoSeasonalPricesLoaded.Text = "There are no Seasonal Pricings that can be loaded.";
                AllSeasonalPricing.Visible = false;
            }
            else
            {
                NoSeasonalPricesLoaded.Visible = false;
                AllSeasonalPricing.Visible = true;
                AllSeasonalPricing.DataSource = seasonalPricings;
                AllSeasonalPricing.DataBind();
            }
        }

        // empty all fields in the Interface
        private void ClearFields()
        {
            IDTextBox.Text = string.Empty;
            NameTextBox.Text = string.Empty;
            MultiplierTextBox.Text = string.Empty;
            StartDateCalendar.SelectedDate = DateTime.MinValue;
            EndDateCalendar.SelectedDate = DateTime.MinValue;
        }
    }
}