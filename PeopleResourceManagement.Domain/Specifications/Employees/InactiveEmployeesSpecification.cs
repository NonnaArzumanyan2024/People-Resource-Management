using PeopleResourceManagement.Domain.Entities;

namespace PeopleResourceManagement.Domain.Specifications.Employees;

public class InactiveEmployeesSpecification
    : BaseSpecification<Employee>
{
    public InactiveEmployeesSpecification()
        : base(employee => !employee.IsActive)
    {
    }
}
