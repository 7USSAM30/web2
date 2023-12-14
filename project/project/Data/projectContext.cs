using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using project.Models;

namespace project.Data
{
    public class projectContext : DbContext
    {
        public projectContext (DbContextOptions<projectContext> options)
            : base(options)
        {
        }

        

        public DbSet<project.Models.Items> Items { get; set; }

        public DbSet<project.Models.Orders> Orders { get; set; }

        public DbSet<project.Models.item>? item { get; set; }
    }
}
