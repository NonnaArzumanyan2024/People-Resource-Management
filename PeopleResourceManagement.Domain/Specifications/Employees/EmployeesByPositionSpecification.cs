using PeopleResourceManagement.Domain.Entities;

namespace PeopleResourceManagement.Domain.Specifications.Employees;

public class EmployeesByPositionSpecification
    : BaseSpecification<Employee>
{
    public EmployeesByPositionSpecification(string position)
        : base(employee => employee.Position == position)
    {
    }
}

