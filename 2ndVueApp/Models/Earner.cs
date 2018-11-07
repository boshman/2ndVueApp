using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2ndVueApp.Models
{
    public class Earner
    {
        public int EarnerId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public ICollection<Earning> Earnings { get; set; }
    }
}
