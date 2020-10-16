using JobSocialPoster.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSocialPoster.DataAccess.SQL
{
    public class DataContext : DbContext
    {
        public DataContext() : base("DefaultConnection")
        {

            }
        
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<ProfileCategory> ProfileCategories { get; set; }
    }
}
