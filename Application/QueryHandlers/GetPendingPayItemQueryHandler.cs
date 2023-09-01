namespace Application.QueryHandlers;

public class GetPendingPayItemQueryHandler : IRequestHandler<GetPendingPayItemQuery, List<PendingPayItemResponse>>
{

    private readonly IMapper _mapper;
    private readonly IItemSummaryQueryRepository _itemSummaryQueryRepository;
    private readonly IItemPaymentQueryRepository _itemPaymentQueryRepository;

    public GetPendingPayItemQueryHandler(IMapper mapper, IItemSummaryQueryRepository itemSummaryQueryRepository, IItemPaymentQueryRepository itemPaymentQueryRepository)
    {
        _mapper = mapper;
        _itemSummaryQueryRepository = itemSummaryQueryRepository;
        _itemPaymentQueryRepository = itemPaymentQueryRepository;
    }

    public async Task<List<PendingPayItemResponse>> Handle(GetPendingPayItemQuery request, CancellationToken cancellationToken)
    {
        var response = new List<PendingPayItemResponse>();        

        var items = await _itemSummaryQueryRepository.FindByUserId(request.UserId);

        var itemsRecurrentes = items.Where(x => x.PeriodTypeId == (int)PeriodTypeEnum.Recurrente).ToList();

        //obtengo de los items recurrentes los que se tendrían que pagar este mes y que todavía no se encuentran pagados
        var itemsPendingPay = itemsRecurrentes
            .Where(_item => ShouldPayThisMonth(_item.StartDate, _item.EndDate, (int)_item.Periodity))
            .Where(_item => _itemPaymentQueryRepository.FindByItemAndThisMonth(_item.ItemId).Result is null)
            .ToList();

        response = _mapper.Map<List<PendingPayItemResponse>>(itemsPendingPay);

        return response;
    }

    private bool ShouldPayThisMonth(DateTime startDate, DateTime endDate, int period)
    {
        DateTime currentDate = DateTime.Now.Date;

        // Si la fecha actual está dentro del rango de fechas
        if (currentDate >= startDate && currentDate <= endDate)
        {
            // Calcular la diferencia en meses entre la fecha actual y la fecha de inicio
            int monthsDifference = (currentDate.Year - startDate.Year) * 12 + currentDate.Month - startDate.Month;

            // Si la diferencia en meses es divisible por la periodicidad, es hora de pagar
            if (monthsDifference % period == 0)
            {
                return true;
            }
        }

        return false;
    }
}
