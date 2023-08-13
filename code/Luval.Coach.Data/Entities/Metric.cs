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
    public class Metric : IdentityEntity
    {

        [Required, Index]
        public string AccountId { get; set; }

        [Required, MaxLength(255)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }

        [Required, MaxLength(15)]
        public MetricType MetricType { get; set; }

        [Required, MaxLength(50)]
        public string MetricUnitOfMeasure { get; set; }

        [Required, MaxLength(100)]
        public string MetricChronExpression { get; set; }

        [MaxLength(100)]
        public string ReminderChronExpression { get; set; }

        public double? TargetDaily { get; set; }
        public double? TargetWeekly { get; set; }
        public double? TargetMonthly { get; set; }
        public double? TargetYearly { get; set; }
        public double? TargetSingle { get; set; }
        public double? TargetAlltime { get; set; }

        public bool SendCoachingMessages { get; set; }

        [Required]
        public DateTime UtcStartDate { get; set; }
        public DateTime? UtcEndDate { get; set; }
    }

    public enum MetricType
    {
        None, Binary, Numeric, Time
    }
}
