using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Eternal.Models;
using Eternal.Models.ViewModels;
using Eternal.Data;

namespace Eternal.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var featuredContent = new FeaturedContent
            {
                FeaturedCards = await DbHelper.GetFeaturedCards(),
                FeaturedDecks = await DbHelper.GetFeaturedDecks()
            };

            foreach (var deck in featuredContent.FeaturedDecks)
            {
                deck.Username = await DbHelper.GetUsername(deck.UserID);
            }

            return View(featuredContent);
        }

        public IActionResult About()
        {
            //ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
