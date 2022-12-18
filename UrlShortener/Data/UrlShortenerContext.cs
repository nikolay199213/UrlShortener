using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.Data
{
    public class UrlShortenerContext : DbContext
    {
        public DbSet<Url> Urls { get; set; }     
        public UrlShortenerContext(DbContextOptions<UrlShortenerContext> options)
            : base(options) { }
    }
}
