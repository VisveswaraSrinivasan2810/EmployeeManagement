using Emp.Data.Repositories.IRepository;
using Emp.Data.Repositories.Services;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emp.Data.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EmployeeDbContext _dbContext;
        private IDbContextTransaction _transaction;
        private readonly Dictionary<Type,Object> _repositories;
        public UnitOfWork(EmployeeDbContext dbContext)
        {
            _dbContext = dbContext;
            _repositories = new Dictionary<Type,Object>();
        }

        public async Task BeginTransactionAsync()
        {
           _transaction = await _dbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await _transaction.CommitAsync();
            }
            catch
            {
                await _transaction.RollbackAsync();
                throw;
            }
            finally
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool _disposed=false;
        protected virtual void Dispose(bool disposing)
        {
            if(!_disposed)
            {
                if(disposing)
                {
                    _dbContext.Dispose();
                }
            }
            _disposed = true;
        }

        public IGenericRepository<T> GetRepository<T>() where T : class
        {
            if(_repositories.ContainsKey(typeof(T)))
            {
                return _repositories[typeof(T)] as IGenericRepository<T>;
            }
            var repository = new GenericServiceRepository<T>(_dbContext);
            _repositories.Add(typeof(T), repository);
            return  repository;
        }

        public async Task RollBackTransactionAsync()
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }

        public async Task SaveTransactionAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
