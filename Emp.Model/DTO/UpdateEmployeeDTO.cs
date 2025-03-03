﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emp.Model.DTO
{
    public class UpdateEmployeeDTO
    {
        public int EmpId { get; set; }
        public string EmpName { get; set; }
        public string Email { get; set; }
        public double Salary { get; set; }
        //Foreign Key 
        [ForeignKey("Department")]
        public int DepId { get; set; }
    }
}
