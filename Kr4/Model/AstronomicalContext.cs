﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kr4.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kr4.Model
{
    public class AstronomicalContext : DbContext
    {
        public AstronomicalContext()
            : base()
        {
            //C:\poe\Kr4.db
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        public AstronomicalContext(DbContextOptions<AstronomicalContext> options)
            : base(options)
        {
            //C:\poe\Kr4.db
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }


        public AstronomicalContext(string dataSource)
            : base()
        {
            this.dataSource = dataSource;
            //C:\poe\Kr4.db
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=" + dataSource);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Planet> Planets { get; set; }
        public DbSet<Star> Stars { get; set; }
        public DbSet<Galaxy> Galaxies { get; set; }

        public DbSet<SpectralClass> SpectralClasses { get; set; }

        public DbSet<GalaxyType> GalaxysTypes { get; set; }


    }
}
