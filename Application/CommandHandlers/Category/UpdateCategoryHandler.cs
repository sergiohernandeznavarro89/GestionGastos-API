namespace Application.CommandHandlers;

public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, UpdateCategoryResponse>
{

    private readonly IUnitOfWorkFactory _unitOfWorkFactory;
    private readonly IMapper _mapper;
    private readonly ICategoryQueryRepository _categoryQueryRepository;

    public UpdateCategoryHandler(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper, ICategoryQueryRepository categoryQueryRepository)
    {
        _unitOfWorkFactory = unitOfWorkFactory;
        _mapper = mapper;
        _categoryQueryRepository = categoryQueryRepository;
    }

    public async Task<UpdateCategoryResponse> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        UpdateCategoryResponse response = new UpdateCategoryResponse();

        var unitOfWork = _unitOfWorkFactory.Create();
        try
        {
            var category = await _categoryQueryRepository.FindById(request.CategoryId);            

            if (category is not null)
            {
                var userId = category.UserId;
                category = _mapper.Map<Category>(request);
                category.UserId = userId;

                var _categoryCommandRepository = unitOfWork.GetRepository<ICategoryCommandRepository>();

                int result = await _categoryCommandRepository.Update(category);

                unitOfWork.SaveChanges();
                response.CategoryId = result;
                response.Success = true;
                response.Message = "Category updated succesfully";
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
            response.Message = "Error to update Category";
        }
        return response;
    }
}
