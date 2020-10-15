using JobSocialPoster.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSocialPoster.Core.ViewModels
{
    public class ProfileManagerViewModel
    {
        public Profile Profile { get; set; }
        public IEnumerable<ProfileCategory> ProfileCategories { get; set; }
    }
}
