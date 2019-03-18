using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EventSearch.Models;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Http;
using EventSearch.Data;

namespace EventSearch.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public string Location { get; set; }
        public string Category { get; set; }
        public int Miles { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public IActionResult Index()
        {
            List<Category> categories = new List<Category>();
            categories = (from category in _context.Category
                          select category).ToList();
            categories.Insert(0, new Category { CategoryId = 0, CategoryName = "Select a Category" });

            ViewBag.ListOfCategory = categories;

            return View();
        }


        public IActionResult Search(string location, string category, int miles, DateTime? startDate, DateTime? endDate)
        {
            string apiKey = "kdRmzVqLVwJBfRnB";
            int pageSize = 100;

            if (!string.IsNullOrEmpty(location))
            {
                Location = location;
            }

            if (!string.IsNullOrEmpty(category))
            {
                Category = category;
            }

            if (miles > 0)
            {
                Miles = miles;
            }

            if (startDate.HasValue)
            {
                StartDate = (DateTime)startDate;
            }

            if (endDate.HasValue)
            {
                EndDate = (DateTime)endDate;
            }

            WebRequest request = WebRequest.Create("http://api.eventful.com/json/events/search?app_key=" + apiKey + "&location=" + Location + "&within=" + Miles + "&units=miles&q=" + Category + "&date=" + StartDate.ToString("yyyyMMdd00") + "-" + EndDate.ToString("yyyyMMdd00") + "&page_size=" + pageSize + "&sort_order=start_time&sort_direction=descending");



            WebResponse response = request.GetResponse();

            Stream stream = response.GetResponseStream();

            StreamReader reader = new StreamReader(stream);

            string responseFromServer = reader.ReadToEnd();

            JObject parsedString = JObject.Parse(responseFromServer);

            EventfulMain eventful = parsedString.ToObject<EventfulMain>();

            return View(eventful);
        }





        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
