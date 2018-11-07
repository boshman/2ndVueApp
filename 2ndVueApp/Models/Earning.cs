using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2ndVueApp.Models
{
    public class Earning
    {
        public int EarningId { get; set; }
        public DateTime EarnedDate { get; set; }
        public int EarnerId { get; set; }
        public Earner Earner { get; set; }
        public int RuleId { get; set; }
        public Rule Rule { get; set; }
        public int Points { get; set; }
        public string Comment { get; set; }
    }
}
