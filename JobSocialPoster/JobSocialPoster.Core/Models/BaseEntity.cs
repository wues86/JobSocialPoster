using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration.Attributes;

namespace JobSocialPoster.Core.Models
{
    public abstract class BaseEntity
    {
        [Optional]
        public string Id { get; set; }
        [Optional]
        public DateTimeOffset CreationTime { get; set; }

        public BaseEntity()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreationTime = DateTime.Now;
        }
    }
}
