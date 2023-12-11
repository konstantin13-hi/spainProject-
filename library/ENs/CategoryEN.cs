using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace library
{
   public class CategoryEN
    {
        CategoryCAD cad = new CategoryCAD();
        public string Statusmessage { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public CategoryEN() { }

        public CategoryEN(int id, string name)
        {
            Id = id;
            Name = name;
        }
        /// <summary>
        /// Creates a new category
        /// </summary>
        public bool CreateCategory()
        {
            bool status = cad.CreateCategory(this);
            Statusmessage = cad.Statusmessage;
            return status;
        }
        /// <summary>
        /// Reads category
        /// </summary>
        public bool ReadCategory()
        {
            bool status = cad.ReadCategory(this);
            Statusmessage = cad.Statusmessage;
            return status;
        }
        /// <summary>
        /// Updates category
        /// </summary>
        public bool UpdateCategory()
        {
            bool status = cad.UpdateCategory(this);
            Statusmessage = cad.Statusmessage;
            return status;
        }
        /// <summary>
        /// Deletes category
        /// </summary>
        public bool DeleteCategory()
        {
            bool status = cad.DeleteCategory(this);
            Statusmessage = cad.Statusmessage;
            return status;
        }

        public DataSet ReadAllCategoriesDataSet()
        {
            return cad.ReadAllCategoriesDataSet();
        }
    }
}
