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
    public partial class UserFavorites : System.Web.UI.Page
    {
        
        private DataSet myDataSet;
        private DataSet set;
        UserEN userEN = new UserEN();
        AccomodationEN accomodationEN = new AccomodationEN();
        int accomodation_id;
        FavoriteEN favoriteEN = new FavoriteEN();
        protected void Page_Load(object sender, EventArgs e)
        {
            userEN = (UserEN)Session["CurrentUser"];
            if (!IsPostBack)
            {
                AccomodationEN accomodationEN = new AccomodationEN();
                // Загружаем данные из базы данных и привязываем к DropDownList
                DataTable categories = accomodationEN.GetCategoriesFromDatabase();

                // Создаем новую строку для элемента "All Categories"
                DataRow allRow = categories.NewRow();
                allRow["id"] = 0; // Используем значение 0 для элемента "All Categories"
                allRow["name"] = "All Categories";
                categories.Rows.InsertAt(allRow, 0);

                ddlCategory.DataSource = categories;
                ddlCategory.DataTextField = "name";
                ddlCategory.DataValueField = "name"; ;
                ddlCategory.DataBind();

                // Устанавливаем выбранный элемент по умолчанию
                ddlCategory.SelectedIndex = 0;

                myDataSet = accomodationEN.ReadAccommodationAllId(userEN.Id);
                hotelsGridView.DataSource = myDataSet; // связываем данные с элементом управления
                hotelsGridView.DataBind(); // отображаем данные
            }


        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            updatingPage();

        }

        protected void CheckBoxList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            updatingPage();
        }



        protected void Unnamed1_SelectedIndexChanged(object sender, EventArgs e)
        {
            updatingPage();

        }



        protected void hotelsGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            int accomodation = int.Parse(hotelsGridView.SelectedRow.Cells[1].Text);
            Response.Redirect("~/Pages/Accomodation.aspx?par1=" + accomodation);
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Button btnDelete = (Button)sender;
            int hotelId = Convert.ToInt32(btnDelete.CommandArgument);
            favoriteEN.DeleteFavoriteBool(userEN.Id, hotelId);
            updatingPage();


            // Ваша логика удаления отеля на основе идентификатора
            // ...
        }



        protected void TextBoxMinPrice_TextChanged(object sender, EventArgs e)
        {
            updatingPage();
        }

        protected void TextBoxMaxPrice_TextChanged(object sender, EventArgs e)
        {
            updatingPage();
        }

        protected void ButtonFilter_Click(object sender, EventArgs e)
        {
            updatingPage();
        }

       

        protected void txtCity_TextChanged(object sender, EventArgs e)
        {
            updatingPage();
        }

        protected void txtCountry_TextChanged(object sender, EventArgs e)
        {
            updatingPage();
        }


        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)


        {
            updatingPage();

        }
        private void updatingPage()
        {
            AccomodationEN accomodationEN = new AccomodationEN();
            myDataSet = accomodationEN.GetFilteredDataId(userEN.Id);
            DataView view = new DataView(myDataSet.Tables[0]);

            string filterExpression = "";
            // Обработчик события выбора элемента
            // Здесь вы можете выполнить нужные действия, основываясь на выбранной категории
            string selectedCategoryName = ddlCategory.SelectedValue;
            if (selectedCategoryName != "All Categories")
            {
                filterExpression += $"category = '{selectedCategoryName}'";
            }


            view.RowFilter = filterExpression;
            filterExpression = "";
            myDataSet.Tables.Clear();
            myDataSet.Tables.Add(view.ToTable());
            view = new DataView(myDataSet.Tables[0]);


            string filter = RadioButtonList1.SelectedValue;

            if (!string.IsNullOrEmpty(filter))
            {
                if (filter == "0")
                {
                    filterExpression = $"average_rating >= {filter} OR average_rating IS NULL";
                }
                else
                {
                    filterExpression = $"average_rating >= {filter}";
                }
                view.RowFilter = filterExpression;
                filterExpression = "";
                myDataSet.Tables.Clear();
                myDataSet.Tables.Add(view.ToTable());
                view = new DataView(myDataSet.Tables[0]);
            }
            double? minPrice = null;
            double? maxPrice = null;
            if (!string.IsNullOrEmpty(TextBoxMinPrice.Text))
            {
                minPrice = Convert.ToDouble(TextBoxMinPrice.Text);
            }

            if (!string.IsNullOrEmpty(TextBoxMaxPrice.Text))
            {
                maxPrice = Convert.ToDouble(TextBoxMaxPrice.Text);
            }

            if (minPrice.HasValue)
            {
                if (!string.IsNullOrEmpty(filterExpression))
                    filterExpression += " AND ";

                filterExpression += $"price >= {minPrice.Value}";
            }

            if (maxPrice.HasValue)
            {
                if (!string.IsNullOrEmpty(filterExpression))
                    filterExpression += " AND ";

                filterExpression += $"price <= {maxPrice.Value}";
            }
            view.RowFilter = filterExpression;
            filterExpression = "";
            myDataSet.Tables.Clear();
            myDataSet.Tables.Add(view.ToTable());
            view = new DataView(myDataSet.Tables[0]);

            List<string> selectedAmenities = new List<string>();

            foreach (ListItem item in CheckBoxList1.Items)
            {
                if (item.Selected)
                {
                    selectedAmenities.Add(item.Value);
                }
            }

            if (selectedAmenities != null && selectedAmenities.Count > 0)
            {

                for (int i = 0; i < selectedAmenities.Count; i++)
                {
                    if (i > 0)
                        filterExpression += " AND ";

                    filterExpression += $"{selectedAmenities[i]} = True";
                }
                view.RowFilter = filterExpression;
                filterExpression = "";
                myDataSet.Tables.Clear();
                myDataSet.Tables.Add(view.ToTable());
                view = new DataView(myDataSet.Tables[0]);
            }



            string city = txtCity.Text;
            string country = txtCountry.Text;




            if (!string.IsNullOrEmpty(city))
            {
                if (!string.IsNullOrEmpty(filterExpression))
                    filterExpression += " AND ";

                filterExpression += $"city = '{city}'";
            }

            if (!string.IsNullOrEmpty(country))
            {
                if (!string.IsNullOrEmpty(filterExpression))
                    filterExpression += " AND ";

                filterExpression += $"country = '{country}'";
            }
            view.RowFilter = filterExpression;
            filterExpression = "";
            myDataSet.Tables.Clear();
            myDataSet.Tables.Add(view.ToTable());
           

            string sortOrder = DropDownList1.SelectedValue;

            if (sortOrder == "price increasing")
            {
                view.Sort = "price ASC"; // Сортировка по столбцу "price" в порядке возрастания
            }
            else if (sortOrder == "price decreasing")
            {
                view.Sort = "price DESC"; // Сортировка по столбцу "price" в порядке убывания
            }
            else if (sortOrder == "star ranking")
            {
                view.Sort = "average_rating DESC"; // Сортировка по столбцу "average_rating" в порядке убывания
            }

            myDataSet.Tables.Clear();
            myDataSet.Tables.Add(view.ToTable());
            // Здесь должна быть логика фильтрации данных
            hotelsGridView.DataSource = myDataSet; // связываем данные с элементом управления
            hotelsGridView.DataBind(); // отображаем данные

            UpdatePanel1.Update();

        }



    }

}