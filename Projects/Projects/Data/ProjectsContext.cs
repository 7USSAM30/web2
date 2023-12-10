using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Projects.Models;

namespace Projects.Data
{
    public class ProjectsContext : DbContext
    {
        public ProjectsContext (DbContextOptions<ProjectsContext> options)
            : base(options)
        {
        }
        public DbSet<Projects.Models.book>? book { get; set; }

        public DbSet<Projects.Models.usersaccounts>? usersaccounts { get; set; }

        public DbSet<Projects.Models.orders>? orders { get; set; }

        public DbSet<Projects.Models.orderdetail> ordersdetail { get; set; }

        public DbSet<Projects.Models.report>? report { get; set; }


    }
}
