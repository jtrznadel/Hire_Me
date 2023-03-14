using HireMe.Data;
using HireMe.Models;
using HireMe.Services;
using Microsoft.AspNetCore.Mvc;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace HireMe.Controllers
{
    public class DashboardController : Controller
    {

        public IActionResult Index()
        {
            List<MeetingModel> meetings = new List<MeetingModel>();
            UsersDAO userDAO = new UsersDAO();
            MeetingsDAO meetingsDAO = new MeetingsDAO();
            meetings = meetingsDAO.FetchAll(GlobalVariables.GlobaluserID);
            ViewData["meetings"] = meetings;
            ViewBag.MyString = userDAO.GetUserFullName(GlobalVariables.GlobaluserID); ;
            return View("Index");
        }

        public IActionResult RouteCompanyAd()
        {
            return RedirectToAction("Index", "JobAd");
        }

        public IActionResult RouteCandidateAd()
        {
            return RedirectToAction("Index", "CandidateAd");
        }

        public IActionResult RouteSearchCompanyAd()
        {
            return RedirectToAction("Index", "JobOfferSearch");
        }

        public IActionResult RouteSearchCandidateAd()
        {
            return RedirectToAction("Index", "CanOfferSearch");
        }

        public IActionResult Logout()
        {
            GlobalVariables.GlobaluserID = 0;
            return RedirectToAction("Index", "Login");
        }

        public IActionResult GetPDF()
        {
            List<MeetingModel> meetings = new List<MeetingModel>();
            MeetingsDAO meetingsDAO = new MeetingsDAO();
            UsersDAO userDAO = new UsersDAO();
            meetings = meetingsDAO.FetchAll(GlobalVariables.GlobaluserID);
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Your meetings";
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            gfx.DrawString($"{userDAO.GetUserFullName(GlobalVariables.GlobaluserID)}'s meetings", new XFont("Arial", 36, XFontStyle.Bold), XBrushes.Black, 50, 80);
            gfx.DrawLine(new XPen(XColor.FromArgb(0, 0, 0)), new XPoint(100, 100), new XPoint(500, 100));
            gfx.DrawString("Organizer", new XFont("Arial", 15, XFontStyle.Bold), XBrushes.Black, new XPoint(50, 120));
            gfx.DrawString("Guest", new XFont("Arial", 15, XFontStyle.Bold), XBrushes.Black, new XPoint(170, 120));
            gfx.DrawString("Details", new XFont("Arial", 15, XFontStyle.Bold), XBrushes.Black, new XPoint(290, 120));
            gfx.DrawString("Date", new XFont("Arial", 15, XFontStyle.Bold), XBrushes.Black, new XPoint(410, 120));
            gfx.DrawString("Place", new XFont("Arial", 15, XFontStyle.Bold), XBrushes.Black, new XPoint(500, 120));
            gfx.DrawLine(new XPen(XColor.FromArgb(0, 0, 0)), new XPoint(50, 130), new XPoint(550, 130));
            int Y = 150;
            foreach(var meeting in meetings)
            {
                gfx.DrawString($"{meeting.OrganizerName}", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(40, Y));
                gfx.DrawString($"{meeting.GuestName}", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(160, Y));
                gfx.DrawString($"{meeting.MeetingInfo}", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(290, Y));
                gfx.DrawString($"{meeting.MeetingDate}", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(370, Y));
                gfx.DrawString($"{meeting.MeetingPlace}", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(500, Y));
                gfx.DrawLine(new XPen(XColor.FromArgb(0, 0, 0)), new XPoint(50, Y + 10), new XPoint(550, Y + 10));
                Y += 30;
            }
            document.Save(GlobalVariables.fileLocation + "TestPDF.pdf");
            return RedirectToAction("Index");
        }
    }
}
