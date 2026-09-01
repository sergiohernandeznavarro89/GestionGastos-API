using Application.Commands.Transfer;
using Application.Dto.TransferPayment;
using Domain.Entities;
using Domain.Repositories.Command;
using Domain.Repositories.Query;
using static Domain.Configuration.Sql;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CommandHandlers.Transfer;

public class AddTransferPaymentHandler : IRequestHandler<AddTransferPaymentCommand, AddTransferPaymentResponse>
{
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;
    private readonly IAccountQueryRepository _accountQueryRepository;
    private readonly ITransferQueryRepository _transferQueryRepository;

    public AddTransferPaymentHandler(IUnitOfWorkFactory unitOfWorkFactory, IAccountQueryRepository accountQueryRepository, ITransferQueryRepository transferQueryRepository)
    {
        _unitOfWorkFactory = unitOfWorkFactory;
        _accountQueryRepository = accountQueryRepository;
        _transferQueryRepository = transferQueryRepository;
    }

    public async Task<AddTransferPaymentResponse> Handle(AddTransferPaymentCommand request, CancellationToken cancellationToken)
    {
        AddTransferPaymentResponse response = new AddTransferPaymentResponse();
        var unitOfWork = _unitOfWorkFactory.Create();
        
        try
        {
            var transferPaymentCommandRepository = unitOfWork.GetRepository<ITransferPaymentCommandRepository>();
            var accountCommandRepository = unitOfWork.GetRepository<IAccountCommandRepository>();

            var transfer = await _transferQueryRepository.FindById(request.TransferId);

            var currentDate = DateTime.Now;
            int lastDayOfMonth = DateTime.DaysInMonth(currentDate.Year, currentDate.Month);
            var paymentDate = new DateTime(currentDate.Year, currentDate.Month, transfer.StartDate.Day > lastDayOfMonth ? lastDayOfMonth : transfer.StartDate.Day);

            var newTransferPayment = new TransferPayment 
            { 
                TransferId = request.TransferId, 
                PaymentDate = paymentDate, 
                Ammount = request.Ammount
            };

            int result = await transferPaymentCommandRepository.Add(newTransferPayment);

            var originAccount = await _accountQueryRepository.FindById(transfer.OriginAccountId);
            var destAccount = await _accountQueryRepository.FindById(transfer.DestinationAccountId);
            
            if (originAccount is not null && destAccount is not null)
            {                    
                originAccount.Ammount = originAccount.Ammount - request.Ammount;
                await accountCommandRepository.Update(originAccount);
                
                destAccount.Ammount = destAccount.Ammount + request.Ammount;
                await accountCommandRepository.Update(destAccount);
            }

            unitOfWork.SaveChanges();            
            response.Success = true;
            response.Message = "Transferencia ejecutada correctamente";
        }
        catch(Exception)
        {
            unitOfWork.UndoChanges();
            response.Success = false;
            response.Message = "Error al ejecutar la transferencia";
        }
        
        return response;
    }
}
