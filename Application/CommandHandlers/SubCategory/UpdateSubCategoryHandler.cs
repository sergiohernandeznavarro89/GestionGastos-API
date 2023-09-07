using Infrastructure.Repositories.Query;

namespace Application.CommandHandlers;

public class UpdateSubCategoryHandler : IRequestHandler<UpdateSubCategoryCommand, UpdateSubCategoryResponse>
{

    private readonly IUnitOfWorkFactory _unitOfWorkFactory;
    private readonly IMapper _mapper;
    private readonly ISubCategoryQueryRepository _subCategoryQueryRepository;

    public UpdateSubCategoryHandler(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper, ISubCategoryQueryRepository subCategoryQueryRepository)
    {
        _unitOfWorkFactory = unitOfWorkFactory;
        _mapper = mapper;
        _subCategoryQueryRepository = subCategoryQueryRepository;
    }

    public async Task<UpdateSubCategoryResponse> Handle(UpdateSubCategoryCommand request, CancellationToken cancellationToken)
    {
        UpdateSubCategoryResponse response = new UpdateSubCategoryResponse();

        var unitOfWork = _unitOfWorkFactory.Create();
        try
        {
            var subCategory = _mapper.Map<SubCategory>(await _subCategoryQueryRepository.FindById(request.SubCategoryId));

            if (subCategory is not null)
            {
                var categoryId = subCategory.CategoryId;
                subCategory = _mapper.Map<SubCategory>(request);
                subCategory.CategoryId = categoryId;

                var _subCategoryCommandRepository = unitOfWork.GetRepository<ISubCategoryCommandRepository>();

                int result = await _subCategoryCommandRepository.Update(subCategory);

                unitOfWork.SaveChanges();
                response.SubCategoryId = result;
                response.Success = true;
                response.Message = "Subcategory updated succesfully";
            }
            else
            {
                response.Success = false;
                response.Message = "Subcategory not found";
            }
        }
        catch(Exception)
        {
            unitOfWork.UndoChanges();
            response.Success = false;
            response.Message = "Error to update Subcategory";
        }
        return response;
    }
}
