using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;


namespace library
{
    public class LandmarkEN
    {
        private LandmarkCAD cad = new LandmarkCAD();
 //       private LocationCAD locCad = new LocationCAD();

        public int Id;

        public string Name { get; set; }
        public float Price { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int Location_id { get; set; }
        public string Type { get; set; }
        public string WebsiteLink { get; set; }
        public string StatusMsg { get; set; }
        public LandmarkEN() { }

        //LandmarkEN constructor
        public LandmarkEN(string name, float price, string address, string city, string type, string websiteLink)
        {
            Name = name;
            Price = price;
            Address = address;
            City = city;
            Type = type;
            WebsiteLink = websiteLink;
        }

      //Function to create new connection to database and read landmark info to dataset
        public DataSet readLandmarkLocation() //public DataSet readLandmark()
        { 
            DataSet ds = cad.readLandmarkLocation();

            return ds;
        }

        //Passes LandmarkEN object to CAD layer method
        public bool CreateLandmark()
        {
            bool success = cad.CreateLandmark(this);
         
            return success;
        }

        //Only admin method, passes LandmarkEN object to CAD layer
        public bool UpdateLandmark()
        {
            bool success = cad.UpdateLandmark(this);
           
            return success; 
        }

        //Only admin method, passes LandmarkEN object to CAD layer
        public bool DeleteLandmark()
        {
            bool success = cad.DeleteLandmark(this);

            return success; 
        }

        public bool ReadCurrentLandmark()
        {
            bool success = cad.ReadCurrentLandmark(this);

            return success;
        }

        public DataSet ReadAllLandmarksDataSet()
        {
            return cad.ReadAllLandmarksDataSet();
        }
    }
}
