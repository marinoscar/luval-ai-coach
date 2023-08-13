using Luval.Framework.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Luval.Framework.Core;
using System.ComponentModel.DataAnnotations.Schema;

namespace Luval.Coach.Data.Entities
{
    public class ApplicationKey : StringAuditEntry
    {

        [Index, Required]
        public string AccountId { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }
        [Required, MaxLength(500)]
        public string EncrypytedValue { get; set; }

        public void Encrypt(string passcode, string value)
        {
            EncrypytedValue = value.Encrypt(passcode);
        }

        public string Decrypt(string passcode)
        {
            return EncrypytedValue.Decrypt(passcode);
        }
    }
}
