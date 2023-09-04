namespace Application.CommandHandlers;

public class AddSubCategoryHandler : IRequestHandler<AddSubCategoryCommand, AddSubCategoryResponse>
{

    private readonly IUnitOfWorkFactory _unitOfWorkFactory;
    private readonly IMapper _mapper;

    public AddSubCategoryHandler(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper)
    {
        _unitOfWorkFactory = unitOfWorkFactory;
        _mapper = mapper;
    }

    public async Task<AddSubCategoryResponse> Handle(AddSubCategoryCommand request, CancellationToken cancellationToken)
    {
        AddSubCategoryResponse response = new AddSubCategoryResponse();

        var unitOfWork = _unitOfWorkFactory.Create();
        try
        {
            var _subCategoryCommandRepository = unitOfWork.GetRepository<ISubCategoryCommandRepository>();

            int result = await _subCategoryCommandRepository.Add(_mapper.Map<SubCategory>(request));

            unitOfWork.SaveChanges();            
            response.SubCategoryId = result;
            response.Success = true;
            response.Message = "Subcategory created succesfully";
        }
        catch(Exception)
        {
            unitOfWork.UndoChanges();
            response.Success = false;
            response.Message = "Error to create Subcategory";
        }
        return response;
    }
}
