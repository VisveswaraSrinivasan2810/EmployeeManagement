using AutoMapper;
using Emp.Data.Repositories.IRepository;
using Emp.Model;
using Emp.Model.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Emp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IGenericRepository<Employee> _repository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;
        public EmployeeController(IGenericRepository<Employee> repository ,IEmployeeRepository employeeRepository,IDepartmentRepository departmentRepository,IMapper mapper)
        {
            _repository = repository;
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;   
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployee()
        {
            var employees = await _repository.GetAll();
            return Ok(employees);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetByIdEmployee(int id)
        {
            var employee = await _repository.GetById(id);   
            return Ok(employee);    
        }
        [HttpPost]
            public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeDTO employeeDTO)
            {
                if (employeeDTO == null)
                {
                    return BadRequest();
                }
                var emp = _mapper.Map<Employee>(employeeDTO);   
                if(emp != null)
                {
                    var dep = await _departmentRepository.GetById(emp.DepId);
                    emp.Department = dep;
                }
                await _repository.Create(emp);
                return CreatedAtAction("GetByIdEmployee", new { id = emp.EmpId }, emp);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id , [FromBody] UpdateEmployeeDTO employeeDTO)
        {
            if(employeeDTO == null || employeeDTO.EmpId != id)
            {
                return BadRequest();
            }
            var emp = _mapper.Map<Employee>(employeeDTO);
          
            await _employeeRepository.Update(emp);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var emp = await _repository.GetById(id);
            if(emp == null)
            {
                return NotFound();
            }
            await _repository.Delete(emp);
            return NoContent();
        }
    }
}
