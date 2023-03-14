using HireMe.Models;

namespace HireMe.Services
{
    public class LoginService
    {
        UsersDAO usersDAO = new UsersDAO();
        public bool IsValid(UserModel user)
        {
            return usersDAO.FindUser(user);
        }

        public int GetUserID(UserModel user)
        {
            return usersDAO.GetUserID(user);
        }

    }
   
}
