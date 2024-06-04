using AutoMapper;
using AutoMapper.Execution;
using Emp.Data.Repositories.IRepository;
using Emp.Model;
using Emp.Model.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace Emp.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IGenericRepository<Department> _repository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;
        
        public DepartmentController(IGenericRepository<Department> repository,IDepartmentRepository departmentRepository,IMapper mapper)
        {
            _repository = repository;
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> GetAllDepartments() 
        {
           var departments = await _repository.GetAll();
            return Ok(departments); 
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetByIdDepartment(int id)
        {
            var department = await _repository.GetById(id);
            return Ok(department);
        }
        [HttpPost]  
        public async Task<IActionResult> CreateDepartment([FromBody] CreateDepartmentDTO departmentDTO)
        {
            if (departmentDTO == null)
            {
                return BadRequest();
            }
            var dep = _mapper.Map<Department>(departmentDTO);
            await _repository.Create(dep);
            return CreatedAtAction("GetByIdDepartment", new { id = dep.DepId }, dep);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id , [FromBody] UpdateDepartmentDTO departmentDTO)
        {
            if(departmentDTO == null || departmentDTO.DepId!=id)
            {
                return BadRequest();
            }
            var dep = _mapper.Map<Department>(departmentDTO);
            await _departmentRepository.Update(dep);
            return NoContent(); 
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var dep = await _repository.GetById(id);
            if(dep == null)
            {
                return NotFound();
            }
            await _repository.Delete(dep);
            return NoContent(); 
        }
    }
}
