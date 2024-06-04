using Emp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emp.Data.Repositories.IRepository
{
    public interface IRoleService
    {
        Task<List<Role>> GetRolesAsync();
        Task<List<string>> GetUserRolesAsync(string email);
        Task<List<string>> AddRolesAsync(string[] roles);
        Task<bool> AddUserRolesAsync(string email, string[] roles);
    }
}
