using PeopleResourceManagement.Domain.Entities;
using PeopleResourceManagement.Domain.Repositories;
using PeopleResourceManagement.Domain.UnitOfWork;

namespace PeopleResourceManagement.Application.Jobs;

public class CheckEmptyUnitsJob
{
    private readonly IUnitRepository _unitRepository;
    private readonly ILogRepository _logRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CheckEmptyUnitsJob(
        IUnitRepository unitRepository,
        ILogRepository logRepository,
        IUnitOfWork unitOfWork)
    {
        _unitRepository = unitRepository;
        _logRepository = logRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task ExecuteAsync(
        CancellationToken cancellationToken)
    {
        var units =
            await _unitRepository.GetAllWithEmployeesAsync();

        foreach (var unit in units)
        {
            if (!unit.Employees.Any())
            {
                var log = new Log
                {
                    UnitId = unit.Id,
                    Descriptor = "Unit has no employees",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                await _logRepository.AddAsync(log);
            }
        }

        await _unitOfWork.SaveChangesAsync();
    }
}