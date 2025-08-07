using Project.DAO.Impl.DataBase;
using Project.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAO.Impl
{
    public class ProductDAO : BaseRepository<Product>, IProductDAO
    {
        private const string TableName = "Products";

        public ProductDAO(DapperContext context) : base(context) { }

        public async Task<IEnumerable<Product>> GetActiveProductsAsync()
        {
            var sql = $"SELECT * FROM {TableName} WHERE IsActive = true";
            return await QueryAsync(sql);
        }

        public async Task<IEnumerable<Product>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice)
        {
            var sql = $"""
            SELECT * FROM {TableName} 
            WHERE Price BETWEEN @minPrice AND @maxPrice
            AND IsActive = true
            """;

            return await QueryAsync(sql, new { minPrice, maxPrice });
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var sql = $"UPDATE {TableName} SET IsActive = false WHERE Id = @id";
            var affected = await ExecuteAsync(sql, new { id });
            return affected > 0;
        }
    }
}
