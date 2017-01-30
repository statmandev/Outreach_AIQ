using System;
using System.Collections.Generic; 
using System.Linq;
using Microsoft.AspNetCore.Mvc; // main namespace
using Outreach_AIQ.Models; // models folder containing custom Quote class

namespace Outreach_AIQ.Controllers
{
	public class HomeController : Controller
    {
		private AIQ_Database _dbModel;

		public HomeController(AIQ_Database dbModel)
		{
			_dbModel = dbModel;
		}

		// GET Method
		public IActionResult Index()
		{
			List<string> makes = new List<string> { };
			List<string> models = new List<string> { };
			foreach (VehicleInformation tableRow in _dbModel.VehicleInformation.ToList())
			{
				makes.Add(tableRow.Make);
				models.Add(tableRow.Model);
			}
			ViewData["Makes"] = makes;
			ViewData["Models"] = models;

			// initialize Quote model object when default page loads
			Quote quote = new Quote
			{
				// premium information
				Age = 0,
				ZipCode = 0,
				Mileage = 0,
				Year = 0,
				Make = "Not Selected",
				Model = "Not Selected",
				Premium = 0,

				// driver information
				FirstName = "",
				LastName = "",
				Address = "",
				City = "",
				State = ""
			};

			// read cookie if it exists
			if (!string.IsNullOrEmpty(Request.Cookies["FIRSTNAME"]) ||
				!string.IsNullOrEmpty(Request.Cookies["LASTNAME"]) ||
				!string.IsNullOrEmpty(Request.Cookies["ADDRESS"]) ||
				!string.IsNullOrEmpty(Request.Cookies["CITY"]) ||
				!string.IsNullOrEmpty(Request.Cookies["STATE"])
				) // if cookie info exists
			{
				// retrieve cookie info and store to quote object model member variables
				quote.FirstName = Request.Cookies["FIRSTNAME"];
				quote.LastName = Request.Cookies["LASTNAME"];
				quote.Address = Request.Cookies["ADDRESS"];
				quote.City = Request.Cookies["CITY"];
				quote.State = Request.Cookies["STATE"];
			}

			return View(quote);
		}

		// POST Method
		[HttpPost]
		public IActionResult Index(Quote model) // pass the model object to set its member variables
        {
			// gather form data and store values to Quote model
			int temp = 0; // initialize temp int to hold age since we cannot use a reference of model.Age during string conversion

			int.TryParse(Request.Form["Age"], out temp); // TryParse to convert Age from string to int and assign to temp
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

			// update current model values and cookies to retain form driver data after submission
			if (!string.IsNullOrWhiteSpace(Request.Form["FirstName"])) // if input is not null, whitespace, or empty
			{
				model.FirstName = Request.Form["FirstName"]; // assign input value to matching model value

				// update cookie with newest form information
				Response.Cookies.Append(
					"FIRSTNAME",
					Request.Form["FirstName"],
					new Microsoft.AspNetCore.Http.CookieOptions()
					{
						Expires = DateTime.Now.AddDays(1)
					}
				);
			}
			else if (string.IsNullOrWhiteSpace(Request.Form["FirstName"])) // input is null, whitespace, or empty
			{
				model.FirstName = Request.Cookies["FIRSTNAME"]; // assign existing cookie value to matching model value to force refresh
			}

			if (!string.IsNullOrWhiteSpace(Request.Form["LastName"])) // if input is not null, whitespace, or empty
			{
				model.LastName = Request.Form["LastName"]; // assign input value to matching model value

				// update cookie with newest form information
				Response.Cookies.Append(
					"LASTNAME",
					Request.Form["LastName"],
					new Microsoft.AspNetCore.Http.CookieOptions()
					{
						Expires = DateTime.Now.AddDays(1)
					}
				);
			}
			else if (string.IsNullOrWhiteSpace(Request.Form["LastName"])) // input is null, whitespace, or empty
			{
				model.LastName = Request.Cookies["LASTNAME"]; // assign existing cookie value to matching model value to force refresh
			}

			if (!string.IsNullOrWhiteSpace(Request.Form["Address"])) // if input is not null, whitespace, or empty
			{
				model.Address = Request.Form["Address"]; // assign input value to matching model value

				// update cookie with newest form information
				Response.Cookies.Append(
					"ADDRESS",
					Request.Form["Address"],
					new Microsoft.AspNetCore.Http.CookieOptions()
					{
						Expires = DateTime.Now.AddDays(1)
					}
				);
			}
			else if (string.IsNullOrWhiteSpace(Request.Form["Address"])) // input is null, whitespace, or empty
			{
				model.Address = Request.Cookies["ADDRESS"]; // assign existing cookie value to matching model value to force refresh
			}

			if (!string.IsNullOrWhiteSpace(Request.Form["City"])) // if input is not null, whitespace, or empty
			{
				model.City = Request.Form["City"]; // assign input value to matching model value

				// update cookie with newest form information
				Response.Cookies.Append(
					"CITY",
					Request.Form["City"],
					new Microsoft.AspNetCore.Http.CookieOptions()
					{
						Expires = DateTime.Now.AddDays(1)
					}
				);
			}
			else if (string.IsNullOrWhiteSpace(Request.Form["City"])) // input is null, whitespace, or empty
			{
				model.City = Request.Cookies["CITY"]; // assign existing cookie value to matching model value to force refresh
			}

			if (!string.IsNullOrWhiteSpace(Request.Form["State"])) // if input is not null, whitespace, or empty
			{
				model.State = Request.Form["State"]; // assign input value to matching model value

				// update cookie with newest form information
				Response.Cookies.Append(
					"STATE",
					Request.Form["State"],
					new Microsoft.AspNetCore.Http.CookieOptions()
					{
						Expires = DateTime.Now.AddDays(1)
					}
				);
			}
			else if (string.IsNullOrWhiteSpace(Request.Form["State"])) // input is null, whitespace, or empty
			{
				model.State = Request.Cookies["STATE"]; // assign existing cookie value to matching model value to force refresh
			}

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
