using HireMe.Models;

namespace HireMe.Services
{
    public class RegisterService
    {
        UsersDAO usersDAO = new UsersDAO();

        public bool IfExists(ExtendedUserModel user)
        {
            return usersDAO.CheckIfExists(user);
        }
        public bool CheckLoginExisting(ExtendedUserModel user)
        {
            return usersDAO.CheckLoginExisting(user);
        }
        public void CreateUser(ExtendedUserModel user)
        {
            usersDAO.CreateUser(user);
        }

    }
}
