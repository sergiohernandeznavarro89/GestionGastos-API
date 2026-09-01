using Application.Commands.Transfer;
using Application.Dto.Transfer;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Domain.Repositories.Command;
using Domain.Repositories.Query;
using static Domain.Configuration.Sql;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CommandHandlers.Transfer;

public class UpdateTransferHandler : IRequestHandler<UpdateTransferCommand, UpdateTransferResponse>
{
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;
    private readonly IMapper _mapper;
    private readonly ITransferQueryRepository _transferQueryRepository;

    public UpdateTransferHandler(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper, ITransferQueryRepository transferQueryRepository)
    {
        _unitOfWorkFactory = unitOfWorkFactory;
        _mapper = mapper;
        _transferQueryRepository = transferQueryRepository;
    }

    public async Task<UpdateTransferResponse> Handle(UpdateTransferCommand request, CancellationToken cancellationToken)
    {
        UpdateTransferResponse response = new UpdateTransferResponse();
        var unitOfWork = _unitOfWorkFactory.Create();
        
        try
        {
            var transfer = await _transferQueryRepository.FindById(request.TransferId);

            if (transfer is not null)
            {
                if (transfer.PeriodTypeId == (int)PeriodTypeEnum.Exporadico)
                {
                    response.Success = false;
                    response.Message = "La transferencia esporádica no se puede editar";
                }
                else
                {
                    var userId = transfer.UserId;
                    var periodTypeId = transfer.PeriodTypeId;
                    
                    transfer = _mapper.Map<Domain.Entities.Transfer>(request);
                    transfer.UserId = userId;
                    transfer.PeriodTypeId = periodTypeId;

                    var transferCommandRepository = unitOfWork.GetRepository<ITransferCommandRepository>();

                    int result = await transferCommandRepository.Update(transfer);

                    unitOfWork.SaveChanges();
                    response.Success = true;
                    response.Message = "Transferencia actualizada correctamente";
                }
            }
            else
            {
                response.Success = false;
                response.Message = "La transferencia no existe";
            }
        }
        catch(Exception)
        {
            unitOfWork.UndoChanges();
            response.Success = false;
            response.Message = "Error al actualizar la transferencia";
        }
        
        return response;
    }
}
