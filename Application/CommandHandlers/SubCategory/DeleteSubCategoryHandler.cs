namespace Application.CommandHandlers;

public class DeleteSubCategoryHandler : IRequestHandler<DeleteSubCategoryCommand, DeleteSubCategoryResponse>
{

    private readonly IUnitOfWorkFactory _unitOfWorkFactory;
    private readonly IMapper _mapper;
    private readonly ISubCategoryQueryRepository _subCategoryQueryRepository;

    public DeleteSubCategoryHandler(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper, ISubCategoryQueryRepository subCategoryQueryRepository)
    {
        _unitOfWorkFactory = unitOfWorkFactory;
        _mapper = mapper;
        _subCategoryQueryRepository = subCategoryQueryRepository;
    }

    public async Task<DeleteSubCategoryResponse> Handle(DeleteSubCategoryCommand request, CancellationToken cancellationToken)
    {
        DeleteSubCategoryResponse response = new DeleteSubCategoryResponse();

        var unitOfWork = _unitOfWorkFactory.Create();
        try
        {
            var subCategory = _mapper.Map<SubCategory>(await _subCategoryQueryRepository.FindById(request.SubCategoryId));

            if (subCategory is not null)
            {
                var _subCategoryCommandRepository = unitOfWork.GetRepository<ISubCategoryCommandRepository>();

                int result = await _subCategoryCommandRepository.Delete(subCategory);

                unitOfWork.SaveChanges();
                response.SubCategoryId = result;
                response.Success = true;
                response.Message = "SubCategory removed succesfully";
            }
            else
            {
                response.Success = false;
                response.Message = "SubCategory not found";
            }
        }
        catch (Exception)
        {
            unitOfWork.UndoChanges();
            response.Success = false;
            response.Message = "Error to remove SubCategory";
        }
        return response;
    }
}
