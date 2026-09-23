using People_Specification.Api.Models;

namespace People_Specification.Api.Specifications.Employees;

public class ActiveEmployeesSpecification : BaseSpecification
{
    public ActiveEmployeesSpecification()
        : base(employee => employee.IsActive)
    {
    }
}
