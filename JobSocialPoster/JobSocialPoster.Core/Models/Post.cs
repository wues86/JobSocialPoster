using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSocialPoster.Core.Models
{
    public class Post : BaseEntity
    {
        [DisplayName("Treść posta")]
        public string PostContent { get; set; }

        [DisplayName("Adres obrazka")]
        [Url]
        public string PostImg { get; set; }

        [DisplayName("Status")]
        [Optional]
        public bool Status { get; set; }

        [DisplayName("Kategoria")]
        [Optional]
        public string Category { get; set; }

        [DisplayName("Profil")]
        [Optional]
        public string Profile { get; set; }

    }
}
