namespace Domain.Entities;

public class ItemPayment
{
    public int ItemPaymentId { get; set; }
    public int ItemId { get; set; }   
    public DateTime PaymentDate { get; set; }    
}
