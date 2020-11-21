using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSocialPoster.Core.Models
{
    public class Profile : BaseEntity
    {

        [DisplayName("Nazwa profilu")]
        public string Name { get; set; }
        [DisplayName("Plik CSV")]
        public string  File { get; set; }
        [DisplayName("Id w SocialPilot")]
        public string SocialpilotId { get; set; }
        [DisplayName("Profil aktywny")]
        public bool IsActive { get; set; }
        [DisplayName("Waga")]
        public int Weight { get; set; }
        [Url]
        [DisplayName("Adres URL")]
        public string Url { get; set; }
        [DisplayName("Import z CSV")]
        public bool ImportCsv { get; set; }
        [DisplayName("Kategoria")]
        public string Category { get; set; }

    }
}
