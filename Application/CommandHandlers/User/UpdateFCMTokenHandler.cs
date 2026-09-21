using Domain.Configuration;
using Domain.Repositories.Command;
using static Domain.Configuration.Sql;

namespace Application.CommandHandlers;

public class UpdateFCMTokenHandler : IRequestHandler<UpdateFCMTokenCommand, bool>
{
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;

    public UpdateFCMTokenHandler(IUnitOfWorkFactory unitOfWorkFactory)
    {
        _unitOfWorkFactory = unitOfWorkFactory;
    }

    public async Task<bool> Handle(UpdateFCMTokenCommand request, CancellationToken cancellationToken)
    {
        var unitOfWork = _unitOfWorkFactory.Create();
        try
        {
            var _userCommandRepository = unitOfWork.GetRepository<IUserCommandRepository>();
            var result = await _userCommandRepository.UpdateFCMToken(request.UserId, request.FCMToken);
            unitOfWork.SaveChanges();
            return result > 0;
        }
        catch (Exception)
        {
            unitOfWork.UndoChanges();
            throw;
        }
    }
}
