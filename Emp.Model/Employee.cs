using System.ComponentModel.DataAnnotations;

namespace Emp.Model
{
    public class Employee
    {
        [Key]
        public int EmpId { get; set; }
        public string EmpName { get; set; }
        public string Email { get; set; }
        public double Salary { get; set; }
        //Foreign Key 
        public int DepId { get; set; }
        //Navigation property
        public Department Department { get; set; }
    }
}
