﻿using System;
using System.Collections.Generic;
using System.Text;
using EventSearch.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EventSearch.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Citites> Cities { get; set; }
        public DbSet<Cuisine> Cuisine { get; set; }
        public DbSet<Establishment> Establishment { get; set; }

        public DbSet<Category> Category { get; set; }
        public DbSet<Image> Image { get; set; }

    }
}
