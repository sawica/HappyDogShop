using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HappyDogShop2.Models
{

	//Mechanizm migracji (Migrations)
	//1. Enable-Migrations - włączenie migracji (raz na początku życia projektu)
	//2. Add-Migration NAZWA - zrobienie migawki struktury modeli (po każdej zmianie modeli)
	//3. Update-Database - zmiany na bazie (po każdym Add-Migration)
	public class MyDbContext : DbContext
	{
		public MyDbContext() : base("MyCS") { }

		public DbSet<Product> Products { get; set; }

		public DbSet<User> Users { get; set; }

        public System.Data.Entity.DbSet<HappyDogShop2.Models.Category> Categories { get; set; }

        public System.Data.Entity.DbSet<HappyDogShop2.Models.Sale> Sales { get; set; }
    }
}