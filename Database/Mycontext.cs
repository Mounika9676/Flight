using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Flight.Entity;
using AutoMapper;
namespace Flight.Database
{
        public class Mycontext : DbContext
        {
            public Mycontext(DbContextOptions<Mycontext> options) : base(options)
            {
            }
            public DbSet<User> Users { get; set; }
            public DbSet<flights> Flights { get; set; }
        }
    }
