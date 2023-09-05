namespace Application.CommandHandlers;

public class AddItemHandler : IRequestHandler<AddItemCommand, AddItemResponse>
{

    private readonly IUnitOfWorkFactory _unitOfWorkFactory;
    private readonly IMapper _mapper;
    private readonly IAccountQueryRepository _accountQueryRepository;

    public AddItemHandler(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper, IAccountQueryRepository accountQueryRepository)
    {
        _unitOfWorkFactory = unitOfWorkFactory;
        _mapper = mapper;
        _accountQueryRepository = accountQueryRepository;
    }

    public async Task<AddItemResponse> Handle(AddItemCommand request, CancellationToken cancellationToken)
    {
        AddItemResponse response = new AddItemResponse();

        var unitOfWork = _unitOfWorkFactory.Create();
        try
        {
            var _itemCommandRepository = unitOfWork.GetRepository<IItemCommandRepository>();
            var _accountCommandRepository = unitOfWork.GetRepository<IAccountCommandRepository>();

            int result = await _itemCommandRepository.Add(_mapper.Map<Item>(request));

            if(request.ItemTypeId == (int)ItemTypeEnum.Gasto && request.PeriodTypeId == (int)PeriodTypeEnum.Exporadico)
            {
                var account = await _accountQueryRepository.FindById(request.AccountId);
                if (account is not null)
                {                    
                    account.Ammount = account.Ammount - request.Ammount;
                    await _accountCommandRepository.Update(account);
                }
            }
            else if(request.ItemTypeId == (int)ItemTypeEnum.Ingreso && request.PeriodTypeId == (int)PeriodTypeEnum.Exporadico)
            {
                var account = await _accountQueryRepository.FindById(request.AccountId);
                if (account is not null)
                {
                    account.Ammount = account.Ammount + request.Ammount;
                    await _accountCommandRepository.Update(account);
                }
            }

            unitOfWork.SaveChanges();            
            response.ItemId = result;
            response.Success = true;
            response.Message = "Item created succesfully";
        }
        catch(Exception)
        {
            unitOfWork.UndoChanges();
            response.Success = false;
            response.Message = "Error to create Item";
        }
        return response;
    }
}
