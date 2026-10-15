namespace PeopleResourceManagement.Application.Exceptions;

public class DuplicateUnitNameException : Exception
{
    public DuplicateUnitNameException(string unitName)
        : base($"Unit with name '{unitName}' already exists.")
    {
    }
}