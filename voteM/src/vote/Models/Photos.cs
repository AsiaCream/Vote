using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vote.Models
{
    public class Photos
    {
        public int Id { get; set; }
        public string PhotoName { get; set; }
        public DateTime DateTime { get; set; }
        public string Author { get; set; }
        public string Discription { get; set; }
        public string Path { get; set; }
    }
}
