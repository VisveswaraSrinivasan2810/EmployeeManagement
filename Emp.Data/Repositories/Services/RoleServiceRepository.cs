using Emp.Data.Repositories.IRepository;
using Emp.Model;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emp.Data.Repositories.Services
{
    public class RoleServiceRepository : IRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        public RoleServiceRepository(RoleManager<IdentityRole> roleManager , UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager; 
        }
        public async Task<List<string>> AddRolesAsync(string[] roles)
        {
            var roleList = new List<string>();
            foreach(var role in roles)
            {
                if(!await _roleManager.RoleExistsAsync(role))
                {
                   await _roleManager.CreateAsync(new IdentityRole(role));
                   roleList.Add(role); 
                }
            }
            return roleList;
        }

        public async Task<bool> AddUserRolesAsync(string email, string[] roles)
        {
           var user = await _userManager.FindByNameAsync(email);
           var existRoles = await ExistRolesAsync(roles);
            if(user != null && existRoles.Count == roles.Length)
            {
                var assignRoles = await _userManager.AddToRolesAsync(user, existRoles);
                return assignRoles.Succeeded;
            }
            return false;
        }
        private async Task<List<string>> ExistRolesAsync(string[] roles)
        {
            var rolesList = new List<string>(); 
            foreach(var role in roles)
            {
                var roleExist = await _roleManager.RoleExistsAsync(role);
                if(roleExist)
                {
                    rolesList.Add(role);
                }
            }
            return rolesList;
        }

        public async Task<List<Role>> GetRolesAsync()
        {
            var roleList = _roleManager.Roles.Select(x =>
           new Role { Id = Guid.Parse(x.Id), Name = x.Name }).ToList();
           return roleList;
        }

        public async Task<List<string>> GetUserRolesAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);  
            var userRoles = await  _userManager.GetRolesAsync(user);
            return userRoles.ToList();
        }
    }
}
