﻿using Project.Entities;
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
    }
}
