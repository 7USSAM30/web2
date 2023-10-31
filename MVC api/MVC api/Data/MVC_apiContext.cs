using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using MVC_api.Models;

namespace MVC_api.Data
{
    public class MVC_apiContext : DbContext
    {
        public MVC_apiContext(DbContextOptions<MVC_apiContext> options)
            : base(options)
        {
        }

        public DbSet<MVC_api.Models.items> items { get; set; } = default!;

        public DbSet<MVC_api.Models.book>? book { get; set; }
        
     
         
    }
}
