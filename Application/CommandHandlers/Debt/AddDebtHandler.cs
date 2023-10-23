namespace Application.CommandHandlers;

public class AddDebtHandler : IRequestHandler<AddDebtCommand, AddDebtResponse>
{

    private readonly IUnitOfWorkFactory _unitOfWorkFactory;
    private readonly IMapper _mapper;

    public AddDebtHandler(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper)
    {
        _unitOfWorkFactory = unitOfWorkFactory;
        _mapper = mapper;
    }

    public async Task<AddDebtResponse> Handle(AddDebtCommand request, CancellationToken cancellationToken)
    {
        AddDebtResponse response = new AddDebtResponse();

        var unitOfWork = _unitOfWorkFactory.Create();
        try
        {
            var _debtCommandRepository = unitOfWork.GetRepository<IDebtCommandRepository>();

            var debtToInsert = _mapper.Map<Debt>(request);
            debtToInsert.Date = DateTime.Now;
            debtToInsert.CurrentAmount = request.StartAmount;

            int result = await _debtCommandRepository.Add(debtToInsert);            

            unitOfWork.SaveChanges();            
            response.DebtId = result;
            response.Success = true;
            response.Message = "Debt created succesfully";
        }
        catch(Exception)
        {
            unitOfWork.UndoChanges();
            response.Success = false;
            response.Message = "Error to create Debt";
        }
        return response;
    }
}
