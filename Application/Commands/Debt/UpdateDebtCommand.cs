namespace Application.Commands;

public class UpdateDebtCommand : IRequest<UpdateDebtResponse>
{
    public int DebtId { get; set; }
    public string DebtName { get; set; }
    public decimal StartAmount { get; set; }
    public int CategoryId { get; set; }
    public int SubCategoryId { get; set; }
    public int DebtTypeId { get; set; }
    public string DebtorName { get; set; }
    public decimal CurrentAmount { get; set; }

}
