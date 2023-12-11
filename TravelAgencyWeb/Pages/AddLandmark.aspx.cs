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
    public partial class WebForm5 : System.Web.UI.Page
    {
        LandmarkEN newLandmark = new LandmarkEN();

        UserEN user = new UserEN();
        DataSet d = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            //check that user is logged in?
            user = (UserEN)Session["CurrentUser"];
            if (user == null)
            {
                Response.Redirect("~/Pages/UserLogin.aspx");
            }
            else if (user.IsAdmin)
            {
                Response.Redirect("~/Admin Pages/AdminLandmark.aspx");
            }
            else
            {
                if (!Page.IsPostBack)
                {

                    d = newLandmark.readLandmarkLocation(); //dataset d takes its values from enLandMark object which calls function readlandmark()
                    RadioButtonList1.DataSource = d; //sets dropdown menu datasource
                    RadioButtonList1.DataBind(); //binds data to the dropdown menu
                    RadioButtonList1.DataTextField = "loc_name"; //Select which fields are binded to dropdown menu
                    RadioButtonList1.DataValueField = "loc_id";
                    RadioButtonList1.DataBind();
                }
            }
        }

        //when user clicks button, new landmarkdata is added to database (insert)
        protected void Button1_Click(object sender, EventArgs e)
        {
 //           LandmarkEN newLandmark = new LandmarkEN();
            try
            {
                float i = float.Parse(TextBox4_price.Text); //maybe add try/catch to check that convert works
                newLandmark.Price = i;
            } 
            catch
            {
                newLandmark.Price = 0;
            }
   
            newLandmark.Name = TextBox_name.Text.ToString();
            newLandmark.Address = TextBox2_address.Text.ToString();
            newLandmark.City = RadioButtonList1.SelectedItem.ToString();
            newLandmark.Location_id = int.Parse(RadioButtonList1.SelectedValue); //This value is 1, 2, 3 etc. Id:s of locations.
            newLandmark.Type = RadioButtonList2.SelectedItem.ToString();
            newLandmark.WebsiteLink = TextBox5_website.Text.ToString();


            bool success = newLandmark.CreateLandmark();

            if (success)
            {
                Response.Redirect("~/Pages/Landmark.aspx");
            }

        }

    }
}