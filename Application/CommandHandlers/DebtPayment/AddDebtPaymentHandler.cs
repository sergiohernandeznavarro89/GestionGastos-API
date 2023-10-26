using Domain.Entities;

namespace Application.CommandHandlers;

public class AddDebtPaymentHandler : IRequestHandler<AddDebtPaymentCommand, AddDebtPaymentResponse>
{

    private readonly IUnitOfWorkFactory _unitOfWorkFactory;
    private readonly IMapper _mapper;
    private readonly IAccountQueryRepository _accountQueryRepository;
    private readonly IDebtQueryRepository _debtQueryRepository;

    public AddDebtPaymentHandler(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper, IAccountQueryRepository accountQueryRepository, IDebtQueryRepository debtQueryRepository)
    {
        _unitOfWorkFactory = unitOfWorkFactory;
        _mapper = mapper;
        _accountQueryRepository = accountQueryRepository;
        _debtQueryRepository = debtQueryRepository;
    }

    public async Task<AddDebtPaymentResponse> Handle(AddDebtPaymentCommand request, CancellationToken cancellationToken)
    {
        AddDebtPaymentResponse response = new AddDebtPaymentResponse();

        var unitOfWork = _unitOfWorkFactory.Create();
        try
        {
            var _debtPaymentCommandRepository = unitOfWork.GetRepository<IDebtPaymentCommandRepository>();
            var _debtCommandRepository = unitOfWork.GetRepository<IDebtCommandRepository>();
            var _accountCommandRepository = unitOfWork.GetRepository<IAccountCommandRepository>();

            var debtTask = _debtQueryRepository.FindById(request.DebtId);
            var accountTask = _accountQueryRepository.FindById(request.AccountId);

            await Task.WhenAll(debtTask, accountTask);

            var debt = debtTask.Result;
            var account = accountTask.Result;

            if(debt is null)
            {
                response.Success = false;
                response.Message = "Debt not found";
                return response;
            }
            if(account is null)
            {
                response.Success = false;
                response.Message = "Account not found";
                return response;
            }

            var newDebtPayment = new DebtPayment { 
                DebtId = request.DebtId, 
                Date = DateTime.Now, 
                Amount = request.Amount, 
                AccountId = request.AccountId,                                
            };

            int result = await _debtPaymentCommandRepository.Add(newDebtPayment);

            if(debt.CurrentAmount - request.Amount == 0)
            {
                debt.CompletedDate = DateTime.Now;
            }

            debt.CurrentAmount = debt.CurrentAmount - request.Amount;
            await _debtCommandRepository.Update(debt);

            if (debt.DebtTypeId == (int)DebtTypeEnum.Entrante)
            {            
                account.Ammount = account.Ammount + request.Amount;
                await _accountCommandRepository.Update(account);
            }
            else if (debt.DebtTypeId == (int)DebtTypeEnum.Saliente)
            {               
                account.Ammount = account.Ammount - request.Amount;
                await _accountCommandRepository.Update(account);
            }

            unitOfWork.SaveChanges();
            response.DebtPaymentId = result;
            response.Success = true;
            response.Message = "Debt payment created succesfully";
        }
        catch (Exception)
        {
            unitOfWork.UndoChanges();
            response.Success = false;
            response.Message = "Error to create Debt payment";
        }
        return response;
    }
}
