namespace Application.CommandHandlers;

public class AddItemPaymentHandler : IRequestHandler<AddItemPaymentCommand, AddItemPaymentResponse>
{

    private readonly IUnitOfWorkFactory _unitOfWorkFactory;
    private readonly IMapper _mapper;    
    private readonly IAccountQueryRepository _accountQueryRepository;
    private readonly IItemQueryRepository _itemQueryRepository;

    public AddItemPaymentHandler(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper, IAccountQueryRepository accountQueryRepository, IItemQueryRepository itemQueryRepository)
    {
        _unitOfWorkFactory = unitOfWorkFactory;
        _mapper = mapper;
        _accountQueryRepository = accountQueryRepository;
        _itemQueryRepository = itemQueryRepository;
    }

    public async Task<AddItemPaymentResponse> Handle(AddItemPaymentCommand request, CancellationToken cancellationToken)
    {
        AddItemPaymentResponse response = new AddItemPaymentResponse();

        var unitOfWork = _unitOfWorkFactory.Create();
        try
        {
            var _itemPaymentCommandRepository = unitOfWork.GetRepository<IItemPaymentCommandRepository>();
            var _accountCommandRepository = unitOfWork.GetRepository<IAccountCommandRepository>();

            var item = await _itemQueryRepository.FindById(request.ItemId);

            var currentDate = DateTime.Now;
            var paymentDate = new DateTime(currentDate.Year, currentDate.Month, item.StartDate.Day);

            var newItemPayment = new ItemPayment { ItemId = request.ItemId, PaymentDate = paymentDate };

            int result = await _itemPaymentCommandRepository.Add(newItemPayment);


            if(item.ItemTypeId == (int)ItemTypeEnum.Gasto)
            {
                var account = await _accountQueryRepository.FindById(item.AccountId);
                if (account is not null)
                {
                    account.Ammount = account.Ammount - item.Ammount;
                    await _accountCommandRepository.Update(account);
                }
            }
            else if(item.ItemTypeId == (int)ItemTypeEnum.Ingreso)
            {
                var account = await _accountQueryRepository.FindById(item.AccountId);
                if (account is not null)
                {
                    account.Ammount = account.Ammount + item.Ammount;
                    await _accountCommandRepository.Update(account);
                }
            }

            unitOfWork.SaveChanges();            
            response.ItemPaymentId = result;
            response.Success = true;
            response.Message = "Item payment created succesfully";
        }
        catch(Exception)
        {
            unitOfWork.UndoChanges();
            response.Success = false;
            response.Message = "Error to create Item payment";
        }
        return response;
    }
}
