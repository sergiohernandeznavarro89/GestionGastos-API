namespace Application.CommandHandlers.Account;

public class DeleteAccountHandler : IRequestHandler<DeleteAccountCommand, DeleteAccountResponse>
{

    private readonly IUnitOfWorkFactory _unitOfWorkFactory;
    private readonly IMapper _mapper;
    private readonly IAccountQueryRepository _accountQueryRepository;

    public DeleteAccountHandler(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper, IAccountQueryRepository accountQueryRepository)
    {
        _unitOfWorkFactory = unitOfWorkFactory;
        _mapper = mapper;
        _accountQueryRepository = accountQueryRepository;
    }

    public async Task<DeleteAccountResponse> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
    {
        DeleteAccountResponse response = new DeleteAccountResponse();

        var unitOfWork = _unitOfWorkFactory.Create();

        try
        {
            var accountTask = _accountQueryRepository.FindById(request.AccountId);
            await Task.WhenAll(accountTask);

            var account = accountTask.Result;

            if (account is not null)
            {
                var _accountCommandRepository = unitOfWork.GetRepository<IAccountCommandRepository>();

                var result = await _accountCommandRepository.Delete(account);

                unitOfWork.SaveChanges();

                response.Success = true;
                response.Message = "Account deleted succesfully";
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
            response.Message = "Error to Delete Account";
        }

        return response;
    }
}
