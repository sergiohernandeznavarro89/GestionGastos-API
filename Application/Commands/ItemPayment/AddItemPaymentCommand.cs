namespace Application.Commands;

public class AddItemPaymentCommand : IRequest<AddItemPaymentResponse>
{
    public int ItemId { get; set; }
    public decimal Ammount { get; set; }

    public AddItemPaymentCommand(int itemId, decimal ammount)
    {
        ItemId = itemId;
        Ammount = ammount;
    }
}
