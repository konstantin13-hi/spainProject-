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
    public partial class WebForm1 : System.Web.UI.Page
    {
        LandmarkEN enLandMark = new LandmarkEN();
        DataSet d = new DataSet();
        UserEN user;
        

        //On page load checks if there is logged in user in the Session object
        //If yes, show Add Landark -button, if not dont show it
        protected void Page_Load(object sender, EventArgs e)
        {
            
            user = (UserEN)Session["CurrentUser"];
            if (user != null)
            {
                Button_addLandmarks.Visible = true;
            }
            else
            {
                Button_addLandmarks.Visible = false;
            }
            //If page isn't postpacked the dropdown menu gets its values from database using readLandmark -method
            //cityname is showed
            if (!Page.IsPostBack)
            {

                d = enLandMark.readLandmarkLocation(); //dataset d takes its values from enLandMark object which calls function readlandmark()
                DropDownList_Landmark.DataSource = d; //sets dropdown menu datasource
                DropDownList_Landmark.DataBind(); //binds data to the dropdown menu
                DropDownList_Landmark.DataTextField = "loc_name"; //Select which fields are binded to dropdown menu
                DropDownList_Landmark.DataValueField = "loc_id";
                DropDownList_Landmark.DataBind(); 

            }
        }

        ///When button is clicked, reads landmarks of chosen city to the gridview
        ///Done using gridview wizard
        protected void Button1_Click(object sender, EventArgs e)
        {

        }
      
        //When the addlandmark button is clicked the page redirects to Addlandmark-page
        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddLandmark.aspx");
        }

    }
}
