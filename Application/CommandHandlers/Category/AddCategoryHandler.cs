namespace Application.CommandHandlers;

public class AddCategoryHandler : IRequestHandler<AddCategoryCommand, AddCategoryResponse>
{

    private readonly IUnitOfWorkFactory _unitOfWorkFactory;
    private readonly IMapper _mapper;

    public AddCategoryHandler(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper)
    {
        _unitOfWorkFactory = unitOfWorkFactory;
        _mapper = mapper;
    }

    public async Task<AddCategoryResponse> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
    {
        AddCategoryResponse response = new AddCategoryResponse();

        var unitOfWork = _unitOfWorkFactory.Create();
        try
        {
            var _categoryCommandRepository = unitOfWork.GetRepository<ICategoryCommandRepository>();

            int result = await _categoryCommandRepository.Add(_mapper.Map<Category>(request));

            unitOfWork.SaveChanges();            
            response.CategoryId = result;
            response.Success = true;
            response.Message = "Category created succesfully";
        }
        catch(Exception)
        {
            unitOfWork.UndoChanges();
            response.Success = false;
            response.Message = "Error to create Category";
        }
        return response;
    }
}
