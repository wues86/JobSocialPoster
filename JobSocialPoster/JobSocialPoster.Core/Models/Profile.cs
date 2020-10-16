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
        [DisplayName("Plik CSV do importu postów")]
        public string  File { get; set; }
        [DisplayName("Id profilu w SocialPilot")]
        public string SocialpilotId { get; set; }
        [DisplayName("Zakończono eksport postów?")]
        public bool IsSent { get; set; }
        [DisplayName("Profil aktywny?")]
        public bool IsActive { get; set; }
        [DisplayName("Waga profilu")]
        public int Weight { get; set; }
        [Url]
        [DisplayName("Adres URL profilu")]
        public string Url { get; set; }
        [DisplayName("Importować posty z CSV?")]
        public bool ImportCsv { get; set; }
        [DisplayName("Kategoria profilu")]
        public string Category { get; set; }

    }
}
