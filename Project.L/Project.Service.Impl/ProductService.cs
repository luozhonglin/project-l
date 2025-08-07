using Project.DAO;
using Project.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Impl
{
    public class ProductService : IProductService
    {
        /// <summary>
        /// ProductRepository
        /// </summary>
        /// 
        public IProductDAO ProductDAO { get; set; }

        public Task<IEnumerable<Product>> GetActiveProductsAsync()
        {
            return ProductDAO.GetActiveProductsAsync();
        }
    }
}
