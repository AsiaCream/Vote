using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace vote.Models
{
    public class Photos
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime DateTime { get; set; } 
        public string Describe{ get; set; }
        public string Path { get; set; }
        public int Priority { get; set; }

        [ForeignKey("Author")]
        public int AuthorId { get; set; }
        public virtual Author Author { get; set; }
    }
}
