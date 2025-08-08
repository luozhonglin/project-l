using Project.Entities;
using Project.Entities.DTO;
using Project.Entities.DTO.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetActiveProductsAsync();

        Task<PageResponse<ProductTypeDTO>> GetPagedProductTypeList(PageRequest request, string whereId);
    }
}
