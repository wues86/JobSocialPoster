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
        public string title { get; set; }

        [DisplayName("Treść posta")]
        public string postContent { get; set; }

        [DisplayName("Adres obrazka")]
        [Url]
        public string postImg { get; set; }

        [DisplayName("Link")]
        [Optional]
        [Url]
        public string url { get; set; }

        [DisplayName("Link skrócony")]
        [Optional]
        [Url]
        public string urlShort { get; set; }

        [DisplayName("Status")]
        [Optional]
        public bool status { get; set; }




        /*public Post(string postContent, string postImg)
        {
            this.postContent = postContent;
            this.postImg = postImg;
        }*/

    }
}
