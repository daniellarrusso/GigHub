using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    [Authorize]
    public class GigsController : Controller
    {
        private ApplicationDbContext _context;
        public GigsController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();

            var gigs = _context.Gigs
                .Include(a => a.Artist)
                .Include(g => g.Genre)
                .Where(a => a.ArtistId == userId && a.GigDate > DateTime.Now).ToList();

            return View(gigs);
        }

        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();

            var gigs = _context.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Gig).Include(a => a.Artist).Include(g => g.Genre)
                .ToList();

            var attendances = _context.Attendances
                .Where(a => a.AttendeeId == userId).ToList().ToLookup(g => g.GigId);

            var viewModel = new GigsViewModel
            {
                Gigs = gigs,
                ShowActions = User.Identity.IsAuthenticated,
                Attendances = attendances,
                Heading = "Gig's I'm attending"
            };

            return View("Gigs", viewModel);

        }

        // GET: Gigs
        public ActionResult Create()
        {

            var viewModel = new GigFormViewModel
            {
                Genres = _context.Genres.ToList(),
                Heading = "Create a Gig"
            };

            return View("GigForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
                return View("GigForm", viewModel);
            }

            var userId = User.Identity.GetUserId();

            var gig = new Gig
            {
                Venue = viewModel.Venue,
                ArtistId = userId,
                GenreId = viewModel.Genre,
                GigDate = viewModel.GetGigDate()
            };

            _context.Gigs.Add(gig);
            _context.SaveChanges();

            return RedirectToAction("Attending", "Gigs");
        }


        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();
            var gig = _context.Gigs.Single(g => g.GigId == id && g.ArtistId == userId);



            var viewModel = new GigFormViewModel
            {
                Genres = _context.Genres.ToList(),
                Venue = gig.Venue,
                Date = gig.GigDate.ToString("d MMM yyyy"),
                Time = gig.GigDate.ToString("HH:mm"),
                Genre = gig.GenreId,
                Heading = "Edit a Gig",
                Id = gig.GigId
            };

            return View("GigForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
                return View("GigForm", viewModel);
            }

            var userId = User.Identity.GetUserId();

            var gig = _context.Gigs.Single(g => g.GigId == viewModel.Id && g.ArtistId == userId);

            gig.Venue = viewModel.Venue;
            gig.GigDate = viewModel.GetGigDate();
            gig.GenreId = viewModel.Genre;

            _context.SaveChanges();

            return RedirectToAction("Mine", "Gigs");
        }




    }


}