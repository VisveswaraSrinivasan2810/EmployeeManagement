using AutoMapper;
using Emp.Model;
using Emp.Model.DTO;

namespace Emp.API.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, CreateEmployeeDTO>().ReverseMap();
            CreateMap<Employee,UpdateEmployeeDTO>().ReverseMap();
            CreateMap<Department,CreateDepartmentDTO>().ReverseMap();
            CreateMap<Department,UpdateDepartmentDTO>().ReverseMap();
        }
    }
}
