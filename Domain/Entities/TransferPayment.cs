namespace Domain.Entities;

public class TransferPayment
{
    public int TransferPaymentId { get; set; }
    public int TransferId { get; set; }
    public DateTime PaymentDate { get; set; }
    public decimal Ammount { get; set; }
}
