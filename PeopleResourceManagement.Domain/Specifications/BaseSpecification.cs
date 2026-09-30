using System.Linq.Expressions;
using PeopleResourceManagement.Domain.Entities;

namespace PeopleResourceManagement.Domain.Specifications;

public abstract class BaseSpecification : ISpecification
{
    public Expression<Func<Employee, bool>> Criteria { get; }

    protected BaseSpecification(Expression<Func<Employee, bool>> criteria)
    {
        Criteria = criteria;
    }
}

