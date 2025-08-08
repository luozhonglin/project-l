using Dapper;
using Project.DAO.Impl.DataBase;
using Project.Entities;
using Project.Entities.DTO;
using Project.Entities.DTO.Common;
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

        /// <summary>
        /// 多表联合分页
        /// </summary>
        /// <param name="request"></param>
        /// <param name="whereId"></param>
        /// <returns></returns>
        public async Task<PageResponse<ProductTypeDTO>> GetPagedProductTypeList(PageRequest request,string whereId)
        {


            // 动态构建WHERE条件
            var whereClauses = new List<string>();
            var parameters = new DynamicParameters();

            if (!string.IsNullOrWhiteSpace(whereId))
            {
                whereClauses.Add("a.Id=@Id");
                parameters.Add("Id", $"{whereId}");
            }

            //if (!string.IsNullOrWhiteSpace(searchRequest.CustomerName))
            //{
            //    whereClauses.Add("c.Name LIKE @CustomerName");
            //    parameters.Add("CustomerName", $"%{searchRequest.CustomerName}%");
            //}

            //if (searchRequest.MinAmount.HasValue)
            //{
            //    whereClauses.Add("o.TotalAmount >= @MinAmount");
            //    parameters.Add("MinAmount", searchRequest.MinAmount);
            //}

            //if (searchRequest.StartDate.HasValue)
            //{
            //    whereClauses.Add("o.OrderDate >= @StartDate");
            //    parameters.Add("StartDate", searchRequest.StartDate);
            //}

            var whereSql = whereClauses.Any() ? "WHERE " + string.Join(" AND ", whereClauses) : "";


            // 基础查询SQL（多表联合）
            var baseSql = @$"select a.Id,a.Name,b.typename,b.typesource from products a left join producttype b 
                            on a.typeId=b.id {whereSql}";

            // 计数SQL
            var countSql = @$"select count(1) from products a left join producttype b 
                            on a.typeId=b.id {whereSql}";
            var data = await QueryPagedAsync<ProductTypeDTO>(baseSql, countSql, request, parameters);
            return data;


        }
    }
}
