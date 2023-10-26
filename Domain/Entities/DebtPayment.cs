namespace Domain.Entities;

public class DebtPayment
{
    public int DebtPaymentId { get; set; }
    public int DebtId { get; set; }   
    public int AccountId { get; set; }
    public DateTime Date { get; set; }   
    public decimal Amount { get; set; }
}
