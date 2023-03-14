using HireMe.Models;
using HireMe.Services;
using Microsoft.AspNetCore.Mvc;

namespace HireMe.Controllers
{
    public class RegisterController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProcessRegister(ExtendedUserModel user)
        {
            if(user.RePassword == null || user.Password == null || user.FirstName == null || user.LastName == null || user.Username == null || user.Email == null)
            {
                TempData["AlertMessage"] = "Please pass all information";
                return RedirectToAction("Index");
            }
            else
            {
                RegisterService registerService = new RegisterService();
                if (registerService.IfExists(user))
                {
                    TempData["AlertMessage"] = "This email address is already being used";
                    return RedirectToAction("Index");
                }
                if (registerService.CheckLoginExisting(user))
                {
                    TempData["AlertMessage"] = "This login is already existing";
                    return RedirectToAction("Index");
                }
                if (user.Password != user.RePassword)
                {
                    TempData["AlertMessage"] = "Passwords are not the same";
                    return RedirectToAction("Index");
                }
                else
                {
                    registerService.CreateUser(user);
                    TempData["AlertMessage"] = "Register Completed";
                    return RedirectToAction("Index");
                }
            }
            
        }
    }
}
