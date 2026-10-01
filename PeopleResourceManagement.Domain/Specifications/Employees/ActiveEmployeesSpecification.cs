using PeopleResourceManagement.Domain.Entities;

namespace PeopleResourceManagement.Domain.Specifications.Employees;

public class ActiveEmployeesSpecification
    : BaseSpecification<Employee>
{
    public ActiveEmployeesSpecification()
        : base(employee => employee.IsActive)
    {
    }
}

