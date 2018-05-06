using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentEdu.Model
{
    public class StudentScore
    {
        public string Id { get; set; }

        public string CardNo { get; set; }

        public long Score { get; set; }

        public DateTime Createtime { get; set; }
    }
}
