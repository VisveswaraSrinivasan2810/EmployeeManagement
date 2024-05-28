using Emp.Data.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emp.Data.Repositories.Services
{
    public class GenericServiceRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly EmployeeDbContext _dbContext;
        public GenericServiceRepository(EmployeeDbContext dbContext)
        {

            _dbContext = dbContext;

        }
        public async Task Create(T entity)
        {
             await _dbContext.Set<T>().AddAsync(entity);
             await Save();
        }

        public async Task Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await Save();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            var res= await _dbContext.Set<T>().FindAsync(id);
            return res!;
        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
