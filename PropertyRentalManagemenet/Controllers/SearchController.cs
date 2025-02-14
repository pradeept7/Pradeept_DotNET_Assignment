using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HouseRentMVC.Controllers
{
    public class SearchController : Controller
    {
        private HouseRentDBContext context = new HouseRentDBContext();

        [HttpGet]
        public ActionResult Index(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                ViewBag.Message = "Please enter a search term.";
                return View(new List<mypost>());
            }

            // Fetch posts that match the search query in AreaName or HouseName
            var results = context.Posts
                .Join(context.Areas,
                    e => e.AreaId,
                    d => d.Id,
                    (e, d) => new mypost
                    {
                        Id = e.Id,
                        HouseName = e.HouseName,
                        Image = e.Image,
                        Floor = e.Floor,
                        Bedroom = e.Bedroom,
                        Dining = e.Dining,
                        Drawing = e.Drawing,
                        Kitchen = e.Kitchen,
                        Category = e.Category,
                        Rent = e.Rent,
                        PhoneNumber = e.PhoneNumber,
                        AreaName = d.AreaName
                    })
                .Where(p => p.HouseName.Contains(query) || p.AreaName.Contains(query))
                .ToList();

            if (!results.Any())
            {
                ViewBag.Message = "No results found for '" + query + "'.";
            }

            return View(results);
        }
    }
}
