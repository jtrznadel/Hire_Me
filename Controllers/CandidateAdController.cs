using HireMe.Data;
using HireMe.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HireMe.Controllers
{
    public class CandidateAdController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateAd(CandidateAdModel canad)
        {
            AdDAO adDAO = new AdDAO();
            adDAO.CreateEAd(canad, GlobalVariables.GlobaluserID);
            TempData["AlertMessage1"] = "Successfully added your ad!";
            return RedirectToAction("Index", "Dashboard");
        }

        public IActionResult ImportFromFile()
        {
            using (var sr = new StreamReader(GlobalVariables.fileLocation + "data.json"))
            {
                var json = sr.ReadToEnd();

                var listToLoad = JsonConvert.DeserializeObject<List<CandidateAdModel>>(json);
                AdDAO adDAO = new AdDAO();
                adDAO.CreateEAdFromFile(listToLoad);
            }
            return RedirectToAction("Index");
        }
    }
}
