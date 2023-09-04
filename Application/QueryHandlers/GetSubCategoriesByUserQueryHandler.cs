using Infrastructure.Repositories.Query;

namespace Application.QueryHandlers;

public class GetSubCategoriesByUserQueryHandler : IRequestHandler<GetSubCategoriesQuery, List<SubCategoryResponse>>
{

    private readonly IMapper _mapper;
    private readonly ISubCategoryQueryRepository _subCategoryQueryRepository;

    public GetSubCategoriesByUserQueryHandler(IMapper mapper, ISubCategoryQueryRepository subCategoryQueryRepository)
    {
        _mapper = mapper;
        _subCategoryQueryRepository = subCategoryQueryRepository;
    }

    public async Task<List<SubCategoryResponse>> Handle(GetSubCategoriesQuery request, CancellationToken cancellationToken)
    {
        var response = new List<SubCategoryResponse>();
        response = _mapper.Map<List<SubCategoryResponse>>(await _subCategoryQueryRepository.FindByUserId(request.UserId));

        return response;
    }
}
