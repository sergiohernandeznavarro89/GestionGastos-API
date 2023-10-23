namespace Application.Commands;

public class AddDebtCommand : IRequest<AddDebtResponse>
{
    public string DebtName { get; set; }
    public decimal StartAmount { get; set; }
    public int CategoryId { get; set; }
    public int SubCategoryId { get; set; }
    public int DebtTypeId { get; set; }
    public int UserId { get; set; }
    public string DebtorName { get; set; }
}
