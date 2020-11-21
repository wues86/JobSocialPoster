using JobSocialPoster.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSocialPoster.Core.ViewModels
{
    public class PostManagerViewModel
    {
        public Post Post { get; set; }
        public IEnumerable<PostCategory> PostCategories { get; set; }

        public IEnumerable<Profile> Profile { get; set; }

    }
}
