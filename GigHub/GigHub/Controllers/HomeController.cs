using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using GigHub.ViewModels;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;
        public HomeController()
        {
            _context = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();

            var gigs = _context.Gigs
                .Include(g => g.Genre)
                .Include(a => a.Artist)
                .Where(g => g.GigDate > DateTime.Now)
                .ToList();

            var attending = _context.Attendances
                .Where(a => a.AttendeeId == userId).ToList()
                .ToLookup(g => g.GigId);



            var viewModel = new GigsViewModel
            {
                Gigs = gigs,
                Attendances = attending,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Upcoming Gigs"

            };


            return View("Gigs", viewModel);
        }

        public ActionResult Mine()
        {


            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}