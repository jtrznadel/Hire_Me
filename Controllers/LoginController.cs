using HireMe.Models;
using HireMe.Services;
using Microsoft.AspNetCore.Mvc;

namespace HireMe.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }

        public IActionResult ProcessLogin(UserModel user)
        {
            if (user.Username == null || user.Password == null)
            {
                TempData["AlertMessage"] = "Please pass all information";
                return RedirectToAction("Index");
            }
            else
            {
                LoginService loginService = new LoginService();
                if (loginService.IsValid(user))
                {
                    user.Id = loginService.GetUserID(user);
                    int _id = user.Id;
                    GlobalVariables.GlobaluserID = user.Id;
                    return RedirectToAction("Index", "Dashboard", new {id = _id});
                }
                else
                {
                    TempData["AlertMessage"] = "You passed wrong username/password";
                    return RedirectToAction("Index");
                }
            }
            
        }
    }
}
