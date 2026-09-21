using Domain.Repositories.Query;
using Domain.Services;
using MediatR;
using System.Text;

namespace Application.CommandHandlers;

public class SendUpcomingNotificationsHandler : IRequestHandler<Commands.SendUpcomingNotificationsCommand, bool>
{
    private readonly IItemSummaryQueryRepository _itemSummaryQueryRepository;
    private readonly IUserQueryRepository _userQueryRepository;
    private readonly IPushNotificationService _pushNotificationService;

    public SendUpcomingNotificationsHandler(
        IItemSummaryQueryRepository itemSummaryQueryRepository,
        IUserQueryRepository userQueryRepository,
        IPushNotificationService pushNotificationService)
    {
        _itemSummaryQueryRepository = itemSummaryQueryRepository;
        _userQueryRepository = userQueryRepository;
        _pushNotificationService = pushNotificationService;
    }

    public async Task<bool> Handle(Commands.SendUpcomingNotificationsCommand request, CancellationToken cancellationToken)
    {
        // El mes siguiente
        DateTime nextMonthDate = DateTime.Now.AddMonths(1);
        int targetYear = nextMonthDate.Year;
        int targetMonth = nextMonthDate.Month;

        // 1. Obtener los gastos/ingresos
        var items = await _itemSummaryQueryRepository.FindUpcomingNotificationsItems(targetYear, targetMonth);

        // 2. Agrupar por usuario
        var groupedByUser = items.GroupBy(i => i.UserId);

        foreach (var group in groupedByUser)
        {
            int userId = group.Key;
            
            // 3. Obtener el token del usuario
            var user = await _userQueryRepository.FindById(userId);
            
            if (user != null && !string.IsNullOrEmpty(user.FCMToken))
            {
                // 4. Construir y enviar notificación
                var gastos = group.Where(g => g.ItemTypeId == (int)Domain.Enums.ItemTypeEnum.Gasto).ToList();
                var ingresos = group.Where(g => g.ItemTypeId == (int)Domain.Enums.ItemTypeEnum.Ingreso).ToList();

                var totalGastos = gastos.Sum(g => g.Ammount);
                var totalIngresos = ingresos.Sum(g => g.Ammount);
                var balance = totalIngresos - totalGastos;

                string title = $"Resumen del próximo mes";
                
                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"Tienes {ingresos.Count} ingreso(s) y {gastos.Count} gasto(s) previstos.");
                
                if (balance >= 0)
                {
                    sb.AppendLine($"Total esperado: 🟢 +{balance} €");
                }
                else
                {
                    sb.AppendLine($"Total esperado: 🔴 {balance} €");
                }

                await _pushNotificationService.SendNotificationAsync(user.FCMToken, title, sb.ToString());
            }
        }

        return true;
    }
}
