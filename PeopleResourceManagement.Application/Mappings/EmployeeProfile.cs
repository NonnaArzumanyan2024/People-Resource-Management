using AutoMapper;
using PeopleResourceManagement.Application.DTOs;
using PeopleResourceManagement.Domain.Entities;

namespace PeopleResourceManagement.Application.Mappings;

public class EmployeeProfile : Profile
{
    public EmployeeProfile()
    {
        CreateMap<Employee, EmployeeDto>();
        CreateMap<EmployeeDto, Employee>();
    }
}


