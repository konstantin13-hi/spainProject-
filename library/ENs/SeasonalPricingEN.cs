using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace library

{
    public class SeasonalPricingEN
    {
        public int Id { get; set; }

        public String NamePricing { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double MultiplierPricing { get; set; }
        private SeasonalPricingCAD seasonalPricingCAD;
        public string message;


        public SeasonalPricingEN()
        {
            this.seasonalPricingCAD = new SeasonalPricingCAD();
        }

        public bool createSeasonalPricing()
        {
            bool value = seasonalPricingCAD.createSeasonalPricing(this);
            this.message = this.seasonalPricingCAD.message;
            return value;
        }

        public bool readSeasonalPricing()
        {
            bool value = seasonalPricingCAD.readSeasonalPricing(this);
            this.message = this.seasonalPricingCAD.message;
            return value;
        }

        public bool updateSeasonalPricing()
        {
            bool value = seasonalPricingCAD.updateSeasonalPricing(this);
            this.message = this.seasonalPricingCAD.message;

            return value;
        }

        public bool deleteSeasonalPricing()
        {
            bool value = seasonalPricingCAD.deleteSeasonalPricing(this);
            this.message = this.seasonalPricingCAD.message;
            return value;
        }

        public DataSet ReadAllSeasonalPricesDataSet()
        {
            return seasonalPricingCAD.ReadAllSeasonalPricesDataSet();
        }
    }
}

