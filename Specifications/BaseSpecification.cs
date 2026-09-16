using System.Linq.Expressions;
using People_Specification.Api.Models;

namespace People_Specification.Api.Specifications;

public abstract class BaseSpecification : ISpecification
{
    public Expression<Func<Employee, bool>> Criteria { get; }

    protected BaseSpecification(Expression<Func<Employee, bool>> criteria)
    {
        Criteria = criteria;
    }
}