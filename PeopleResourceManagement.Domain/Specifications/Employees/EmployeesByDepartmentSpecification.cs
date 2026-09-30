using PeopleResourceManagement.Domain.Entities;

namespace PeopleResourceManagement.Domain.Specifications.Employees;

public class EmployeesByDepartmentSpecification : BaseSpecification
{
    public EmployeesByDepartmentSpecification(string department)
        : base(employee => employee.Department == department)
    {
    }
}

