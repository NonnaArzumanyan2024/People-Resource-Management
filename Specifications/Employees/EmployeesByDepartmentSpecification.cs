using People_Specification.Api.Models;

namespace People_Specification.Api.Specifications.Employees;

public class EmployeesByDepartmentSpecification : BaseSpecification
{
    public EmployeesByDepartmentSpecification(string department)
        : base(employee => employee.Department == department)
    {
    }
}
