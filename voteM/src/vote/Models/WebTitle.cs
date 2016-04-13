using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vote.Models
{
    public class WebTitle
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string location { get; set; }// 标题对应的网页位置，固定的不能改变
        public string type { get; set; }//大标题？ 还是小标题
    }
}
