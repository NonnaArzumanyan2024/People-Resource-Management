using People_Specification.Api.Models;

namespace People_Specification.Api.Specifications.Employees;

public class EmployeesByHireDateSpecification : BaseSpecification
{
    public EmployeesByHireDateSpecification(DateTime hireDate)
        : base(employee => employee.HireDate >= hireDate)
    {
    }
}