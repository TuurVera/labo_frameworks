using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rest_deel2.Model;

namespace Rest_deel2.Data
{
    public class NewsContext : DbContext
    {
        public NewsContext (DbContextOptions<NewsContext> options)
            : base(options)
        {
        }

        public DbSet<Rest_deel2.Model.NewsMessage> NewsMessage { get; set; }
    }
}
