namespace Application.Commands;

public class AddItemPaymentCommand : IRequest<AddItemPaymentResponse>
{
    public int ItemId { get; set; }

    public AddItemPaymentCommand(int itemId)
    {
        ItemId = itemId;
    }
}
