using Luval.Framework.Data.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.Coach.Data.Entities
{
    public class MetricCategory : StringAuditEntry
    {
        public MetricCategory()
        {
        }

        [Required, MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }

        public string Color { get; set; }
        public string IconUrl { get; set; }

        public bool IsUserCategory { get; set; }

        [Index]
        public string? AccountId { get; set; }


        private static string GetJsonValues()
        {
            return @"
[
    {
        ""Name"": ""Health"",
        ""Description"": ""Maintaining and improving physical well-being."",
        ""Color"": ""#F28D35"",
        ""IconUrl"": ""bi-heart-fill""
    },
    {
        ""Name"": ""Nutrition"",
        ""Description"": ""Dietary habits and food intake."",
        ""Color"": ""#D83367"",
        ""IconUrl"": ""bi-apple""
    },
    {
        ""Name"": ""Sports"",
        ""Description"": ""Engaging in physical activities & games."",
        ""Color"": ""#4FACFE"",
        ""IconUrl"": ""bi-football""
    },
    {
        ""Name"": ""Social"",
        ""Description"": ""Interacting and connecting with others."",
        ""Color"": ""#9055A2"",
        ""IconUrl"": ""bi-people-fill""
    },
    {
        ""Name"": ""Study"",
        ""Description"": ""Learning and academic progress."",
        ""Color"": ""#FFC75F"",
        ""IconUrl"": ""bi-book-fill""
    },
    {
        ""Name"": ""Finance"",
        ""Description"": ""Managing money and investments."",
        ""Color"": ""#0B3D91"",
        ""IconUrl"": ""bi-wallet-fill""
    },
    {
        ""Name"": ""Work"",
        ""Description"": ""Professional tasks and objectives."",
        ""Color"": ""#F0A500"",
        ""IconUrl"": ""bi-briefcase-fill""
    },
    {
        ""Name"": ""Home"",
        ""Description"": ""Household chores and improvements."",
        ""Color"": ""#00C9A7"",
        ""IconUrl"": ""bi-house-door-fill""
    },
    {
        ""Name"": ""Quit a bad habit"",
        ""Description"": ""Eliminating detrimental behaviors."",
        ""Color"": ""#C34A36"",
        ""IconUrl"": ""bi-x-circle-fill""
    },
    {
        ""Name"": ""Other"",
        ""Description"": ""Miscellaneous activities and tasks."",
        ""Color"": ""#6B4226"",
        ""IconUrl"": ""bi-three-dots""
    }
]

";
        }

        internal static List<MetricCategory> GetInitialValues()
        {
            return JsonConvert.DeserializeObject<List<MetricCategory>>(GetJsonValues());
        }

    }
}
