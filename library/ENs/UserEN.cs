using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace library
{
    public class UserEN
    {
        private UserCAD cad = new UserCAD();

        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } 
        public string Name { get; set; }
        public bool IsAdmin { get; set; } 
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public string ExitMsg { get; set; }

        public UserEN() { }

        // Creates a new user in the DB
        public bool CreateUser()
        {
            bool exitStatus = cad.CreateUser(this);
            ExitMsg = cad.ExitMsg;
            return exitStatus;
        }

        // Reads user from DB
        public bool ReadUser()
        {
            bool exitStatus = cad.ReadUser(this);
            ExitMsg = cad.ExitMsg;
            return exitStatus;
        }

        // Updates user in DB
        public bool UpdateUser()
        {
            bool exitStatus = cad.UpdateUser(this);
            ExitMsg = cad.ExitMsg;
            return exitStatus;
        }

        // Deletes user from DB
        public bool DeleteUser()
        {
            bool exitStatus = cad.DeleteUser(this);
            ExitMsg = cad.ExitMsg;
            return exitStatus;
        }

        // Gets a dataset with all users
        public DataSet ReadAllUsersDataSet()
        {
            return cad.ReadAllUsersDataSet();
        }

        // Reads the email of a user and return the ID
        public bool GetIdFromEmail()
        {
            bool exitStatus = cad.GetIdFromEmail(this);
            ExitMsg = cad.ExitMsg;
            return exitStatus;
        }
    }
}
