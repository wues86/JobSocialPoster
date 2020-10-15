using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSocialPoster.Core.Models
{
    public class ProfileCategory
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public ProfileCategory()
        {
            this.Id = Guid.NewGuid().ToString();
        }
    }
}
