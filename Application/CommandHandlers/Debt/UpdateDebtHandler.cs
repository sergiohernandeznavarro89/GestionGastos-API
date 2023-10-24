namespace Application.CommandHandlers;

public class UpdateDebtHandler : IRequestHandler<UpdateDebtCommand, UpdateDebtResponse>
{

    private readonly IUnitOfWorkFactory _unitOfWorkFactory;
    private readonly IDebtQueryRepository _debtQueryRepository;
    private readonly IMapper _mapper;

    public UpdateDebtHandler(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper, IDebtQueryRepository debtQueryRepository)
    {
        _unitOfWorkFactory = unitOfWorkFactory;
        _mapper = mapper;
        _debtQueryRepository = debtQueryRepository;
    }

    public async Task<UpdateDebtResponse> Handle(UpdateDebtCommand request, CancellationToken cancellationToken)
    {
        UpdateDebtResponse response = new UpdateDebtResponse();

        var unitOfWork = _unitOfWorkFactory.Create();
        try
        {
            var debt = await _debtQueryRepository.FindById(request.DebtId);

            if (debt is not null)
            {
                var _debtCommandRepository = unitOfWork.GetRepository<IDebtCommandRepository>();

                var debtToUpdate = _mapper.Map<Debt>(request);
                debtToUpdate.Date = DateTime.Now;
                debtToUpdate.CompletedDate = debt.CompletedDate;
                debtToUpdate.UserId = debt.UserId;

                int result = await _debtCommandRepository.Update(debtToUpdate);

                unitOfWork.SaveChanges();
                response.DebtId = result;
                response.Success = true;
                response.Message = "Debt updated succesfully";
            }
            else
            {
                response.Success = false;
                response.Message = "Debt not found";
            }
        }
        catch(Exception)
        {
            unitOfWork.UndoChanges();
            response.Success = false;
            response.Message = "Error to update Debt";
        }
        return response;
    }
}
