using System.Data;

namespace Domain.Entities;

public class Debt
{
    public int DebtId { get; set; }
    public string DebtName { get; set; }
    public decimal StartAmount { get; set; }
    public DateTime Date { get; set; }
    public int CategoryId { get; set; }
    public int SubCategoryId { get; set; }
    public int DebtTypeId { get; set; }
    public int UserId { get; set; }
    public string DebtorName { get; set; }
    public decimal CurrentAmount { get; set; }
    public DateTime? CompletedDate { get; set; }
}
