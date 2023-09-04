using Infrastructure.Repositories.Query;

namespace Application.QueryHandlers;

public class GetCategoriesByUserQueryHandler : IRequestHandler<GetCategoriesQuery, List<CategoryResponse>>
{

    private readonly IMapper _mapper;
    private readonly ICategoryQueryRepository _categoryQueryRepository;

    public GetCategoriesByUserQueryHandler(IMapper mapper, ICategoryQueryRepository categoryQueryRepository)
    {
        _mapper = mapper;
        _categoryQueryRepository = categoryQueryRepository;
    }

    public async Task<List<CategoryResponse>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var response = new List<CategoryResponse>();
        response = _mapper.Map<List<CategoryResponse>>(await _categoryQueryRepository.FindByUserId(request.UserId));

        return response;
    }
}
