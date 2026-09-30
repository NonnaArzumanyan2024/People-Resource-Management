using PeopleResourceManagement.Domain.Entities;

namespace PeopleResourceManagement.Domain.Specifications.Employees;

public class EmployeesByPositionSpecification : BaseSpecification
{
    public EmployeesByPositionSpecification(string position)
        : base(employee => employee.Position == position)
    {
    }
}

