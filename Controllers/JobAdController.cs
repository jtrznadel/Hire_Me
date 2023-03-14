using HireMe.Data;
using HireMe.Models;
using Microsoft.AspNetCore.Mvc;

namespace HireMe.Controllers
{
    public class JobAdController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateAd(JobadModel jobad)
        {
            AdDAO adDAO = new AdDAO();
            adDAO.CreateCAd(jobad, GlobalVariables.GlobaluserID);
            TempData["AlertMessage1"] = "Successfully added your ad!";
            return RedirectToAction("Index", "Dashboard");
        }
    }
}
