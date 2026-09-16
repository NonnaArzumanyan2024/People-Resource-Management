using System.Linq.Expressions;
using People_Specification.Api.Models;

namespace People_Specification.Api.Specifications;

public interface ISpecification
{
    Expression<Func<Employee, bool>> Criteria { get; }
}