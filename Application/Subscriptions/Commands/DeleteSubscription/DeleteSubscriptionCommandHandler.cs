using Application.Common.Interfaces;
using ErrorOr;
using MediatR;

namespace Application.Subscriptions.Commands.DeleteSubscription;

public class DeleteSubscriptionCommandHandler: IRequestHandler<DeleteSubscriptionCommand, ErrorOr<Deleted>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGymRepository _gymRepository;
    private readonly IAdminRepository _adminRepository;
    private readonly ISubscriptionRepository _subscriptionRepository;

    public DeleteSubscriptionCommandHandler(ISubscriptionRepository subscriptionRepository, IAdminRepository adminRepository, IGymRepository gymRepository, IUnitOfWork unitOfWork)
    {
        _subscriptionRepository = subscriptionRepository;
        _adminRepository = adminRepository;
        _gymRepository = gymRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Deleted>> Handle(DeleteSubscriptionCommand command, CancellationToken cancellationToken)
    {
        var subscription = await _subscriptionRepository.GetByIdAsync(command.SubscriptionId);
        
        if (subscription is null)
        {
           return Error.NotFound(description: "Subscription not found");
        }
        var admin = await _adminRepository.GetByIdAsync(subscription.AdminId);

        if (admin is null)
        {
            return Error.NotFound(description: "Admin not found");
        }
        
        admin.DeleteSubscription(command.SubscriptionId);
        
        var gymsToDelete = await _gymRepository.ListBySubscriptionIdAsync(command.SubscriptionId);
        
        await _adminRepository.UpdateAsync(admin);
        await _subscriptionRepository.RemoveSubscriptionAsync(subscription);
        await _gymRepository.RemoveRangeAsync(gymsToDelete);
        await _unitOfWork.CommitChangesAsync();

        return Result.Deleted;

    }
}