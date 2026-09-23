using People_Specification.Api.Models;

namespace People_Specification.Api.Specifications.Employees;

public class InactiveEmployeesSpecification : BaseSpecification
{
    public InactiveEmployeesSpecification()
        : base(employee => !employee.IsActive)
    {
    }
}
