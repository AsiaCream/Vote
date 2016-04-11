using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vote.Models
{
    public class Activity
    {
        public int Id { get; set; }
        public string ActivityTitle { get; set; }
        public string Content { get; set; }
        public string Originator { get; set; }
        public DateTime DateTime { get; set; }

    }
}
