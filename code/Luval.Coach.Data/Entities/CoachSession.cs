using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Luval.Framework.Data.Entities;

namespace Luval.Coach.Data.Entities
{
    public class CoachSession : IdentityEntity
    {
        [Required, ForeignKey("Metric")]
        public ulong MetricId { get; set; }

        public virtual Metric Metric { get; set; }

        [Required]
        public DateTime UtcPublishDate { get; set; }

        public virtual ICollection<CoachNote> Responses { get; set; }
    }
}
