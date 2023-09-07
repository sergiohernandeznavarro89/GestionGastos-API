namespace Application.CommandHandlers;

public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, DeleteCategoryResponse>
{

    private readonly IUnitOfWorkFactory _unitOfWorkFactory;
    private readonly IMapper _mapper;
    private readonly ICategoryQueryRepository _categoryQueryRepository;

    public DeleteCategoryHandler(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper, ICategoryQueryRepository categoryQueryRepository)
    {
        _unitOfWorkFactory = unitOfWorkFactory;
        _mapper = mapper;
        _categoryQueryRepository = categoryQueryRepository;
    }

    public async Task<DeleteCategoryResponse> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        DeleteCategoryResponse response = new DeleteCategoryResponse();

        var unitOfWork = _unitOfWorkFactory.Create();
        try
        {
            var category = await _categoryQueryRepository.FindById(request.CategoryId);            

            if (category is not null)
            {
                var _categoryCommandRepository = unitOfWork.GetRepository<ICategoryCommandRepository>();

                int result = await _categoryCommandRepository.Delete(category);

                unitOfWork.SaveChanges();
                response.CategoryId = result;
                response.Success = true;
                response.Message = "Category removed succesfully";
            }
            else
            {
                response.Success = false;
                response.Message = "Category not found";
            }
        }
        catch(Exception)
        {
            unitOfWork.UndoChanges();
            response.Success = false;
            response.Message = "Error to remove Category";
        }
        return response;
    }
}
