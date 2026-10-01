using System.Linq.Expressions;

namespace PeopleResourceManagement.Domain.Specifications;

public interface ISpecification<T>
{
    Expression<Func<T, bool>> Criteria { get; }
}