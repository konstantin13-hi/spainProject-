using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace library
{
    public class LocationEN
    {
        private LocationCAD cad = new LocationCAD();
        public int Id { get; set; }
        public string Statusmessage { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public LocationEN() { }

        public LocationEN(string name, string country)
        {
            Name = name;
            Country = country;
        }
        /// <summary>
        /// Creates a new location
        /// </summary>
        public bool CreateLocation()
        {
            bool status = cad.CreateLocation(this);
            Statusmessage = cad.Statusmessage;
            return status;
        }
        /// <summary>
        /// Reads location
        /// </summary>
        public bool ReadLocation()
        {
            bool status = cad.ReadLocation(this);
            Name = cad.Name;
            Country = cad.Country;
            Statusmessage = cad.Statusmessage;
            return status;
        }
        public DataSet ReadLocationName(string search)
        {
            return cad.ReadLocationName(search);
        }
        /// <summary>
        /// Updates a location
        /// </summary>
        public bool UpdateLocation()
        {
            bool status = cad.UpdateLocation(this);
            Statusmessage = cad.Statusmessage;
            return status;
        }
        /// <summary>
        /// Deletes a location
        /// </summary>
        public bool DeleteLocation()
        {
            bool status = cad.DeleteLocation(this);
            Statusmessage = cad.Statusmessage;
            return status;
        }
        /// <summary>
        /// Reads all locations
        /// </summary>
        public DataSet ReadAllLocationsDataSet()
        {
            return cad.ReadAllLocationsDataSet();
        }
        /// <summary>
        /// Reads 8 most popular locations
        /// </summary>
        public DataSet ReadPopularDataSet()
        {
            return cad.ReadPopularDataSet();
        }
        /// <summary>
        /// Reads 8 accomodations from the location
        /// </summary>
        public DataSet ReadAccomodations()
        {
            return cad.ReadAccomodations(this);
        }
        /// <summary>
        /// Reads 8 landmarks from the location
        /// </summary>
        public DataSet ReadLandmarks()
        {
            return cad.ReadLandmarks(this);
        }
    }
}
