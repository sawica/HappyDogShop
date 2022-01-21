
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace HappyDogShop2.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<HappyDogShop2.Models.MyDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }
    } 
}