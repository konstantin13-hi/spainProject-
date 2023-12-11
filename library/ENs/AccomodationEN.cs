using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace library
{

    public class AccomodationEN
    {
        private AccomodationCAD cad = new AccomodationCAD();
        public string ExitMsg { get; set; }

        public string Name { get; set; }
        public string Address { get; set; }
        public int NumberOfRooms { get; set; }
        public double Area { get; set; }
        public decimal Price { get; set; }
        public bool HasSwimmingPool { get; set; }
        public bool HasGym { get; set; }
        public bool HasParking { get; set; }
        public bool IsPetFriendly { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
        public CategoryEN categoryEN { get; set; }
        public LocationEN locationEN { get; set; }
        public SeasonalPricingEN seasonalPriceEN { get; set; }


        public AccomodationEN(string name, string address, int numberOfRooms, double area,
            decimal price, bool hasSwimmingPool, bool hasGym, bool hasParking, bool isPetFriendly, string description)
        {
            Name = name;
            Address = address;
            NumberOfRooms = numberOfRooms;
            Area = area;
            Price = price;
            HasSwimmingPool = hasSwimmingPool;
            HasGym = hasGym;
            HasParking = hasParking;
            IsPetFriendly = isPetFriendly;
            Description = description;
            Id = 0;
            categoryEN = null;
            locationEN = null;
            seasonalPriceEN = null;
        }
    

        public AccomodationEN(int id)
        {
            Id = id;

        }

        public AccomodationEN()
        {
        }


        public DataSet CreateAccommodation()
        {
            DataSet ds = cad.CreateAccommodation(this);
            ExitMsg = cad.ExitMsg;
            return ds;
        }

        public DataSet ReadAccommodation(int id_accomadation)
        {
            DataSet ds = cad.ReadAccommodation(id_accomadation);
            ExitMsg = cad.ExitMsg;
            return ds;
        }
        
        public DataSet ReadAccommodationAll()
        {
            DataSet ds = cad.ReadAccommodationAll();
            ExitMsg = cad.ExitMsg;
            return ds;
        }

        public DataSet UpdateAccommodation(int id_accomadation)
        {
            DataSet ds = cad.UpdateAccommodation(this, id_accomadation);
            ExitMsg = cad.ExitMsg;
            return ds;
        }

        public DataSet DeleteAccommodation(int id_accomadation)
        {
            DataSet ds = cad.DeleteAccommodation(this, id_accomadation);
            ExitMsg = cad.ExitMsg;
            return ds;
        }


        public bool CreateAccommodationBool()
        {
            bool exitStatus = cad.CreateAccommodationBool(this);
            ExitMsg = cad.ExitMsg;
            return exitStatus;
        }

        public bool ReadAccommodationBool()
        {
            bool exitStatus = cad.ReadAccommodationBool(this);
            ExitMsg = cad.ExitMsg;
            return exitStatus;
        }


        public bool UpdateAccommodationBool()
        {
            bool exitStatuss = cad.UpdateAccommodationBool(this);
            ExitMsg = cad.ExitMsg;
            return exitStatuss;
        }

        public bool DeleteAccommodationBool()
        {
            bool exitStatus = cad.DeleteAccommodationBool(this);
            ExitMsg = cad.ExitMsg;
            return exitStatus;
        }

        public DataSet Filtration(DataSet ds, List<string> selectedItems)
        {
            // DataSet ds = cad.DeleteAccommodation(this, id_accomadation);
            ExitMsg = cad.ExitMsg;
            return ds;
        }

        public DataSet FilterAccomodations(DataSet d, string str)
        {
            DataSet ds = cad.FilterAccomodations(d, str);
            ExitMsg = cad.ExitMsg;
            return ds;
        }

        public List<string> GetCountries(string prefix)
        {
            List<string> list = cad.GetCountries(prefix);
            return list;
        }

        public List<string> GetCities(string prefix)
        {
            List<string> list = cad.GetCities(prefix);
            return list;
        }



        public DataSet ReadAllAccomodationsDataSet()
        {
            return cad.ReadAllAccomodationsDataSet();
        }

        public DataSet ReadAccommodationAllId(int id_user)
        {
            return cad.ReadAccommodationAllId(id_user);
        }

        public DataSet GetFilteredData()
        {
            DataSet ds = cad.GetFilteredData();
            ExitMsg = cad.ExitMsg;
            return ds;

        }
        public DataSet GetFilteredDataId(int userId) {
            DataSet ds = cad.GetFilteredDataId(userId);
            ExitMsg = cad.ExitMsg;
            return ds;
        }
        public DataTable GetCategoriesFromDatabase()
        {
            return cad.GetCategoriesFromDatabase();
        }

        public DataSet GetMostReviewed(int num)
        {
            return cad.GetMostReviewed(num);
        }

        public DataSet GetHighestRanked(int num)
        {
            return cad.GetHighestRanked(num);
        }

        public DataSet GetMostFavorited(int num)
        {
            return cad.GetMostFavorited(num);
        }
    }
}