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


namespace EventSearch.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            string apiKey = "kdRmzVqLVwJBfRnB";
            string location = "Milwaukee";
            string category = "music";
            string startDate = "2019031600";
            string endDate = "2019031600";
            int pageSize = 100;


            //WebRequest request = WebRequest.Create("http://api.eventful.com/json/events/search?app_key=" + apiKey + "&location=Milwaukee&q=Music");

            WebRequest request = WebRequest.Create("http://api.eventful.com/json/events/search?app_key=" + apiKey + "&location=" + location + "&q=" + category + "&date=" + startDate + "-" + endDate + "&page_size=" + pageSize + "&sort_order=start_time&sort_direction=descending");


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
