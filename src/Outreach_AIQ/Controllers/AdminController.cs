using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc; // main namespace
using Outreach_AIQ.Models; // models folder containing custom Quote class

namespace Outreach_AIQ.Controllers
{
	public class AdminController : Controller
    {
		// load database context
		private AIQ_Database _dbModel;
		public AdminController(AIQ_Database dbModel)
		{
			_dbModel = dbModel;
		}

		// GET method
		public IActionResult Admin()
		{
			// set view based on being logged in as admin or not
			if (!string.IsNullOrWhiteSpace(Request.Cookies["LOGIN_ID"])) // if cookie value is not null, whitespace, or empty
			{
				// initialize ViewData["loginID"] to LOGIN_ID cookie value
				ViewData["loginID"] = Request.Cookies["LOGIN_ID"];
			}
			else
			{
				// initialize ViewData["loginID"] and LOGIN_ID cookie to 0 to represent logged out
				ViewData["loginID"] = 0;

				Response.Cookies.Append(
					"LOGIN_ID",
					"0",
					new Microsoft.AspNetCore.Http.CookieOptions()
					{
						Expires = DateTime.Now.AddDays(1)
					}
				);
			}

			return View(_dbModel.VehicleInformation.ToList());
		}

		// POST method
		[HttpPost]
		public IActionResult Admin(IEnumerable<VehicleInformation> model)
		{
			// initialize ViewData["loginID"] to LOGIN_ID cookie value
			ViewData["loginID"] = Request.Cookies["LOGIN_ID"];

			// attempt to log user in by setting ViewData["loginID"] and LOGIN_ID cookie to User_ID that
			// matches login and password from the database (admin User_ID == 1) to represent logged in
			if (!string.IsNullOrWhiteSpace(Request.Form["UserName"]) && !string.IsNullOrWhiteSpace(Request.Form["Password"]))
			{
				// get query listing database rows that match login and password from the form
				List<LoginInformation> query = (
						from p in _dbModel.Login
						where p.Username.Equals(Request.Form["UserName"]) && p.Password.Equals(Request.Form["Password"])
						select p).ToList();

				// set ViewData["loginID"] and LOGIN_ID cookie to the query's User_ID
				foreach (var tableRow in query)
				{
					ViewData["loginID"] = tableRow.User_ID.ToString();

					Response.Cookies.Append(
						"LOGIN_ID",
						tableRow.User_ID.ToString(),
						new Microsoft.AspNetCore.Http.CookieOptions()
						{
							Expires = DateTime.Now.AddDays(1)
						}
					);
				}
			}

			// if user clicks logout then set ViewData["loginID"] and LOGIN_ID cookie to 0 to represent logged out
			if (Request.Form["logout"] == "logout")
			{
				ViewData["loginID"] = 0;

				Response.Cookies.Append(
					"LOGIN_ID",
					"0",
					new Microsoft.AspNetCore.Http.CookieOptions()
					{
						Expires = DateTime.Now.AddDays(1)
					}
				);
			}

			// add make and model to vehicle information table in database
			if (!string.IsNullOrWhiteSpace(Request.Form["Make"]) && !string.IsNullOrWhiteSpace(Request.Form["Model"]))
			{
				if (ModelState.IsValid)
				{
					VehicleInformation vInfo = new VehicleInformation { };
					vInfo.Make = Request.Form["Make"];
					vInfo.Model = Request.Form["Model"];

					_dbModel.VehicleInformation.Add(vInfo);
					_dbModel.SaveChanges();
					return View(_dbModel.VehicleInformation.ToList());
				}
			}

			// remove make and model from vehicle information table in database
			int numVehicles = _dbModel.VehicleInformation.Count();
			for (int i = 0; i < numVehicles; i++)
			{
				if (Request.Form["chkBox" + i] == "on")
				{
					int parsedID = 0;
					int.TryParse(Request.Form["hiddenID" + i], out parsedID);

					VehicleInformation vInfo = new VehicleInformation { };
					vInfo.Vehicle_ID = parsedID;
					vInfo.Make = Request.Form["hiddenMake" + i];
					vInfo.Model = Request.Form["hiddenModel" + i];

					_dbModel.VehicleInformation.Remove(vInfo);
					_dbModel.SaveChanges();
				}
			}

			return View(_dbModel.VehicleInformation.ToList());
		}
	}
}
