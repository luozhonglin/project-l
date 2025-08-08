using Dapper;
using Dapper.Contrib.Extensions;
using Project.Entities.DTO.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAO.Impl.DataBase
{
    public abstract class BaseRepository<T> where T : class
    {
        protected readonly DapperContext _context;

        protected BaseRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _context.Connection.GetAsync<T>(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Connection.GetAllAsync<T>();
        }

        public async Task<int> AddAsync(T entity)
        {
            return await _context.Connection.InsertAsync(entity);
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            return await _context.Connection.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            return await _context.Connection.DeleteAsync(entity);
        }

        protected async Task<IEnumerable<T>> QueryAsync(string sql, object? param = null)
        {
            return await _context.Connection.QueryAsync<T>(sql, param);
        }

        protected async Task<T?> QueryFirstOrDefaultAsync(string sql, object? param = null)
        {
            return await _context.Connection.QueryFirstOrDefaultAsync<T>(sql, param);
        }

        protected async Task<int> ExecuteAsync(string sql, object? param = null)
        {
            return await _context.Connection.ExecuteAsync(sql, param);
        }

        /// <summary>
        /// 分页公共引擎
        /// </summary>
        /// <typeparam name="T">分页实例类型</typeparam>
        /// <param name="baseSql">查询sql</param>
        /// <param name="countSql">总条数sql</param>
        /// <param name="pageRequest">分页请求条件</param>
        /// <param name="parameters">条件筛选参数</param>
        /// <returns></returns>
        public async Task<PageResponse<T>> QueryPagedAsync<T>(
        string baseSql,          // 基础查询SQL（不含分页）
        string countSql,          // 计数SQL
        PageRequest pageRequest,  // 分页参数
        object? parameters = null)
        {
            // 1. 获取总记录数
            var totalCount = await _context.Connection.ExecuteScalarAsync<int>(countSql, parameters);

            // 2. 计算分页参数
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageRequest.PageSize);
            var skip = (pageRequest.PageNumber - 1) * pageRequest.PageSize;

            // 3. 构建排序语句
            var orderByClause = string.IsNullOrWhiteSpace(pageRequest.OrderBy)
                ? ""
                : $" ORDER BY {pageRequest.OrderBy} {(pageRequest.IsDescending ? "DESC" : "ASC")}";

            // 4. MySQL分页语法
            var pagedSql = $@"
            {baseSql}
            {orderByClause}
            LIMIT {skip}, {pageRequest.PageSize}";

            // 5. 执行分页查询
            var data = await _context.Connection.QueryAsync<T>(pagedSql, parameters);

            return new PageResponse<T>
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                CurrentPage = pageRequest.PageNumber,
                PageSize = pageRequest.PageSize,
                Data = data
            };
        }

    }
}
