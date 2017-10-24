using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using LostAndFound.Models;
using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using LostAndFound.Models.Database;

namespace LostAndFound
{
    public class MainDbContext : DbContext
    {

        public MainDbContext()
        : base("name=DefaultConnection")
        {
        }

        public DbSet <ApplicationUser> applicationUser { get; set; }
        public DbSet <LostAndFoundRoles> roles { get; set; }
        public DbSet <ClientBasicInfo> clientBasicInfo { get; set; }
        public DbSet <DeviceCategory> deviceCategory { get; set; }
        public DbSet <DeviceCondition> deviceCondition { get; set; }
        public DbSet <DeviceSubcategory> deviceSubcategory { get; set; }
        public DbSet <ClientDeviceInfo> clientDeviceInfo { get; set; }
        public DbSet <DeviceBin> deviceBin { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);

        }

        public static MainDbContext Create()
        {
            return new MainDbContext();
        }
    }
}