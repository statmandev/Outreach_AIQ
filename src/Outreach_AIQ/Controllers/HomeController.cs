using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http; // to load FormCollection class for handling form data
using Outreach_AIQ.Models; // models folder containing custom Quote class

namespace Outreach_AIQ.Controllers
{
	public class HomeController : Controller
    {
		// Get Method
		public IActionResult Index()
		{
			// initialize Quote model object when default page loads
			Quote quote = new Quote
			{
				Age = 0,
				ZipCode = 0,
				Mileage = 0,
				Year = 0,
				Make = "Not Selected",
				Model = "Not Selected",
				Premium = 0
			};

			return View(quote);
		}

		// Post Method
		[HttpPost]
		public IActionResult Index(FormCollection collection, Quote model) // collection will contain all form information from Index View Post
        {
			// gather form data
			int temp = 0; // define int to hold age since we cannot use a reference of model.Age during string conversion

			int.TryParse(Request.Form["Age"], out temp); // TryParse to convert from string to int and assign to temp
			model.Age = temp; // assign converted int to Age member variable of our Quote model

			int.TryParse(Request.Form["ZipCode"], out temp);
			model.ZipCode = temp;

			int.TryParse(Request.Form["Mileage"], out temp);
			model.Mileage = temp;

			int.TryParse(Request.Form["Year"], out temp);
			model.Year = temp;

			model.Make = Request.Form["Make"];
			model.Model = Request.Form["Model"];

			// calculate annual premium
			model.Premium = 100;
            return View(model);
        }
		
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
