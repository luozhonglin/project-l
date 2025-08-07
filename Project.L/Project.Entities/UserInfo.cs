using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Entities
{
    [Dapper.Contrib.Extensions.Table("UserInfo")]
    public class UserInfo
    {
        [ExplicitKey]
        public Guid id { get; set; }
        public  string username { get; set; }
        public string? password { get; set; }
        public string companyid { get; set; }

    }
}
