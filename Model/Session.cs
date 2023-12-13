using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealMaster.Model
{
    public static class Session
    {
        public static int CurrentUser { get; private set; }
        public static void ActivateUser(string login)
        {
            if (!DataBase.IsLoginExists(login))
            {
                throw new Exception("The user with this login does not exist");
            }

            CurrentUser = DataBase.GetUserID(login);
        }
    }
}
