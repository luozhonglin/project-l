using Project.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAO
{
    public interface IUserInfoDAO
    {
        Task<UserInfo?> GetInfoAsync(string username,string password);

        Task<UserInfo?> GetByIdAsync(Guid id);
    }
}
