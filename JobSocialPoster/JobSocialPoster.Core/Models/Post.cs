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
        [DisplayName("Tytuł posta")]
        [Optional]
        public string Title { get; set; }

        [DisplayName("Treść posta")]
        public string PostContent { get; set; }

        [DisplayName("Adres obrazka")]
        [Url]
        public string PostImg { get; set; }

        [DisplayName("Link")]
        [Optional]
        [Url]
        public string Url { get; set; }

        [DisplayName("Link skrócony")]
        [Optional]
        [Url]
        public string UrlShort { get; set; }

        [DisplayName("Status")]
        [Optional]
        public bool Status { get; set; }

        [DisplayName("Kategoria")]
        [Optional]
        public string Category { get; set; }


        /*public Post(string postContent, string postImg)
        {
            this.postContent = postContent;
            this.postImg = postImg;
        }*/

    }
}
