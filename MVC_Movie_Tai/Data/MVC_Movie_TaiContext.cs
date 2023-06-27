using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MVC_Movie_Tai.Models;

namespace MVC_Movie_Tai.Data
{
    public class MVC_Movie_TaiContext : DbContext
    {
        public MVC_Movie_TaiContext (DbContextOptions<MVC_Movie_TaiContext> options)
            : base(options)
        {
        }

        public DbSet<MVC_Movie_Tai.Models.Movie> Movie { get; set; }
    }
}
