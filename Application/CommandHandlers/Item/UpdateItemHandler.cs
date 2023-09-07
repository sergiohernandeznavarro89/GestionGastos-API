namespace Application.CommandHandlers;

public class UpdateItemHandler : IRequestHandler<UpdateItemCommand, UpdateItemResponse>
{

    private readonly IUnitOfWorkFactory _unitOfWorkFactory;
    private readonly IMapper _mapper;
    private readonly IItemQueryRepository _itemQueryRepository;

    public UpdateItemHandler(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper, IItemQueryRepository itemQueryRepository)
    {
        _unitOfWorkFactory = unitOfWorkFactory;
        _mapper = mapper;
        _itemQueryRepository = itemQueryRepository;
    }

    public async Task<UpdateItemResponse> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
    {
        UpdateItemResponse response = new UpdateItemResponse();

        var unitOfWork = _unitOfWorkFactory.Create();
        try
        {
            var item = await _itemQueryRepository.FindById(request.ItemId);

            if (item is not null)
            {
                if (item.PeriodTypeId == (int)PeriodTypeEnum.Exporadico)
                {
                    response.Success = false;
                    response.Message = "Item occasional cannot be edited";
                }
                else
                {

                    var userId = item.UserId;
                    var periodTypeId = item.PeriodTypeId;
                    item = _mapper.Map<Item>(request);
                    item.UserId = userId;
                    item.PeriodTypeId = periodTypeId;

                    var _itemCommandRepository = unitOfWork.GetRepository<IItemCommandRepository>();

                    int result = await _itemCommandRepository.Update(item);

                    unitOfWork.SaveChanges();
                    response.ItemId = result;
                    response.Success = true;
                    response.Message = "Item updated succesfully";
                }
            }
            else
            {
                response.Success = false;
                response.Message = "Item not found";
            }
        }
        catch (Exception)
        {
            unitOfWork.UndoChanges();
            response.Success = false;
            response.Message = "Error to update Item";
        }

        return response;
    }
}
