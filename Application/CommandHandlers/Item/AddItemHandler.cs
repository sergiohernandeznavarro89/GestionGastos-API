namespace Application.CommandHandlers;

public class AddItemHandler : IRequestHandler<AddItemCommand, AddItemResponse>
{

    private readonly IUnitOfWorkFactory _unitOfWorkFactory;
    private readonly IMapper _mapper;

    public AddItemHandler(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper)
    {
        _unitOfWorkFactory = unitOfWorkFactory;
        _mapper = mapper;
    }

    public async Task<AddItemResponse> Handle(AddItemCommand request, CancellationToken cancellationToken)
    {
        AddItemResponse response = new AddItemResponse();

        var unitOfWork = _unitOfWorkFactory.Create();
        try
        {
            var _itemCommandRepository = unitOfWork.GetRepository<IItemCommandRepository>();

            int result = await _itemCommandRepository.Add(_mapper.Map<Item>(request));

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
