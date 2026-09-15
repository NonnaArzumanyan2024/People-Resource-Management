using AutoMapper;
using People_Specification.Api.DTOs;
using People_Specification.Api.Models;

namespace People_Specification.Api.Mappings;

public class EmployeeProfile : Profile
{
    public EmployeeProfile()
    {
        CreateMap<Employee, EmployeeDto>();
        CreateMap<EmployeeDto, Employee>();
    }
}