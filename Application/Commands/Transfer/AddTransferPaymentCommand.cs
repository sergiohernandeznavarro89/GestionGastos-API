using Application.Dto.TransferPayment;
using MediatR;
using System;

namespace Application.Commands.Transfer;

public class AddTransferPaymentCommand : IRequest<AddTransferPaymentResponse>
{
    public int TransferId { get; set; }
    public DateTime PaymentDate { get; set; }
    public decimal Ammount { get; set; }
}
