using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentEdu.Service
{
    public class BaseService
    {
        public static string Connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public string ToGuid(string guid)
        {
            return "{" + guid + "}";
        }
    }
}
