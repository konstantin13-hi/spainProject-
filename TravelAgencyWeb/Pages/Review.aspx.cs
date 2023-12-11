using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using library;

namespace TravelAgencyWeb.Pages
{
    //This page is only for logged in users (and admins). If user can't be found from Session object, user is returned to Home-page
    public partial class Review : System.Web.UI.Page
    {
        //create new review object, dataset and currentUser object.
        ReviewEN newReview = new ReviewEN();
        DataSet d = new DataSet();
        UserEN user = new UserEN();

        //When page loads: take accommmodation.id parameter from url and currentUser id and add those to newReview object 
        protected void Page_Load(object sender, EventArgs e)
        {
            string id_as_string = Request.QueryString["par1"];
            int accommodation_id = int.Parse(id_as_string);
            user = (UserEN)Session["CurrentUser"];
            
            if (user == null)
            {
                Response.Redirect("~/Pages/Home.aspx");
            }
            else if (user.IsAdmin)
            {
                Response.Redirect("~/Admin Pages/AdminUser.aspx");
            }
            else
            {
                newReview.Accommodation_id = accommodation_id;
                newReview.User_id = user.Id;

                //If page isn't postbacked and user comes to the page from user menu (default accommodation_id=0) load accommodations from database
                if (!Page.IsPostBack && accommodation_id == 0)
                {
                    displayUnboundList();
                }
                else if (!Page.IsPostBack && accommodation_id > 0) //if there is value > 0 at the url parameter, show label 
                {
                    displayLabel();
                }   
            }
        }

        //read from database all accommodations user has booked but not yet reviewed, bind to the radiobuttonlist
        protected void displayUnboundList()
        {
            AccommodationList.Visible = true;
            Cue_text.Visible = true;

            d = newReview.readNonReviewed(); //dataset d takes its values 

            if (d.Tables[0].Rows.Count < 1) //if there is no data 
            {
                Cue_text.Text = "There is no accommodation you could review!";
            } else
            {
                AccommodationList.DataSource = d; //sets dropdown menu datasource 
                AccommodationList.DataTextField = "name"; //Select which fields are binded to dropdown menu
                AccommodationList.DataValueField = "id";
                AccommodationList.DataBind();
            }
        }

        //Show label where is the name of the accommodation user is going to review
        protected void displayLabel()
        {
            AccommodationList.Visible = false;
            Cue_text.Visible = false;
            Label_name.Visible = true;
            string name = newReview.readAccommodation();
            if(name == "")
            {
                Label_name.Text = "You have already reviewed this accommodation.";
            } else {
                Label_name.Text = "You want to review " + name;
            }
            
        }

        //Method to insert new review to database
        protected void Button1_Click(object sender, EventArgs e)
        {
            //When clicks button: checks that user has given all the gradings (all mandatory)
            //and written not-mandatory review text, then inserts review to database Review table
            if (Page.IsValid)
            {
                if (newReview.Accommodation_id==0) //If user came here from menu, not from accommodation page
                {
                    newReview.Accommodation_id = int.Parse(AccommodationList.SelectedValue);
                }
                newReview.OverallPoints = float.Parse(TextBox_overall.Text);
                newReview.TidinessPoints = float.Parse(TextBox_tidiness.Text);
                newReview.AreaPoints = float.Parse(TextBox_area.Text);
                newReview.Review = TextBox_writtenReview.Text.ToString();
                newReview.AveragePoints = (newReview.OverallPoints + newReview.AreaPoints + newReview.TidinessPoints) / 3;

                bool added = newReview.createReview();
                if (added)
                {
                    Label_name.Visible = false;
                    Label1.Text = "Review given!";
                    SeeRevs.Visible = true;
                } else
                {
                    Label1.Text = "Something went wrong.";
                }
                displayUnboundList();   
            }

        }

        //Method to redirect user to the review managing page
        protected void SeeRevs_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserReview.aspx");
        }
    }
}