using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Configuration;
using System.Linq;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace library
{
    public class AccomodationCAD


    {



        private string constring = ConfigurationManager.ConnectionStrings["dbconnection"].ToString();
        public string ExitMsg { get; set; }
        public string DisplayMsg { get; set; }



        public DataSet CreateAccommodation(AccomodationEN en)
        {
            DataSet dbVirtual = new DataSet();
            SqlConnection c = new SqlConnection(constring);
            SqlDataAdapter da = new SqlDataAdapter("select * from accomodations", c);
            da.Fill(dbVirtual, "accomodations");
            DataTable t = dbVirtual.Tables["accomodations"];

            DataRow newRow = t.NewRow();
            newRow["name"] = en.Name;
            newRow["adress"] = en.Address;
            newRow["area"] = en.Area;
            newRow["hasswimmingpool"] = en.HasSwimmingPool;
            newRow["hasgym"] = en.HasGym;
            newRow["hasparking"] = en.HasParking;
            newRow["ispetfriendly"] = en.IsPetFriendly;
            newRow["description"] = en.Description;
           // newRow["category_id"] = en.categoryEN.Id;
            //newRow["location_id"] = en.locationEN.Id;
           // newRow["seasonal_price_id"] = en.locationEN.Id;

            t.Rows.Add(newRow);

            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(da);
            da.Update(dbVirtual, "accomodations");

            return dbVirtual;
        }

        public DataSet ReadAccommodation(int id_accomodation)
        {
            DataSet dbVirtual = new DataSet();
            SqlConnection c = new SqlConnection(constring);
            SqlDataAdapter da = new SqlDataAdapter("select * from accomodations where id = " + id_accomodation, c);
            da.Fill(dbVirtual, "accomodations");

            return dbVirtual;
        }
        public DataSet ReadAccommodationAll()
        {
            DataSet dbVirtual = new DataSet();
            SqlConnection c = new SqlConnection(constring);
            string
            query = "SELECT a.id, a.name, r.average_rating, a.hasswimmingpool, CASE WHEN  CONVERT(varchar(10), GETDATE(), 120) BETWEEN sp.startDate AND sp.endDate THEN a.price * sp.multiplierPrize ELSE a.price END AS price , a.hasgym, a.ispetfriendly, a.hasparking,l.name as city, l.country ,c.name as category FROM accomodations a left JOIN ( SELECT a2.id, a2.name, a2.price, a2.hasgym, a2.hasswimmingpool, a2.ispetfriendly, a2.hasparking, AVG(r.pointsAvg) AS average_rating FROM accomodations a2 LEFT JOIN reviews r ON a2.id = r.accomodation_id GROUP BY a2.id, a2.name, a2.price, a2.hasgym, a2.hasswimmingpool, a2.ispetfriendly, a2.hasparking ) r ON a.id = r.id left JOIN locations l ON l.id = a.location_id left JOIN seasonal_prices sp ON a.seasonal_price_id = sp.id left join categories c on (c.id=a.category_id)";
           
            SqlDataAdapter da = new SqlDataAdapter(query, c);
            da.Fill(dbVirtual, "accomodations");

            return dbVirtual;
        }
        public DataSet ReadAccommodationAllId(int id_user)
        {
            DataSet dbVirtual = new DataSet();
            SqlConnection c = new SqlConnection(constring);
            string query = "SELECT a.id, a.name, r.average_rating, a.hasswimmingpool, CASE WHEN CONVERT(varchar(10), GETDATE(), 120) BETWEEN sp.startDate AND sp.endDate THEN a.price * sp.multiplierPrize ELSE a.price END AS price, a.hasgym, a.ispetfriendly, a.hasparking, l.name AS city, l.country, c.name AS category FROM accomodations a LEFT JOIN (SELECT a2.id, a2.name, a2.price, a2.hasgym, a2.hasswimmingpool, a2.ispetfriendly, a2.hasparking, AVG(r.pointsAvg) AS average_rating FROM accomodations a2 LEFT JOIN reviews r ON a2.id = r.accomodation_id GROUP BY a2.id, a2.name, a2.price, a2.hasgym, a2.hasswimmingpool, a2.ispetfriendly, a2.hasparking) r ON a.id = r.id LEFT JOIN locations l ON l.id = a.location_id LEFT JOIN seasonal_prices sp ON a.seasonal_price_id = sp.id LEFT JOIN categories c ON (c.id = a.category_id) LEFT JOIN favorites f ON f.accomodation_id = a.id where f.user_id =@userId";
           
            SqlDataAdapter da = new SqlDataAdapter(query, c);
            da.SelectCommand.Parameters.AddWithValue("@userId", id_user);
            da.Fill(dbVirtual, "accomodations");

            return dbVirtual;
        }

        public DataSet FilterAccomodations(DataSet dataSet, string minRating)
        {
            SqlConnection c = new SqlConnection(constring);
            SqlDataAdapter da = new SqlDataAdapter($"SELECT a.id, a.name, AVG(r.pointsAvg) AS average_rating,  a.price FROM accomodations a LEFT JOIN reviews r ON a.id = r.accomodation_id GROUP BY a.id, a.name,  a.price HAVING AVG(r.pointsAvg) >= @minRating", c);
            da.SelectCommand.Parameters.AddWithValue("@minRating", minRating);

            DataSet filteredDataSet = new DataSet();
            da.Fill(filteredDataSet, "accomodations");

            return filteredDataSet;
        }

        public DataSet UpdateAccommodation(AccomodationEN en, int id_accomodation)
        {
            DataSet dbVirtual = new DataSet();
            SqlConnection c = new SqlConnection(constring);
            SqlDataAdapter da = new SqlDataAdapter("select * from accomodations", c);
            da.Fill(dbVirtual, "accomodations");
            DataTable t = dbVirtual.Tables["accomodations"];

            DataRow rowToUpdate = t.Rows[id_accomodation - 1];
            rowToUpdate["name"] = en.Name;
            rowToUpdate["adress"] = en.Address;
            rowToUpdate["area"] = en.Area;
            rowToUpdate["hasswimmingpool"] = en.HasSwimmingPool;
            rowToUpdate["hasgym"] = en.HasGym;
            rowToUpdate["hasparking"] = en.HasParking;
            rowToUpdate["ispetfriendly"] = en.IsPetFriendly;
            rowToUpdate["description"] = en.Description;
            rowToUpdate["category_id"] = en.categoryEN.Id;
            rowToUpdate["location_id"] = en.locationEN.Id;
            rowToUpdate["seasonal_price_id"] = en.locationEN.Id;


            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(da);
            da.Update(dbVirtual, "accomodations");

            return dbVirtual;
        }


        public DataSet DeleteAccommodation(AccomodationEN en, int id_accomodation)
        {
            DataSet dbVirtual = new DataSet();
            SqlConnection c = new SqlConnection(constring);
            SqlDataAdapter da = new SqlDataAdapter("select * from accomodations", c);
            da.Fill(dbVirtual, "accomodations");
            DataTable t = dbVirtual.Tables["accomodations"];

            // Найдите строку с указанным идентификатором и удалите ее
            DataRow rowToDelete = null;
            foreach (DataRow row in t.Rows)
            {
                if (Convert.ToInt32(row["id"]) == id_accomodation)
                {
                    rowToDelete = row;
                    break;
                }
            }
            if (rowToDelete != null)
            {
                t.Rows.Remove(rowToDelete);
            }

            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(da);
            da.Update(dbVirtual, "accomodations");

            return dbVirtual;
        }

        public static List<AccomodationEN> GetAllAccomodations()
        {

            string connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ToString();
            List<AccomodationEN> hotels = new List<AccomodationEN>();


            SqlConnection connection = new SqlConnection(connectionString);
            {
                string query = "SELECT * FROM Accomodations";
                SqlCommand command = new SqlCommand(query, connection);
                try
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        AccomodationEN hotel = new AccomodationEN();
                        hotel.Id = (int)reader["id"];
                        hotel.Name = reader["name"] != DBNull.Value ? (string)reader["name"] : null;
                        hotel.Address = reader["adress"] != DBNull.Value ? (string)reader["adress"] : null;
                        hotel.NumberOfRooms = reader["numberofrooms"] != DBNull.Value ? (int)reader["numberofrooms"] : 0;
                        hotel.Area = reader["area"] != DBNull.Value ? (double)reader["area"] : 0;
                        hotel.Price = reader["price"] != DBNull.Value ? (decimal)reader["price"] : 0;
                        hotel.HasSwimmingPool = reader["hasswimmingpool"] != DBNull.Value ? (bool)reader["hasswimmingpool"] : false;
                        hotel.HasGym = reader["hasgym"] != DBNull.Value ? (bool)reader["hasgym"] : false;
                        hotel.HasParking = reader["hasparking"] != DBNull.Value ? (bool)reader["hasparking"] : false;
                        hotel.IsPetFriendly = reader["ispetfriendly"] != DBNull.Value ? (bool)reader["ispetfriendly"] : false;
                        hotel.Description = reader["description"] != DBNull.Value ? (string)reader["description"] : null;

                        // Assuming CategoryEN, LocationEN, and SeasonalPricingEN have appropriate constructors
                        hotel.categoryEN = new CategoryEN();
                        //hotel.categoryEN.Id = reader["category_id"] != DBNull.Value ? (int)reader["category_id"] : 0;
                        hotel.categoryEN.ReadCategory();

                        hotel.locationEN = new LocationEN();
                        hotel.locationEN.Id = reader["location_id"] != DBNull.Value ? (int)reader["location_id"] : 0;
                        hotel.locationEN.ReadLocation();

                        hotel.seasonalPriceEN = new SeasonalPricingEN();
                        hotel.seasonalPriceEN.Id = reader["seasonal_price_id"] != DBNull.Value ? (int)reader["seasonal_price_id"] : 0;
                        if (!hotel.seasonalPriceEN.readSeasonalPricing())
                        {
                            hotel.seasonalPriceEN = null;
                        }
                        hotels.Add(hotel);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    connection.Close();

                }
            }
            connection.Close();

            return hotels;
        }



        public static List<AccomodationEN> GetSortedAccomodations(string sortOrder)
        {
            List<AccomodationEN> accomodations = GetAllAccomodations();

            switch (sortOrder)
            {
                case "name_asc":
                    accomodations = accomodations.OrderBy(a => a.Name).ToList();
                    break;
                case "name_desc":
                    accomodations = accomodations.OrderByDescending(a => a.Name).ToList();
                    break;
                case "price_asc":
                    accomodations = accomodations.OrderBy(a => a.Price).ToList();
                    break;
                case "price_desc":
                    accomodations = accomodations.OrderByDescending(a => a.Price).ToList();
                    break;
                // добавьте другие варианты сортировки по необходимости
                default:
                    // по умолчанию сортируем по названию отеля в алфавитном порядке
                    accomodations = accomodations.OrderBy(a => a.Name).ToList();
                    break;
            }

            return accomodations;
        }

        // public static  GetBookingPrice(accommodationId)

        public DataSet ReadAllAccomodationsDataSet()
        {
            DataSet dbVirtual = new DataSet();
            SqlConnection c = new SqlConnection(constring);
            SqlDataAdapter da = new SqlDataAdapter("select * from accomodations", c);
            da.Fill(dbVirtual, "accomodations");
            return dbVirtual;
        }


        public DataSet GetFilteredData()
        {

            // Получение данных из базы данных
            string connectionString = constring;
            string query = "SELECT a.id, a.name, r.average_rating, a.hasswimmingpool, CASE WHEN  CONVERT(varchar(10), GETDATE(), 120) BETWEEN sp.startDate AND sp.endDate THEN a.price * sp.multiplierPrize ELSE a.price END AS price , a.hasgym, a.ispetfriendly, a.hasparking,l.name as city, l.country ,c.name as category FROM accomodations a left JOIN ( SELECT a2.id, a2.name, a2.price, a2.hasgym, a2.hasswimmingpool, a2.ispetfriendly, a2.hasparking, AVG(r.pointsAvg) AS average_rating FROM accomodations a2 LEFT JOIN reviews r ON a2.id = r.accomodation_id GROUP BY a2.id, a2.name, a2.price, a2.hasgym, a2.hasswimmingpool, a2.ispetfriendly, a2.hasparking ) r ON a.id = r.id left JOIN locations l ON l.id = a.location_id left JOIN seasonal_prices sp ON a.seasonal_price_id = sp.id left join categories c on (c.id=a.category_id)";

            // Создание подключения к базе данных
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                // Открытие подключения
                connection.Open();

                // Создание адаптера данных и заполнение DataSet
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);

                // Фильтрация данных по выбранному значению фильтра и ценовому диапазону (если указаны)

                connection.Close();
                return dataSet;
            }


        }


        public DataSet GetFilteredDataId(int userId)
        {

            // Получение данных из базы данных
            string connectionString = constring;
            string query = "SELECT a.id, a.name, r.average_rating, a.hasswimmingpool, CASE WHEN CONVERT(varchar(10), GETDATE(), 120) BETWEEN sp.startDate AND sp.endDate THEN a.price * sp.multiplierPrize ELSE a.price END AS price, a.hasgym, a.ispetfriendly, a.hasparking, l.name AS city, l.country, c.name AS category FROM accomodations a LEFT JOIN (SELECT a2.id, a2.name, a2.price, a2.hasgym, a2.hasswimmingpool, a2.ispetfriendly, a2.hasparking, AVG(r.pointsAvg) AS average_rating FROM accomodations a2 LEFT JOIN reviews r ON a2.id = r.accomodation_id GROUP BY a2.id, a2.name, a2.price, a2.hasgym, a2.hasswimmingpool, a2.ispetfriendly, a2.hasparking) r ON a.id = r.id LEFT JOIN locations l ON l.id = a.location_id LEFT JOIN seasonal_prices sp ON a.seasonal_price_id = sp.id LEFT JOIN categories c ON (c.id = a.category_id) LEFT JOIN favorites f ON f.accomodation_id = a.id where f.user_id =@userId";

            // Создание подключения к базе данных
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", userId);
                // Открытие подключения
                connection.Open();

                // Создание адаптера данных и заполнение DataSet
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);

                return dataSet;
            }


        }
        //--------------------------------------------------------------------------------------------------------------------------------------------------

        public bool CreateAccommodationBool(AccomodationEN accommodation)
        {

            string sqlInsert = "INSERT INTO accomodations (name, adress, numberofrooms, area, price, hasswimmingpool, hasgym, hasparking, ispetfriendly, description, category_id, location_id, seasonal_price_id) VALUES (@Name, @Address, @NumberOfRooms, @Area, @Price, @HasSwimmingPool, @HasGym, @HasParking, @IsPetFriendly, @Description, @CategoryEN, @LocationEN, @SeasonalPriceEN)";

            using (SqlConnection c = new SqlConnection(constring))
            {
                try
                {
                    c.Open();
                    SqlCommand com = new SqlCommand(sqlInsert, c);
                    com.Parameters.AddWithValue("@Name", accommodation.Name);
                    com.Parameters.AddWithValue("@Address", accommodation.Address);
                    com.Parameters.AddWithValue("@NumberOfRooms", accommodation.NumberOfRooms);
                    com.Parameters.AddWithValue("@Area", accommodation.Area);
                    com.Parameters.AddWithValue("@Price", accommodation.Price);
                    com.Parameters.AddWithValue("@HasSwimmingPool", accommodation.HasSwimmingPool);
                    com.Parameters.AddWithValue("@HasGym", accommodation.HasGym);
                    com.Parameters.AddWithValue("@HasParking", accommodation.HasParking);
                    com.Parameters.AddWithValue("@IsPetFriendly", accommodation.IsPetFriendly);
                    com.Parameters.AddWithValue("@Description", accommodation.Description);
                    com.Parameters.AddWithValue("@CategoryEN", accommodation.categoryEN.Id);
                    com.Parameters.AddWithValue("@LocationEN", accommodation.locationEN.Id);
                    com.Parameters.AddWithValue("@SeasonalPriceEN", accommodation.seasonalPriceEN.Id);

                    com.ExecuteNonQuery();

                    ExitMsg = "Accommodation created successfully.";
                    c.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    c.Close();
                    ExitMsg = $"Error creating accommodation: {ex.Message}";
                    return false;
                }
            }





        }

        public bool ReadAccommodationBool(AccomodationEN accommodation)
        {
            string sqlSelect = "SELECT * FROM accomodations WHERE id = @your_id";

            using (SqlConnection c = new SqlConnection(constring))
            {
                try
                {
                    c.Open();
                    SqlCommand com = new SqlCommand(sqlSelect, c);
                    com.Parameters.AddWithValue("@your_id", accommodation.Id);

                    SqlDataReader reader = com.ExecuteReader();

                    if (reader.Read())
                    {
                        // Заполнение объекта AccomodationEN данными из результата запроса
                        accommodation.Name = reader["name"].ToString();
                        accommodation.Address = reader["adress"].ToString();
                        accommodation.NumberOfRooms = Convert.ToInt32(reader["numberofrooms"]);
                        accommodation.Area = Convert.ToDouble(reader["area"]);
                        accommodation.Price = Convert.ToDecimal(reader["price"]);
                        accommodation.HasSwimmingPool = Convert.ToBoolean(reader["hasswimmingpool"]);
                        accommodation.HasGym = Convert.ToBoolean(reader["hasgym"]);
                        accommodation.HasParking = Convert.ToBoolean(reader["hasparking"]);
                        accommodation.IsPetFriendly = Convert.ToBoolean(reader["ispetfriendly"]);
                        accommodation.Description = reader["description"].ToString();

                        CategoryEN category = new CategoryEN();
                        category.Id = Convert.ToInt32(reader["category_id"]);
                        category.ReadCategory();
                        accommodation.categoryEN = category;
                        LocationEN location = new LocationEN();
                        location.Id = Convert.ToInt32(reader["location_id"]);
                        location.ReadLocation();
                        accommodation.locationEN = location;
                        SeasonalPricingEN seasonalPrice = new SeasonalPricingEN();
                        seasonalPrice.Id = Convert.ToInt32(reader["seasonal_price_id"]);
                        seasonalPrice.readSeasonalPricing();
                        accommodation.seasonalPriceEN = seasonalPrice;

                        reader.Close();
                        c.Close();
                        return true;
                    }
                    else
                    {
                        // Отель с указанным ID не найден
                        reader.Close();
                        c.Close();
                        ExitMsg = "Accommodation not found.";
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    c.Close();
                    ExitMsg = $"Read error: {ex.Message}";
                    return false;
                }
            }


        }

        public bool UpdateAccommodationBool(AccomodationEN accommodation)
        {
            
            string sqlUpdate = "UPDATE accomodations SET name = @Name, adress = @Address, numberofrooms = @NumberOfRooms, area = @Area, price = @Price, hasswimmingpool = @HasSwimmingPool, hasgym = @HasGym, hasparking = @HasParking, ispetfriendly = @IsPetFriendly, description = @Description, category_id = @CategoryEN, location_id = @LocationEN, seasonal_price_id = @SeasonalPriceEN WHERE Id = @Id";

            using (SqlConnection c = new SqlConnection(constring))
            {
                try
                {
                    c.Open();
                    SqlCommand com = new SqlCommand(sqlUpdate, c);
                    com.Parameters.AddWithValue("@Name", accommodation.Name);
                    com.Parameters.AddWithValue("@Address", accommodation.Address);
                    com.Parameters.AddWithValue("@NumberOfRooms", accommodation.NumberOfRooms);
                    com.Parameters.AddWithValue("@Area", accommodation.Area);
                    com.Parameters.AddWithValue("@Price", accommodation.Price);
                    com.Parameters.AddWithValue("@HasSwimmingPool", accommodation.HasSwimmingPool);
                    com.Parameters.AddWithValue("@HasGym", accommodation.HasGym);
                    com.Parameters.AddWithValue("@HasParking", accommodation.HasParking);
                    com.Parameters.AddWithValue("@IsPetFriendly", accommodation.IsPetFriendly);
                    com.Parameters.AddWithValue("@Description", accommodation.Description);
                    com.Parameters.AddWithValue("@CategoryEN", accommodation.categoryEN.Id);
                    com.Parameters.AddWithValue("@LocationEN", accommodation.locationEN.Id);
                    com.Parameters.AddWithValue("@SeasonalPriceEN", accommodation.seasonalPriceEN.Id);
                    com.Parameters.AddWithValue("@Id", accommodation.Id);

                    int rowsAffected = com.ExecuteNonQuery();
                    c.Close();

                    if (rowsAffected > 0)
                    {
                        ExitMsg = "Accommodation updated successfully.";
                        return true;
                    }
                    else
                    {
                        ExitMsg = "Accommodation not found or update operation failed.";
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    c.Close();
                    ExitMsg = $"Error updating accommodation: {ex.Message}";
                    return false;
                }
            }
        }

        public bool DeleteAccommodationBool(AccomodationEN accommodation)
        {
            string sqlDelete = "DELETE FROM accomodations WHERE id = @Id";

            using (SqlConnection c = new SqlConnection(constring))
            {
                try
                {
                    c.Open();
                    SqlCommand com = new SqlCommand(sqlDelete, c);
                    com.Parameters.AddWithValue("@Id", accommodation.Id);
                    com.ExecuteNonQuery(); 
                    c.Close();

                    ExitMsg = "Accommodation deleted successfully.";
                    return true;
                }
                catch (Exception ex)
                {
                    c.Close();
                    ExitMsg = $"Error deleting accommodation: {ex.Message}";
                    return false;
                }
            }
        }


        public DataTable GetCategoriesFromDatabase()
        {
            

            string connectionString =constring;
            string query = "SELECT id, name FROM categories";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return dataTable;
            }
        }

        public DataSet GetMostReviewed(int num)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(constring))
                {
                    string sqlQuery = @"
                    SELECT TOP (@num) a.id, a.name, COUNT(r.id) AS reviewCount
                    FROM accomodations a
                    LEFT JOIN reviews r ON a.id = r.accomodation_id
                    GROUP BY a.id, a.name
                    ORDER BY reviewCount DESC";
                    SqlCommand command = new SqlCommand(sqlQuery, connection);
                    command.Parameters.AddWithValue("@num", num);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataSet dataset = new DataSet();
                    adapter.Fill(dataset, "MostReviewed");
                    return dataset;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                ExitMsg = e.Message;
                return null;
            }

        }

        public DataSet GetHighestRanked(int num)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(constring))
                {
                    string sqlQuery = @"
                    SELECT TOP (@num) a.id, a.name, AVG(r.pointsOverall) AS averagePointsOverall
                    FROM accomodations a
                    JOIN reviews r ON r.accomodation_id = a.id
                    GROUP BY a.id, a.name
                    ORDER BY averagePointsOverall DESC";
                    SqlCommand command = new SqlCommand(sqlQuery, connection);
                    command.Parameters.AddWithValue("@num", num);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataSet dataset = new DataSet();
                    adapter.Fill(dataset, "HighestRanked");
                    return dataset;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                ExitMsg = e.Message;
                return null;
            }

        }

        public DataSet GetMostFavorited(int num)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(constring))
                {
                    string sqlQuery = @"
                    SELECT TOP (@num) a.id, a.name, COUNT(f.accomodation_id) AS FavoriteCount
                    FROM accomodations a
                    JOIN favorites f ON a.id = f.accomodation_id
                    GROUP BY a.id, a.name
                    ORDER BY FavoriteCount DESC;
                    ";
                    SqlCommand command = new SqlCommand(sqlQuery, connection);
                    command.Parameters.AddWithValue("@num", num);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataSet dataset = new DataSet();
                    adapter.Fill(dataset, "MostFavorited");
                    return dataset;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                ExitMsg = e.Message;
                return null;
            }

        }
        public List<string> GetCities(string prefix)
        {
            List<string> cities = new List<string>();

            // Создание подключения к базе данных
            using (SqlConnection connection = new SqlConnection(constring))
            {
                string query = "SELECT DISTINCT name FROM locations WHERE name LIKE @prefix + '%'";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@prefix", prefix);

                // Открытие подключения
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string city = reader.GetString(0);
                        cities.Add(city);
                    }
                }
            }

            return cities;
        }

        
        public List<string> GetCountries(string prefix)
        {
            List<string> countries = new List<string>();

            // Создание подключения к базе данных
            using (SqlConnection connection = new SqlConnection(constring))
            {
                string query = "SELECT DISTINCT country FROM locations WHERE country LIKE @prefix + '%'";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@prefix", prefix);

                // Открытие подключения
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string country = reader.GetString(0);
                        countries.Add(country);
                    }
                }
            }

            return countries;
        }
    }



}
