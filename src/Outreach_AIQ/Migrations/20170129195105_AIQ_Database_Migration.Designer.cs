using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Outreach_AIQ.Models;

namespace Outreach_AIQ.Migrations
{
    [DbContext(typeof(AIQ_Database))]
    [Migration("20170129195105_AIQ_Database_Migration")]
    partial class AIQ_Database_Migration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Outreach_AIQ.Models.LoginInformation", b =>
                {
                    b.Property<int>("User_ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Password");

                    b.Property<string>("Username");

                    b.HasKey("User_ID");

                    b.ToTable("Login");
                });

            modelBuilder.Entity("Outreach_AIQ.Models.VehicleInformation", b =>
                {
                    b.Property<int>("Vehicle_ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Make");

                    b.Property<string>("Model");

                    b.HasKey("Vehicle_ID");

                    b.ToTable("VehicleInformation");
                });
        }
    }
}
