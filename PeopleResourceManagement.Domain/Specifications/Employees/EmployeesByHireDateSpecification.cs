using PeopleResourceManagement.Domain.Entities;

namespace PeopleResourceManagement.Domain.Specifications.Employees;

public class EmployeesByHireDateSpecification : BaseSpecification
{
    public EmployeesByHireDateSpecification(DateTime hireDate)
        : base(employee => employee.HireDate >= hireDate)
    {
    }
}

