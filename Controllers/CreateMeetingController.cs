using Microsoft.AspNetCore.Mvc;
using HireMe.Models;
using HireMe.Data;

namespace HireMe.Controllers
{
    public class CreateMeetingController : Controller
    {

        public IActionResult Index(int guest)
        {
            GlobalVariables.GlobalGuestID = guest;
            return View();
        }

        public IActionResult CreateMeeting(MeetingModel model)
        { 
            MeetingsDAO meetingsDAO = new MeetingsDAO();
            model.OrganizerID = GlobalVariables.GlobaluserID;
            meetingsDAO.CreateMeeting(model, GlobalVariables.GlobalGuestID);
            TempData["AlertMessage1"] = "Meeting successfully added!";
            return RedirectToAction("Index", "Dashboard");
        }
    }
}
