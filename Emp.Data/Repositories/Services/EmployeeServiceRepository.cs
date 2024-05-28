using Emp.Data.Repositories.IRepository;
using Emp.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emp.Data.Repositories.Services
{
    public class EmployeeServiceRepository :GenericServiceRepository<Employee>,IEmployeeRepository
    {
        private readonly EmployeeDbContext _dbContext;
        public EmployeeServiceRepository(EmployeeDbContext dbContext) : base(dbContext) 
        {

            _dbContext = dbContext;

        }
        public async Task Update(Employee entity)
        {
            _dbContext.Employees.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
