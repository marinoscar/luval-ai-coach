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
    public class CoachNoteResponse : IdentityEntity
    {
        [Required, ForeignKey("CoachNote")]
        public ulong CoachNoteId { get; set; }

        [Required, MaxLength(2000)]
        public string Response { get; set; }
        public short? FeelingScore { get; set; }
        [Required]
        public bool IsCoachResponse { get; set; }
    }
}
