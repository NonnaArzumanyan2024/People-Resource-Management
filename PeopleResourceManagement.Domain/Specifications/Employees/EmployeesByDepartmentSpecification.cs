using PeopleResourceManagement.Domain.Entities;

namespace PeopleResourceManagement.Domain.Specifications.Employees;

public class EmployeesByDepartmentSpecification
    : BaseSpecification<Employee>
{
    public EmployeesByDepartmentSpecification(string department)
        : base(employee => employee.Department == department)
    {
    }
}

