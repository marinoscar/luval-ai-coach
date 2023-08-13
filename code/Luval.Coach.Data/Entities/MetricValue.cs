using Luval.Framework.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.Coach.Data.Entities
{
    public class MetricValue: IdentityEntity
    {

        [Required, ForeignKey("Metric")]
        public ulong MetricId { get; set; }

        public virtual Metric Metric { get; set; }

        [Required]
        public DateTime UtcDateTime { get; set; }

        [Required]
        public double Value { get; set; }

        public short? FeelingScore { get; set; }

        [MaxLength(255)]
        public string? Note { get; set; }

        [MaxLength(50)]
        public string? Sentinment { get; set; }
    }
}
