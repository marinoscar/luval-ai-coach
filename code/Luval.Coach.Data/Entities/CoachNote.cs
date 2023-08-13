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
    public class CoachNote : IdentityEntity
    {
        [Required, ForeignKey("Metric")]
        public ulong MetricId { get; set; }

        public virtual Metric Metric { get; set; }

        [Required]
        public DateTime UtcDateTime { get; set; }

        [Required, MaxLength(2500)]
        public string Note { get; set; }

        public short? FeelingScore { get; set; }

        public virtual ICollection<CoachNoteResponse> Responses { get; set; }
    }
}
