using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Entities
{
    [Dapper.Contrib.Extensions.Table("ProductType")]
    public class ProductType
    {
        [ExplicitKey]
        public int Id { get; set; }
        public  string typename { get; set; }
        public string? typesource { get; set; }

    }
}
