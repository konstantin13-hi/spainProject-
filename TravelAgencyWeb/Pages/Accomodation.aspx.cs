using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using library;


namespace TravelAgencyWeb.Pages
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        private DataSet myDataSet;
        DataSet reviewDS = new DataSet();
        UserEN userEN = new UserEN();
        AccomodationEN accomodationEN = new AccomodationEN();
        string accommodationIdString; //LEENA added this here, so we can pass par1 only one time at the Page_load method and use elsewhere after that
        int accomodation_id;
        FavoriteEN favoriteEN =new FavoriteEN();
        ReviewEN reviews = new ReviewEN(); //LEENA added this, need to get reviews of accommodation

        protected void Page_Load(object sender, EventArgs e)
        {
            userEN = (UserEN)Session["CurrentUser"];
            
            //LEENA check if review button is showed or not at the page
            if (userEN != null) { checkUserHistory(userEN); }
            else { Button1_giveReview.Visible = false; }
            
            //take accommodation id from url as parameter to string and then parse to int
            accommodationIdString = Request.QueryString["par1"];
            accomodation_id = int.Parse(accommodationIdString);

            if (!IsPostBack)
            {
                myDataSet = accomodationEN.ReadAccommodation(accomodation_id);
                hotelsGridView.DataSource = myDataSet; // связываем данные с элементом управления
                hotelsGridView.DataBind();

                //LEENA display review grades to accommodation page
                showReviews();
            }

//            userEN = (UserEN)Session["CurrentUser"]; //LEENA This is already made above, no need to do again
            if (userEN != null)
            {
                bool isAddedToFavorites = CheckIfAddedToFavorites();

                if (isAddedToFavorites)
                {
                    btnAddToFavorites.Text = "Delete from Favorites";
                }
                else
                {
                    btnAddToFavorites.Text = "Add to Favorites";
                }
            }

        }
        //LEENAs method: Show grades of accommodation.
        //Uses readAvgReview method to count and get avg grades this accommodation has got from users
        //If there is no grades yet at the DB, show text that informs about this
        protected void showReviews()
        {
            Label_accId.Visible = false; //This label holds accommodation_id value, so I can filter reviewtexts by using this label
            Label_accId.Text = accommodationIdString;

            reviews.Accommodation_id = accomodation_id;
            reviewDS = reviews.readAvgReview(); //this returns dataset with avg grades of this accommodation
            if (reviewDS.Tables[0].Rows.Count > 0) 
            {
                Label1_avg.Text = "Average: " + reviewDS.Tables["Averages"].Rows[0][0].ToString();
                Label2_overall.Text = "Overall: " + reviewDS.Tables["Averages"].Rows[0][1].ToString();
                Label3_location.Text = "Location: " + reviewDS.Tables["Averages"].Rows[0][2].ToString();
                Label4_cleanliness.Text = "Cleanliness: " + reviewDS.Tables["Averages"].Rows[0][3].ToString();
            }
            else  //If there is no rows
            {
                Label1_avg.Visible = true;
                Label1_avg.ForeColor = System.Drawing.Color.Red;
                Label2_overall.Visible = false;
                Label3_location.Visible = false;
                Label4_cleanliness.Visible = false;
            }
        }

        //LEENAs method: Check if current user has booked this accommodation at some point
        //If yes, displays "Give review" button, if no won't display the button
        protected void checkUserHistory(UserEN user)
        {
            string accommodation_id = Request.QueryString["par1"];
            int accId = int.Parse(accommodation_id);
            ReviewEN rev = new ReviewEN();
            bool history = rev.readAccHistory(user, accId);
            if (history)
            {
                Button1_giveReview.Visible = true;
            } else if (!history)
            {
                Button1_giveReview.Visible = false;
            }
        }

        //Accomodation.asp?id=123.
        protected void addToFavoritesButton_Click(object sender, EventArgs e)
        {
            if (userEN != null)
            {

                bool isAddedToFavorites = CheckIfAddedToFavorites();

                if (isAddedToFavorites)
                {
                    // Логика удаления из избранного
                    favoriteEN.DeleteFavoriteBool(userEN.Id, accomodation_id);
                    btnAddToFavorites.Text = "Add to Favorites";
                }
                else
                {
                    // Логика добавления в избранное
                    favoriteEN.CreateFavoriteBool(userEN.Id, accomodation_id);
                    btnAddToFavorites.Text = "Delete from Favorites";
                }
            }
            else
            {
                Response.Redirect("~/Pages/UserLogin.aspx");
            }
        }

        protected void btnAddToFavorites_Click(object sender, EventArgs e)
        {
            // Проверка, зарегистрирован ли пользователь

            if (userEN != null)
            {

                bool isAddedToFavorites = CheckIfAddedToFavorites();

                if (isAddedToFavorites)
                {
                    // Логика удаления из избранного
                    favoriteEN.DeleteFavoriteBool(userEN.Id, accomodation_id);
                    btnAddToFavorites.Text = "Add to Favorites";
                }
                else
                {
                    // Логика добавления в избранное
                    favoriteEN.CreateFavoriteBool(userEN.Id, accomodation_id);
                    btnAddToFavorites.Text = "Delete from Favorites";
                }
            }
            else
            {

                // Отображение модального окна при неавторизованном пользователе
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "$('#myModal').modal();", true);
            }
           
                // Логика добавления в избранное для авторизованного пользователя
               
            
        }

        protected void btnGoToRegistration_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/UserLogin.aspx");
        }


        protected void ReturnToBrowser(object sender, EventArgs e)
        {
            
            Response.Redirect("~/Pages/AccomodationBrowser.aspx");
        }

        //LEENAs method: On click redirects user to the Give review page and passes this accommodation.Id as parameter in url
        protected void Button1_giveReview_Click(object sender, EventArgs e)
        {
 //           string accommodation_id = Request.QueryString["par1"];
            Response.Redirect("~/Pages/Review.aspx?par1=" + accommodationIdString );
        }
        
        private bool CheckIfAddedToFavorites()
        {
            return favoriteEN.ReadFavoriteBool(userEN.Id,accomodation_id);
        }

        protected void Unnamed4_Click(object sender, EventArgs e)
        {
            if (userEN != null)
            {
                Response.Redirect("~/Pages/UserBooking.aspx?par1=" + accomodation_id);
            }
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myModalSecond", "$('#myModalSecond').modal();", true);
            }
        }
    }
}