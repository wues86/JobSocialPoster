using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSocialPoster.Core.Models
{
    public class PostCategory : BaseEntity
    {
        [DisplayName("Nazwa Kategorii")]
        public string Name { get; set; }

    }
}
