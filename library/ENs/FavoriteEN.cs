using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace library
{
  public class FavoriteEN
    {
        private FavoriteCAD cad = new FavoriteCAD();
        public UserEN user { get;set; }
        public AccomodationEN accomodation { get; set; }
        public string ExitMsg { get; set ;}
       
        public FavoriteEN()
        {
            user = new UserEN();
            accomodation = new AccomodationEN();
         
        }

        // Method for adding to the list of favorites
        public DataSet CreateFavorite(int userId, int accomodationId)
        {
           DataSet ds = cad.CreateFavorite( userId, accomodationId);
           ExitMsg = cad.ExitMsg;
           return ds;
        }

        public DataSet DeleteFavorite(int userId, int accomodationId)
        {
           DataSet ds = cad.DeleteFavorite( userId,  accomodationId);
           ExitMsg = cad.ExitMsg;
           return ds;
        }

        public DataSet ReadFavorite(int userId, int accomodationId)
        {
            DataSet ds = cad.ReadFavorite( userId,  accomodationId);
            ExitMsg = cad.ExitMsg;
            return ds;
        }

        public DataSet ReadFavoriteAll(int userId)
        {
            DataSet ds = cad.ReadFavoriteAll(userId);
            ExitMsg = cad.ExitMsg;
            return ds;
        }



        public bool CreateFavoriteBool(int userId, int accomodationId)
        {
            bool exitStatus = cad.CreateFavoriteBool(userId, accomodationId);
            ExitMsg = cad.ExitMsg;
            return exitStatus;
        }

        public bool DeleteFavoriteBool(int userId, int accomodationId)
        {
            bool exitStatus = cad.DeleteFavoriteBool(userId, accomodationId);
            ExitMsg = cad.ExitMsg;
            return exitStatus;
        }

        public bool ReadFavoriteBool(int userId, int accomodationId)
        {
            bool exitStatus = cad.ReadFavoriteBool(userId, accomodationId);
            ExitMsg = cad.ExitMsg;
            return exitStatus;
        }

       





    }
}
