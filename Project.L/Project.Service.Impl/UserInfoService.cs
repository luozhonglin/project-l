using Project.DAO;
using Project.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Impl
{
    public class UserInfoService:IUserInfoService
    {
        public IUserInfoDAO UserInfoDAO { get; set; }

        public async Task<UserInfo?> GetInfoAsync(string username, string password)
        { 
          return await UserInfoDAO.GetInfoAsync(username, password);


        }
    }
}
