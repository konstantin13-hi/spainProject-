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
    public partial class AdminUser : System.Web.UI.Page
    {
        // Initialize objects
        UserEN sessionUser = new UserEN();
        UserEN en = new UserEN();
        DataSet d = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Check which user that is logged in
            sessionUser = (UserEN)Session["CurrentUser"];
            if (sessionUser == null)
            {
                Response.Redirect("~/Pages/Home.aspx");
            }
            else if (!sessionUser.IsAdmin)
            {
                Response.Redirect("~/Pages/Home.aspx");
            }
            else
            {
                if (!Page.IsPostBack)
                {
                    // Bind data to grid view
                    d = en.ReadAllUsersDataSet();
                    AdminUsersGridView.DataSource = d;
                    AdminUsersGridView.DataBind();
                }
            }
        }

        // Displays modification panel
        private void DisplayUserModification()
        {
            ButtonNewUser.Visible = false;
            PanelUserValues.Visible = true;
            ButtonUpdateUser.Visible = true;
            ButtonDeleteUser.Visible = true;
            ButtonCancel.Visible = true;
        }

        // Hides modification panel
        private void HideUserModification()
        {
            ButtonNewUser.Visible = true;
            PanelUserValues.Visible = false;
            ButtonUpdateUser.Visible = false;
            ButtonDeleteUser.Visible = false;
            ButtonCancel.Visible = false;
        }

        // Displays creation panel
        private void DisplayUserCreation()
        {
            ButtonNewUser.Visible = false;
            PanelUserValues.Visible = true;
            ButtonCreateUser.Visible = true;
            ButtonCancel.Visible = true;
        }

        // Hides creation panel
        private void HideUserCreation()
        {
            ButtonNewUser.Visible = true;
            PanelUserValues.Visible = false;
            ButtonCreateUser.Visible = false;
            ButtonCancel.Visible = false;
        }

        // Extra function to manage panels
        protected void NewUser(object sender, EventArgs e)
        {
            HideUserModification();
            DisplayUserCreation();
        }

        // Hides all panels
        protected void CancelChanges(object sender, EventArgs e)
        {
            HideUserModification();
            HideUserCreation();
        }

        // Creates a new user in the database with values from fields
        // Rebinds the gridview
        protected void CreateUser(object sender, EventArgs e)
        {
            en.Email = TextBoxEmail.Text;
            en.Password = BCrypt.Net.BCrypt.HashPassword(TextBoxPassword.Text);
            en.Name = TextBoxName.Text;
            en.IsAdmin = bool.Parse(DropDownIsAdmin.SelectedValue);
            en.CreateUser();
            AdminUsersGridView.DataSource = en.ReadAllUsersDataSet();
            AdminUsersGridView.DataBind();
            HideUserCreation();
        }

        // Updates a user in the database with values from fields
        // Rebinds the gridview
        protected void UpdateUser(object sender, EventArgs e)
        {
            en.Id = int.Parse(AdminUsersGridView.SelectedRow.Cells[1].Text);
            en.Email = TextBoxEmail.Text;
            en.Password = BCrypt.Net.BCrypt.HashPassword(TextBoxPassword.Text);
            en.Name = TextBoxName.Text;
            en.IsAdmin = bool.Parse(DropDownIsAdmin.SelectedValue);

            en.UpdateUser();
            AdminUsersGridView.DataSource = en.ReadAllUsersDataSet();
            AdminUsersGridView.DataBind();
            HideUserModification();

        }

        // Deletes the selected user
        // Rebinds gridview
        protected void DeleteUser(object sender, EventArgs e)
        {
            en.Id = int.Parse(AdminUsersGridView.SelectedRow.Cells[1].Text);
            en.DeleteUser();
            AdminUsersGridView.DataSource = en.ReadAllUsersDataSet();
            AdminUsersGridView.DataBind();
            HideUserModification();
        }

        // Fills in fields with data from selected user
        protected void AdminUsersGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            HideUserCreation();
            DisplayUserModification();
            en.Id = int.Parse(AdminUsersGridView.SelectedRow.Cells[1].Text);
            en.ReadUser();
            TextBoxEmail.Text = en.Email;
            TextBoxPassword.Text = en.Password;
            TextBoxName.Text = en.Name;
            DropDownIsAdmin.SelectedValue = en.IsAdmin.ToString();
        }
    }
}