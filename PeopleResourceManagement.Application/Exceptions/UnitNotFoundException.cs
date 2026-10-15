namespace PeopleResourceManagement.Application.Exceptions;

public class UnitNotFoundException : Exception
{
    public UnitNotFoundException(int unitId)
        : base($"Unit with id '{unitId}' was not found.")
    {
    }
}