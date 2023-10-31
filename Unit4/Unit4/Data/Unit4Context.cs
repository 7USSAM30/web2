using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Unit4.Models;

namespace Unit4.Data
{
    public class Unit4Context : DbContext
    {
        public Unit4Context (DbContextOptions<Unit4Context> options)
            : base(options)
        {
        }

        public DbSet<Unit4.Models.items> items { get; set; } = default!;

        public DbSet<Unit4.Models.book>? book { get; set; }
        public DbSet<Unit4.Models.orderdetail> orderdetail { get; set; }
        public DbSet<Unit4.Models.orders>? orders { get; set; }
    }
}
