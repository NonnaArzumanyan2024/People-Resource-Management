using System.Linq.Expressions;
using PeopleResourceManagement.Domain.Entities;

namespace PeopleResourceManagement.Domain.Specifications;

public interface ISpecification
{
    Expression<Func<Employee, bool>> Criteria { get; }
}

