using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace library
{
    public class WeatherEN
    {
        private WeatherCAD cad = new WeatherCAD();

        public int Id { get; set; }
        public int TemperatureAvg { get; set; }
        public int TemperatureMax { get; set; }
        public int TemperatureMin { get; set; }
        public int Rain { get; set; }
        public int Wind { get; set; }
        public LocationEN Location = new LocationEN();
        public DateTime Date { get; set; }

        public string ExitMsg { get; set; }

        public WeatherEN() { }

        // Creates a new weather in the DB
        public DataSet CreateWeather()
        {
            DataSet ds = cad.CreateWeather(this);
            ExitMsg = cad.ExitMsg;
            return ds;
        }

        // Reads a weather from the DB
        public bool ReadWeather()
        {
            bool exitStatus = cad.ReadWeather(this);
            ExitMsg = cad.ExitMsg;
            return exitStatus;
        }

        // Updates the weather in the DB
        public bool UpdateWeather(int id)
        {
            bool exitStatus = cad.UpdateWeather(this, id);
            ExitMsg = cad.ExitMsg;
            return exitStatus;
        }

        // Deletes the weather from the DB
        public bool DeleteWeather(int id)
        {
            bool exitStatus = cad.DeleteWeather(this, id);
            ExitMsg = cad.ExitMsg;
            return exitStatus;
        }

        public DataSet ReadWeathersOfLocation(int location_id)
        {
            return cad.ReadWeathersOfLocation(location_id);
        }
    }
}
