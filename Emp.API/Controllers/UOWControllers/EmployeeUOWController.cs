using AutoMapper;
using Emp.Data.Repositories.UnitOfWork;
using Emp.Model;
using Emp.Model.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Emp.API.Controllers.UOWControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeUOWController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public EmployeeUOWController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;   
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var employees = await _unitOfWork.GetRepository<Employee>().GetAll();
            return Ok(employees);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEmployeeDTO employeeDTO)
        {
            try
            {
                using (var transaction = _unitOfWork.BeginTransactionAsync())
                {
                    var emp = _mapper.Map<Employee>(employeeDTO);
                    if(emp!=null)
                    {
                        var dep = await _unitOfWork.GetRepository<Department>().GetById(emp.DepId);
                        emp.Department = dep;
                    }

                    var empResult =  _unitOfWork.GetRepository<Employee>().Create(emp);
                    await _unitOfWork.SaveTransactionAsync();
                    await _unitOfWork.CommitTransactionAsync();
                    return Ok(empResult);
                }
            }
            catch
            {
                await _unitOfWork.RollBackTransactionAsync();
                throw;
            }
        }
    }
}
