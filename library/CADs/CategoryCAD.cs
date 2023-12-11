using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace library
{
    class CategoryCAD
    {
        public string Statusmessage { get; set; }
        private string constring { get; set; }
        public int Id;
        public string Name;
        public CategoryCAD()
        {
            constring = ConfigurationManager.ConnectionStrings["dbconnection"].ToString();
        }
        /// <summary>
        /// Creates a new category in the database
        /// </summary>
        public bool CreateCategory(CategoryEN en)
        {
            try
            {
                DataSet dbVirtual = new DataSet();
                SqlConnection c = new SqlConnection(constring);
                SqlDataAdapter da = new SqlDataAdapter("select * from categories", c);
                da.Fill(dbVirtual, "categories");
                DataTable t = new DataTable();
                t = dbVirtual.Tables["categories"];
                DataRow newCat = t.NewRow();
                newCat["name"] = en.Name;
                t.Rows.Add(newCat);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(da);
                da.Update(dbVirtual, "categories");
                Statusmessage = "Category created successfully.";
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                Statusmessage = e.Message;
                return false;
            }
        }
        /// <summary>
        /// Reads a category from the database
        /// </summary>
        public bool ReadCategory(CategoryEN en)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(constring))
                {
                    connection.Open();
                    using (SqlCommand com = new SqlCommand("SELECT * FROM categories WHERE id = @id", connection))
                    {
                        com.Parameters.AddWithValue("@id", en.Id);
                        using (SqlDataReader dr = com.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                int.TryParse(dr["id"].ToString(), out Id);
                                en.Name = dr["name"].ToString();
                                connection.Close();
                                dr.Close();
                                Statusmessage = "Category read successfully.";
                                return true;
                            }
                            else
                            {
                                Statusmessage = "Category doesnt exist.";
                                connection.Close();
                                dr.Close();
                                return false;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                Statusmessage = e.Message;
                return false;
            }
        }
        /// <summary>
        /// Updates a category from the database
        /// </summary>
        public bool UpdateCategory(CategoryEN en)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(constring))
                {
                    connection.Open();
                    using (SqlCommand com = new SqlCommand("SELECT COUNT(*) FROM locations WHERE id = @id", connection))
                    {
                        com.Parameters.AddWithValue("@id", en.Id);
                        int count = (int)com.ExecuteScalar();
                        if (count == 0)
                        {
                            Statusmessage = "Cant update category that doesnt exist.";
                            connection.Close();
                            return false;
                        }
                        SqlCommand com_update = new SqlCommand("UPDATE categories set name = @name WHERE id = @id", connection);
                        com_update.Parameters.AddWithValue("@id", en.Id);
                        com_update.Parameters.AddWithValue("@name", en.Name);
                        com_update.ExecuteNonQuery();
                        Statusmessage = "Category updated successfully.";
                        connection.Close();
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                Statusmessage = e.Message;
                return false;
            }
        }
        /// <summary>
        /// Deletes a category from the database
        /// </summary>
        public bool DeleteCategory(CategoryEN en)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(constring))
                {
                    connection.Open();
                    using (SqlCommand com = new SqlCommand("SELECT COUNT(*) FROM categories WHERE id = @id", connection))
                    {
                        com.Parameters.AddWithValue("@id", en.Id);
                        int count = (int)com.ExecuteScalar();
                        if (count == 0)
                        {
                            Statusmessage = "Cant delete category that doesnt exist.";
                            connection.Close();
                            return false;
                        }
                        using (SqlCommand com_del = new SqlCommand("DELETE FROM categories where id = @id", connection))
                        {
                            com_del.Parameters.AddWithValue("@id", en.Id);
                            com_del.ExecuteNonQuery();
                            connection.Close();
                            Statusmessage = "Category deleted successfully.";
                            return true;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                Statusmessage = e.Message;
                return false;
            }
        }

        public DataSet ReadAllCategoriesDataSet()
        {
            DataSet dbVirtual = new DataSet();
            SqlConnection c = new SqlConnection(constring);
            SqlDataAdapter da = new SqlDataAdapter("select * from categories", c);
            da.Fill(dbVirtual, "categories");
            return dbVirtual;
        }
    }
 }
