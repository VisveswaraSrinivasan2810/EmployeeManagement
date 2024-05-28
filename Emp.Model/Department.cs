using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emp.Model
{
    public class Department
    {
        [Key]
        public int DepId { get; set; }
        public string DepName { get; set; } 
    }
}
