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
    public class DepartmentServiceRepository : GenericServiceRepository<Department>,IDepartmentRepository
    {
        private readonly EmployeeDbContext _dbContext;
        public DepartmentServiceRepository(EmployeeDbContext dbContext) : base(dbContext) 
        {

            _dbContext = dbContext;

        }
        public async Task Update(Department entity)
        {
            _dbContext.Departments.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
