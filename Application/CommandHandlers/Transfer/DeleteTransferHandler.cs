using Application.Commands.Transfer;
using Application.Dto.Transfer;
using Domain.Enums;
using Domain.Repositories.Command;
using Domain.Repositories.Query;
using static Domain.Configuration.Sql;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CommandHandlers.Transfer;

public class DeleteTransferHandler : IRequestHandler<DeleteTransferCommand, DeleteTransferResponse>
{
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;
    private readonly ITransferQueryRepository _transferQueryRepository;
    private readonly IAccountQueryRepository _accountQueryRepository;

    public DeleteTransferHandler(IUnitOfWorkFactory unitOfWorkFactory, ITransferQueryRepository transferQueryRepository, IAccountQueryRepository accountQueryRepository)
    {
        _unitOfWorkFactory = unitOfWorkFactory;
        _transferQueryRepository = transferQueryRepository;
        _accountQueryRepository = accountQueryRepository;
    }

    public async Task<DeleteTransferResponse> Handle(DeleteTransferCommand request, CancellationToken cancellationToken)
    {
        DeleteTransferResponse response = new DeleteTransferResponse();
        var unitOfWork = _unitOfWorkFactory.Create();
        
        try
        {
            var transferCommandRepository = unitOfWork.GetRepository<ITransferCommandRepository>();
            var accountCommandRepository = unitOfWork.GetRepository<IAccountCommandRepository>();

            var transferPaymentCommandRepository = unitOfWork.GetRepository<ITransferPaymentCommandRepository>();

            var transfer = await _transferQueryRepository.FindById(request.TransferId);
            if (transfer == null)
            {
                response.Success = false;
                response.Message = "La transferencia no existe";
                return response;
            }

            int result = 0;
            if(transfer.PeriodTypeId == (int)PeriodTypeEnum.Exporadico)
            {
                await transferPaymentCommandRepository.DeleteByTransferId(request.TransferId);
                result = await transferCommandRepository.Delete(request.TransferId);

                var originAccount = await _accountQueryRepository.FindById(transfer.OriginAccountId);
                var destAccount = await _accountQueryRepository.FindById(transfer.DestinationAccountId);
                
                if (originAccount is not null && destAccount is not null)
                {                    
                    originAccount.Ammount = originAccount.Ammount + transfer.Ammount;
                    await accountCommandRepository.Update(originAccount);
                    
                    destAccount.Ammount = destAccount.Ammount - transfer.Ammount;
                    await accountCommandRepository.Update(destAccount);
                }
            }
            else
            {
                transfer.Cancelled = true;
                result = await transferCommandRepository.Update(transfer);
            }

            unitOfWork.SaveChanges();            
            response.Success = true;
            response.Message = "Transferencia borrada correctamente";
        }
        catch(Exception)
        {
            unitOfWork.UndoChanges();
            response.Success = false;
            response.Message = "Error al borrar la transferencia";
        }
        
        return response;
    }
}
