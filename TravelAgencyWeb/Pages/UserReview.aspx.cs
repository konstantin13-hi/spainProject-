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
    public partial class WebForm6 : System.Web.UI.Page
    {
        ReviewEN review = new ReviewEN();
        DataSet d = new DataSet();
        UserEN user = new UserEN();

        //On page load, initialises current user and check that there is one
        protected void Page_Load(object sender, EventArgs e)
        {
            user = (UserEN)Session["CurrentUser"];

            if (user == null)
            {
                Response.Redirect("~/Pages/Home.aspx");
            }
            else
            {
                bindGridView();
            }
        }

        //bind users reviews from database to the gridview
        public void bindGridView()
        {
            review.User_id = user.Id;
            d = review.readReview();
            GridView1.DataSource = d;
            GridView1.DataBind();
        }

        //Updates selected review
        protected void update(object sender, EventArgs e)
        {
            //review.User_id is setted at the page load
 //           review.Id = GridView1.SelectedIndex + 1;
            review.Id = int.Parse(GridView1.SelectedRow.Cells[1].Text); //In this context when user can give 1 review per accommodation, this can be used as key
            review.OverallPoints = int.Parse(Overall_points.Text);
            review.AreaPoints = int.Parse(Area_points.Text);
            review.TidinessPoints = int.Parse(Tidiness_points.Text);
            review.AveragePoints = (review.OverallPoints + review.AreaPoints + review.TidinessPoints) / 3;
            review.Review = WrittenReview.Text;

            bool updated = review.updateReview();
            bindGridView();

            if (updated)
            {
                PanelReviews.Visible = false;
            } else
            {
                MessageLabel.Visible = true;
                MessageLabel.Text = "Something went wrong!";
            }
        }

        //Deletes selected review
        protected void delete(object sender, EventArgs e)
        {
            review.Id = int.Parse(GridView1.SelectedRow.Cells[1].Text);

            bool deleted = review.deleteReview();
            bindGridView();
            if (deleted) { PanelReviews.Visible = false; }
            else 
            {
                MessageLabel.Visible = true;
                MessageLabel.Text = "Something went wrong!";
            }
        }

        //Button to redirect user to the give review page
        protected void give_review(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Review.aspx?par1=0");
        }

        //gridview select -button. When some row is selected, panel with labels and boxes is initialised and displayed to the user
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            PanelReviews.Visible = true;
            name.Text = GridView1.SelectedRow.Cells[2].Text;
            avgPoints.Text = GridView1.SelectedRow.Cells[4].Text;
            Overall_points.Text = GridView1.SelectedRow.Cells[5].Text;
            Area_points.Text = GridView1.SelectedRow.Cells[6].Text;
            Tidiness_points.Text = GridView1.SelectedRow.Cells[7].Text;
            WrittenReview.Text = GridView1.SelectedRow.Cells[8].Text;
            
        }
    }
}