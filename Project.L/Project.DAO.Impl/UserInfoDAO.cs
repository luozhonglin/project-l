using Project.DAO.Impl.DataBase;
using Project.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAO.Impl
{
    public class UserInfoDAO : BaseRepository<UserInfo>, IUserInfoDAO
    {
        private const string TableName = "UserInfo";

        public UserInfoDAO(DapperContext context) : base(context) { }


        public async Task<UserInfo?> GetInfoAsync(string username, string password)
        {

                var sql = $"SELECT * FROM {TableName} WHERE username = @username and password=@password";
                return await QueryFirstOrDefaultAsync(sql, new { username, password });
  
        }
    }
}
