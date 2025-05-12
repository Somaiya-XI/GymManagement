using Application.Common.Interfaces;
using Domain.Gyms;
using ErrorOr;
using MediatR;

namespace Application.Gyms.Commands.AddTrainer;

public class AddTrainerCommandHandler : IRequestHandler<AddTrainerCommand, ErrorOr<Success>>
{    private readonly IGymRepository _gymsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddTrainerCommandHandler(IGymRepository gymsRepository, IUnitOfWork unitOfWork)
    {
        _gymsRepository = gymsRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Success>> Handle(AddTrainerCommand command, CancellationToken cancellationToken)
    {
        var gym = await _gymsRepository.GetByIdAsync(command.GymId);

        if (gym is null)
        {
            return Error.NotFound(description: "Gym not found");
        }
        
        var addTrainerResult = gym.AddTrainer(command.TrainerId);

        if (addTrainerResult.IsError)
        {
            return addTrainerResult.Errors;
        }
        
        await _gymsRepository.UpdateGymAsync(gym);
        await _unitOfWork.CommitChangesAsync();
        
        return Result.Success;
    }
    }