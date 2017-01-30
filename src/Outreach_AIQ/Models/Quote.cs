using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Outreach_AIQ.Models
{
	public class Quote
    {
		// premium information
		public int Age { get; set; }
		public int ZipCode { get; set; }
		public int Mileage { get; set; }
		public int Year { get; set; }
		public string Make { get; set; }
		public string Model { get; set; }
		public int Premium { get; set; }

		// driver information
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
		public string State { get; set; }
	}
}
