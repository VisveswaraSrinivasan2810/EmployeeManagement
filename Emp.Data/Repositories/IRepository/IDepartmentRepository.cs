using Emp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emp.Data.Repositories.IRepository
{
    public interface IDepartmentRepository:IGenericRepository<Department>
    {
        Task Update(Department entity);
    }
}
