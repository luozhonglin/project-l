using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
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

        public async Task<T?> GetByIdAsync(int id)
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
    }
}
