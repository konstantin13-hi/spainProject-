using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace library
{
    public class ReviewEN
    {
        private float _averagePoints;
        public int Id { get; set; }
        public int Accommodation_id { get; set; }
        public int User_id { get; set; } 
        public float AveragePoints 
            {   get { return _averagePoints; }
                set { _averagePoints = value; } 
            } //counted from all other point categories
        public float OverallPoints { get; set; } //How was your stay overall?
        public float AreaPoints { get; set; } //How was the area around the acoommodation
        public float TidinessPoints { get; set; } //How clean was the accommodation?
        public string Review { get; set; } //Review text can be added here
        public string StatusMsg { get; set; }

        private ReviewCAD rew = new ReviewCAD();

        public ReviewEN() { }
        public ReviewEN(int accommodation_id, int user_id, float averagepoints, float overallPoints, float areaPoints, float tidinessPoints, string review)
        {
            Accommodation_id = accommodation_id;
            User_id = user_id;
            AveragePoints = averagepoints;
            OverallPoints = overallPoints;
            AreaPoints = areaPoints;
            TidinessPoints = tidinessPoints;
            Review = review;
        }
        //passes object to cad layer readAvgReview method, returns dataset
        public DataSet readAvgReview()
        {
            DataSet ds = rew.readAvgReview(this);

            return ds;
        }
        //passes object to cad layer readAccommodation method, returns accommodation name as string
        public string readAccommodation()
        {
            string name = rew.readAccommodation(this);

            return name;
        }
        //passes object to cad layer readNonReviewed method, return dataset
        public DataSet readNonReviewed()
        { 
            DataSet ds = rew.readNonReviewed(this);

            return ds;
        }
        //passes object to cad layer readAccHistory method, return true if operation ok, false if failed. 
        public bool readAccHistory(UserEN user, int i)
        {
            bool history = rew.readAccHistory(user, i);
            
            return history;
        }
        //passes object to cad layer createReview method return true if operation ok, false if failed.
        public bool createReview()
        {
            ReviewCAD rew = new ReviewCAD();
            bool exitStatus = rew.createReview(this);
            return exitStatus;
        }

        public DataSet readReview()
        {  
            DataSet ds = rew.readReview(this);

            return ds;
        }
        //passes object to cad layer updateReview method return true if operation ok, false if failed.
        public bool updateReview()
        {
            bool exitStatus = rew.updateReview(this);
            return exitStatus;
        }
        //passes object to cad layer deleteReview method return true if operation ok, false if failed.
        public bool deleteReview()
        {
            bool exitStatus = rew.deleteReview(this);
 
            return exitStatus;
        }
    }
}
