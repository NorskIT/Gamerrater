﻿using System;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;
using GamerRater.Model;
using Microsoft.EntityFrameworkCore;

namespace GamerRater.DataAccess
{
	public class DataContext : DbContext
	{

		public DbSet<GameRoot> Games { get; set; }
		public DbSet<GameCover> Covers { get; set; }
		public DbSet<Rating> Ratings { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<UserGroup> UserGroups { get; set; }
		public DbSet<Platform> Platforms { get; set; }
		

		public DataContext()
		{
		}
		public DataContext(DbContextOptions<DataContext> options)
			: base(options)
		{
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
			{
				/*DataSource = "Donau.hiof.no",
				InitialCatalog = "magnusp",
				UserID = "magnusp",
				Password = "6U8Ujvm7"*/

				DataSource = "(localdb)\\MSSQLLocalDB",
				InitialCatalog = "GamerRater",
				IntegratedSecurity = true
			};

            optionsBuilder.UseSqlServer(builder.ConnectionString, b => b.MigrationsAssembly("GamerRater.Api"));
		}
		
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

            modelBuilder.Entity<GameRoot>().Property(t => t.Id).ValueGeneratedNever();
            modelBuilder.Entity<GameCover>().Property(t => t.id).ValueGeneratedNever();
            modelBuilder.Entity<Platform>().Property(t => t.Id).ValueGeneratedNever();

            modelBuilder.Entity<GameRoot>()
                .HasMany<Rating>(r => r.Ratings)
                .WithOne(g => g.Game);

            modelBuilder.Entity<Rating>()
                .HasOne<GameRoot>(g => g.Game)
                .WithMany(r => r.Ratings);

        }
	}
}
