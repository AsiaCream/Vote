using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vote.Models
{
    public class ComputerIP
    {
        public int Id { get; set; }
        public string IP { get; set; }//投票IP地址
        public DateTime First { get; set; }//第一次时间
        public DateTime Second { get; set; }//第二次时间
    }
}
