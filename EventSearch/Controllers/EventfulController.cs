using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EventSearch.Data;
using EventSearch.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using X.PagedList;

namespace EventSearch.Controllers
{
    public class EventfulController : Controller
    {
        private readonly ApplicationDbContext _context;


        public EventfulController(ApplicationDbContext context)
        {
            _context = context;
        }

        public string Location { get; set; }
        public string Category { get; set; }
        public int Miles { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Page { get; set; }

        public IActionResult Index()
        {
            List<Category> categories = new List<Category>();
            categories = (from category in _context.Category
                          select category).ToList();
            categories.Insert(0, new Category { CategoryId = 0, CategoryName = "Select a Category" });

            ViewBag.ListOfCategory = categories;

            return View();
        }


        public IActionResult Search(string location, string category, int miles, DateTime? startDate, DateTime? endDate, int? page)
        {

            string apiKey = "kdRmzVqLVwJBfRnB";
            int totalPages = 100;
            int pageSize = 25;

            if (!string.IsNullOrEmpty(location))
            {
                HttpContext.Session.SetString("Location", location);
                ViewBag.Location = location;
            }
            Location = (string)HttpContext.Session.GetString("Location");

            if (!string.IsNullOrEmpty(category))
            {
                HttpContext.Session.SetString("Category", category);
                ViewBag.Category = category;
            }
            Category = (string)HttpContext.Session.GetString("Category");

            if (miles > 0)
            {
                Miles = miles;
            }

            if (startDate.HasValue)
            {
                HttpContext.Session.SetString("StartDate", startDate.ToString());
                ViewBag.StartDate = (DateTime)startDate;
            }
            DateTime dateTime;
            DateTime.TryParse(HttpContext.Session.GetString("StartDate"), out dateTime);
            StartDate = dateTime;

            if (endDate.HasValue)
            {
                HttpContext.Session.SetString("EndDate", endDate.ToString());
                ViewBag.EndDate = (DateTime)endDate;
            }
            DateTime eoutDate;
            DateTime.TryParse(HttpContext.Session.GetString("EndDate"), out eoutDate);
            EndDate = eoutDate;

            if (page.HasValue)
            {
                Page = (int)page;
            }
            else
            {
                Page = 1;
            }

            WebRequest request = WebRequest.Create("http://api.eventful.com/json/events/search?app_key=" + apiKey + "&location=" + Location + "&within=" + Miles + "&units=miles&q=" + Category + "&date=" + StartDate.ToString("yyyyMMdd00") + "-" + EndDate.ToString("yyyyMMdd00") + "&page_size=" + totalPages + "&sort_order=start_time&sort_direction=descending");



            WebResponse response = request.GetResponse();

            Stream stream = response.GetResponseStream();

            StreamReader reader = new StreamReader(stream);

            string responseFromServer = reader.ReadToEnd();

            JObject parsedString = JObject.Parse(responseFromServer);

            //EventfulMain eventful = parsedString.ToObject<EventfulMain>();


            int totalEventCount;

            if (parsedString["events"].HasValues)
            {
                var evts = parsedString["events"]["event"];
                IList<Event> events = new List<Event>();
                foreach (var ev in evts)
                {
                    Event e = new Event
                    {
                        title = (string)ev["title"],
                        venue_address = (string)ev["venue_address"],
                        city_name = (string)ev["city_name"],
                        start_time = (DateTime)ev["start_time"],
                        //stop_time = (DateTime)ev["stop_time"],
                        latitude = (string)ev["latitude"],
                        longitude = (string)ev["longitude"],
                        url = (string)ev["url"],
                        description = (string)ev["description"]
                    };
                    events.Add(e);
                }
                ViewBag.CurrentSort = "start_time";
                ViewBag.Model = events.ToPagedList(Page, pageSize);
                return View();
            }

            return View("Found Nothing");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}