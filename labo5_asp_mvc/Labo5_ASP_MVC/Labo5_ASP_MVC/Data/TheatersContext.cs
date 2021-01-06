using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Labo5_ASP_MVC.Models
{
    public class TheatersContext : DbContext
    {
        public TheatersContext (DbContextOptions<TheatersContext> options)
            : base(options)
        {
        }

        public DbSet<Labo5_ASP_MVC.Models.NewsMessage> NewsMessage { get; set; }
    }
}
