using People_Specification.Api.Models;

namespace People_Specification.Api.Specifications.Employees;

public class EmployeesByPositionSpecification : BaseSpecification
{
    public EmployeesByPositionSpecification(string position)
        : base(employee => employee.Position == position)
    {
    }
}
