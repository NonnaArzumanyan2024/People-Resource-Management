using MediatR;
using PeopleResourceManagement.Domain.Repositories;
using DomainLog = PeopleResourceManagement.Domain.Entities.Log;
using PeopleResourceManagement.Domain.UnitOfWork;

namespace PeopleResourceManagement.Application.Logs.Commands.CreateOrUpdateUnitLog;

public class CreateOrUpdateUnitLogCommandHandler : IRequestHandler<CreateOrUpdateUnitLogCommand>
{
    private readonly ILogRepository _logRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateOrUpdateUnitLogCommandHandler(ILogRepository logRepository, IUnitOfWork unitOfWork)
    {
        _logRepository = logRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(CreateOrUpdateUnitLogCommand request, CancellationToken cancellationToken)
    {
        var existingLog = await _logRepository.GetByUnitIdAsync(request.UnitId);

        if (existingLog is null)
        {
            var log = new DomainLog
            {
                UnitId = request.UnitId,
                Descriptor = request.Descriptor,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _logRepository.AddAsync(log);
        }
        else
        {
            if (existingLog.Descriptor == request.Descriptor)
            {
                return;
            }

            existingLog.Descriptor = request.Descriptor;
            existingLog.UpdatedAt = DateTime.UtcNow;

            await _logRepository.UpdateAsync(existingLog);
        }

        await _unitOfWork.SaveChangesAsync();
    }
}