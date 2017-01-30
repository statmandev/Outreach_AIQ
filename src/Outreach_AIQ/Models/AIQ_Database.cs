using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore; // for inheriting DbContext
using System.ComponentModel.DataAnnotations; // to set key value

namespace Outreach_AIQ.Models
{
    public class AIQ_Database : DbContext
    {
		public AIQ_Database(DbContextOptions<AIQ_Database> options) : base(options) { } // constructor to create database named AIQ_Database

		public DbSet<LoginInformation> Login { get; set; } // create database Login table that holds login information

		public DbSet<VehicleInformation> VehicleInformation { get; set; } // create database VehicleInformation table that holds vehicle information
	}

	public class LoginInformation
	{
		[Key]
		public int User_ID { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
	}

	public class VehicleInformation
	{
		[Key]
		public int Vehicle_ID { get; set; }
		public string Make { get; set; }
		public string Model { get; set; }
	}
}
