using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;


namespace library
{
    public class UserCAD
    {
        private string constring { get; set; }
        public string ExitMsg;

        public UserCAD()
        {
            // Set connection to database
            constring = ConfigurationManager.ConnectionStrings["dbconnection"].ToString();
        }

        // Creates a new user in the DB
        public bool CreateUser(UserEN en)
        {
            try
            {
                DataSet dbVirtual = new DataSet();
                SqlConnection c = new SqlConnection(constring);
                SqlDataAdapter da = new SqlDataAdapter("select * from users", c);
                da.Fill(dbVirtual, "users");
                DataTable t = new DataTable();
                t = dbVirtual.Tables["users"];
                DataRow newUser = t.NewRow();
                newUser["email"] = en.Email;
                newUser["password"] = en.Password;
                newUser["name"] = en.Name;
                newUser["isadmin"] = en.IsAdmin;
                newUser["created_at"] = DateTime.Now;
                newUser["updated_at"] = DateTime.Now;

                t.Rows.Add(newUser);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(da);
                da.Update(dbVirtual, "users");
                ExitMsg = "User successfully created";

                return true;
            }
            catch (Exception ex)
            {
                ExitMsg = ex.Message.ToString();
                Console.WriteLine($"Operation failed. Error: {ExitMsg}");
                return false;
            }
        }


        // Reads user from DataBase
        public bool ReadUser(UserEN en)
        {
            string sqlSelect = "select * from users where id=@id";
            SqlConnection c = new SqlConnection(constring);
            SqlDataReader dr;
            try
            {
                c.Open();
                SqlCommand com = new SqlCommand(sqlSelect, c);
                com.Parameters.AddWithValue("@id", en.Id);
                dr = com.ExecuteReader();

                if (!dr.Read()) // Checks if the user exists
                {
                    ExitMsg = $"User with id {en.Id} does not exist.";
                    Console.WriteLine($"Operation failed. Error: {ExitMsg}");
                    dr.Close();
                    c.Close();
                    return false;
                }
                en.Name = dr["name"].ToString();
                en.Email = dr["email"].ToString();
                en.IsAdmin = bool.Parse(dr["isAdmin"].ToString());
                en.Password = dr["password"].ToString();
                dr.Close();
                ExitMsg = "User successfully read.";
                c.Close();
                return true;
            }
            catch (Exception ex)
            {
                ExitMsg = ex.Message.ToString();
                Console.WriteLine($"Operation failed. Error: {ExitMsg}");
                c.Close();
                return false;
            }
        }

        // Updates values of the user in the DB
        public bool UpdateUser(UserEN en)
        {
            // Checks if the user exists
            if (!Exists(en))
            {
                return false;
            }

            string sqlUpdate = "update users set email=@email, password=@password, name=@name, isadmin=@isadmin, updated_at=@updated_at where id=@id";
            
            SqlConnection c = new SqlConnection(constring);
            try
            {
                c.Open();
                SqlCommand com = new SqlCommand(sqlUpdate, c);
                com.Parameters.AddWithValue("@id", en.Id);
                com.Parameters.AddWithValue("@email", en.Email);
                com.Parameters.AddWithValue("@password", en.Password);
                com.Parameters.AddWithValue("@name", en.Name);
                com.Parameters.AddWithValue("@isadmin", en.IsAdmin);
                com.Parameters.AddWithValue("@updated_at", DateTime.Now);
                com.ExecuteNonQuery();
                c.Close();
                ExitMsg = "User successfully updated";
                return true;
            }
            catch (Exception ex)
            {
                ExitMsg = ex.Message.ToString();
                Console.WriteLine($"Operation failed. Error: {ExitMsg}");
                c.Close();
                return false;
            }
        }

        // Deletes user from database
        public bool DeleteUser(UserEN en)
        {
            // Check if user exists
            if (!Exists(en))
            {
                return false;
            }

            string sqlDelete = "delete from users where id=@id";
            SqlConnection c = new SqlConnection(constring);
            try
            {
                c.Open();
                SqlCommand com = new SqlCommand(sqlDelete, c);
                com.Parameters.AddWithValue("@id", en.Id);
                com.ExecuteNonQuery();
                c.Close();
                ExitMsg = "User successfully deleted.";
                return true;
            }
            catch (Exception ex)
            {
                ExitMsg = ex.Message.ToString();
                Console.WriteLine($"Operation has failed. Error: {ExitMsg}");
                c.Close();
                return false;
            }
        }

        // Returns a dataset with all users
        public DataSet ReadAllUsersDataSet()
        {
            DataSet dbVirtual = new DataSet();
            SqlConnection c = new SqlConnection(constring);
            SqlDataAdapter da = new SqlDataAdapter("select * from users", c);
            da.Fill(dbVirtual, "users");
            return dbVirtual;
        }

        // Checks if the user exist in the Database
        private bool Exists(UserEN en)
        {
            string sqlSelect = "select id from users where id=@id";
            SqlConnection c = new SqlConnection(constring);
            SqlDataReader dr;
            try
            {
                c.Open();
                SqlCommand com = new SqlCommand(sqlSelect, c);
                com.Parameters.AddWithValue("@id", en.Id);
                dr = com.ExecuteReader();
                if (!dr.Read())
                {
                    ExitMsg = $"User with id {en.Id} does not exist.";
                    Console.WriteLine($"Operation failed. Error: {ExitMsg}");
                    dr.Close();
                    c.Close();
                    return false;
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                ExitMsg = ex.Message.ToString();
                Console.WriteLine($"Operation failed. Error: {ExitMsg}");
                c.Close();
                return false;
            }
            c.Close();
            return true;
        }

        // Gets the Id from the Email of a user
        public bool GetIdFromEmail(UserEN en)
        {
            string sqlSelect = "select id from users where email=@email";
            SqlConnection c = new SqlConnection(constring);
            SqlDataReader dr;
            try
            {
                c.Open();
                SqlCommand com = new SqlCommand(sqlSelect, c);
                com.Parameters.AddWithValue("@email", en.Email);
                dr = com.ExecuteReader();

                if (!dr.Read()) // Checks if the user with email exists
                {
                    ExitMsg = $"User with email {en.Email} does not exist.";
                    Console.WriteLine($"Operation failed. Error: {ExitMsg}");
                    dr.Close();
                    c.Close();
                    return false;
                }

                en.Id = int.Parse(dr["id"].ToString());
                dr.Close();

            }
            catch (Exception ex)
            {
                ExitMsg = ex.Message.ToString();
                Console.WriteLine($"Operation failed. Error: {ExitMsg}");
                c.Close();
                return false;
            }
            c.Close();
            return true;
        }
    }
}
