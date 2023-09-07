namespace Application.CommandHandlers;

public class UpdateAccountHandler : IRequestHandler<UpdateAccountCommand, UpdateAccountResponse>
{

    private readonly IUnitOfWorkFactory _unitOfWorkFactory;
    private readonly IMapper _mapper;
    private readonly IAccountQueryRepository _accountQueryRepository;

    public UpdateAccountHandler(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper, IAccountQueryRepository accountQueryRepository)
    {
        _unitOfWorkFactory = unitOfWorkFactory;
        _mapper = mapper;
        _accountQueryRepository = accountQueryRepository;
    }

    public async Task<UpdateAccountResponse> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
    {
        UpdateAccountResponse response = new UpdateAccountResponse();

        var unitOfWork = _unitOfWorkFactory.Create();
        try
        {
            var account = await _accountQueryRepository.FindById(request.AccountId);
            if (account is not null)
            {
                var userId = account.UserId;
                account = _mapper.Map<Domain.Entities.Account>(request);
                account.UserId = userId;

                var _accountCommandRepository = unitOfWork.GetRepository<IAccountCommandRepository>();

                int result = await _accountCommandRepository.Update(account);

                unitOfWork.SaveChanges();
                response.AccountId = result;
                response.Success = true;
                response.Message = "Account updated succesfully";
            }
            else
            {
                response.Success = false;
                response.Message = "Account not found";
            }
        }
        catch (Exception)
        {
            unitOfWork.UndoChanges();
            response.Success = false;
            response.Message = "Error to update Account";
        }
        return response;
    }
}
