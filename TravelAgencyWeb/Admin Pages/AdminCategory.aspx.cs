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
    public partial class WebForm2 : System.Web.UI.Page
    {
        // Initialize objects
        CategoryEN en = new CategoryEN();
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
                    d = en.ReadAllCategoriesDataSet();
                    AdminCategoriesGridView.DataSource = d;
                    AdminCategoriesGridView.DataBind();
                }
            }
        }

        // Displays modification panel
        private void DisplayCategoryModification()
        {
            ButtonNewCategory.Visible = false;
            PanelCategoryValues.Visible = true;
            ButtonUpdateCategory.Visible = true;
            ButtonDeleteCategory.Visible = true;
            ButtonCancel.Visible = true;
        }

        // Hides modification panel
        private void HideCategoryModification()
        {
            ButtonNewCategory.Visible = true;
            PanelCategoryValues.Visible = false;
            ButtonUpdateCategory.Visible = false;
            ButtonDeleteCategory.Visible = false;
            ButtonCancel.Visible = false;
        }

        // Displays creation panel
        private void DisplayCategoryCreation()
        {
            ButtonNewCategory.Visible = false;
            PanelCategoryValues.Visible = true;
            ButtonCreateCategory.Visible = true;
            ButtonCancel.Visible = true;
        }

        // Hides creation panel
        private void HideCategoryCreation()
        {
            ButtonNewCategory.Visible = true;
            PanelCategoryValues.Visible = false;
            ButtonCreateCategory.Visible = false;
            ButtonCancel.Visible = false;
        }

        // Extra function to manage panels
        protected void NewCategory(object sender, EventArgs e)
        {
            HideCategoryModification();
            DisplayCategoryCreation();
        }

        // Creates a new category in the database with values from fields
        // Rebinds the gridview
        protected void CreateCategory(object sender, EventArgs e)
        {
            en.Name = TextBoxCategoryName.Text;
            en.CreateCategory();
            AdminCategoriesGridView.DataSource = en.ReadAllCategoriesDataSet();
            AdminCategoriesGridView.DataBind();
            HideCategoryCreation();
        }

        // Updates a category in the database with values from fields
        // Rebinds the gridview
        protected void UpdateCategory(object sender, EventArgs e)
        {
            en.Id = int.Parse(AdminCategoriesGridView.SelectedRow.Cells[1].Text);
            en.Name = TextBoxCategoryName.Text;
            en.UpdateCategory();
            AdminCategoriesGridView.DataSource = en.ReadAllCategoriesDataSet();
            AdminCategoriesGridView.DataBind();
            HideCategoryModification();
        }

        // Deletes the selected category
        // Rebinds gridview
        protected void DeleteCategory(object sender, EventArgs e)
        {
            en.Id = int.Parse(AdminCategoriesGridView.SelectedRow.Cells[1].Text);
            en.DeleteCategory();
            AdminCategoriesGridView.DataSource = en.ReadAllCategoriesDataSet();
            AdminCategoriesGridView.DataBind();
            HideCategoryModification();
        }

        // Hides all panels
        protected void CancelChanges(object sender, EventArgs e)
        {
            HideCategoryModification();
            HideCategoryCreation();
        }
        
        // Fills in fields with data from selected category
        protected void AdminCategoriesGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            HideCategoryCreation();
            DisplayCategoryModification();
            en.Id = int.Parse(AdminCategoriesGridView.SelectedRow.Cells[1].Text);
            en.ReadCategory();
            TextBoxCategoryName.Text = en.Name;
        }
    }
}