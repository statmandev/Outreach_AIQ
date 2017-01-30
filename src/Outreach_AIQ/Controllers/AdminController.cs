using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc; // main namespace
using Outreach_AIQ.Models; // models folder containing custom Quote class

namespace Outreach_AIQ.Controllers
{
	public class AdminController : Controller
    {
		private AIQ_Database _dbModel;

		public AdminController(AIQ_Database dbModel)
		{
			_dbModel = dbModel;
		}

		// GET method
		public IActionResult Admin()
		{
			return View(_dbModel.VehicleInformation.ToList());
		}

		// POST method
		[HttpPost]
		public IActionResult Admin(IEnumerable<VehicleInformation> model)
		{
			if (!string.IsNullOrWhiteSpace(Request.Form["Make"]) && !string.IsNullOrWhiteSpace(Request.Form["Model"]))
			{
				if (ModelState.IsValid)
				{
					VehicleInformation vInfo = new VehicleInformation { };
					vInfo.Make = Request.Form["Make"];
					vInfo.Model = Request.Form["Model"];

					_dbModel.VehicleInformation.Add(vInfo);
					_dbModel.SaveChanges();
					return RedirectToAction("Admin");
				}
			}

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
