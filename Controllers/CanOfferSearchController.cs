using Microsoft.AspNetCore.Mvc;
using HireMe.Models;
using HireMe.Data;

namespace HireMe.Controllers
{
    public class CanOfferSearchController : Controller
    {
        public IActionResult Index(SearchedItem item)
        {
            List<SearchingResult> results = new List<SearchingResult>();
            AdDAO adDAO = new AdDAO();
            results = adDAO.FetchAllCanOffers(item);
            ViewData["offers"] = results;
            return View();
        }

        public IActionResult ProcessSearch(SearchedItem item)
        {
            List<SearchingResult> results = new List<SearchingResult>();
            AdDAO adDAO = new AdDAO();
            results = adDAO.FetchCanOffers(item);
            ViewData["offers"] = results;
            return View("Index");
        }
    }
}
