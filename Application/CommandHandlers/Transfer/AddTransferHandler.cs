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

public class AddTransferHandler : IRequestHandler<AddTransferCommand, AddTransferResponse>
{
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;
    private readonly IMapper _mapper;
    private readonly IAccountQueryRepository _accountQueryRepository;

    public AddTransferHandler(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper, IAccountQueryRepository accountQueryRepository)
    {
        _unitOfWorkFactory = unitOfWorkFactory;
        _mapper = mapper;
        _accountQueryRepository = accountQueryRepository;
    }

    public async Task<AddTransferResponse> Handle(AddTransferCommand request, CancellationToken cancellationToken)
    {
        AddTransferResponse response = new AddTransferResponse();
        var unitOfWork = _unitOfWorkFactory.Create();
        
        try
        {
            var transferCommandRepository = unitOfWork.GetRepository<ITransferCommandRepository>();
            var accountCommandRepository = unitOfWork.GetRepository<IAccountCommandRepository>();

            var transfer = _mapper.Map<Domain.Entities.Transfer>(request);
            int result = await transferCommandRepository.Add(transfer);

            if(request.PeriodTypeId == (int)PeriodTypeEnum.Exporadico)
            {
                var originAccount = await _accountQueryRepository.FindById(request.OriginAccountId);
                var destAccount = await _accountQueryRepository.FindById(request.DestinationAccountId);
                
                if (originAccount is not null && destAccount is not null)
                {                    
                    originAccount.Ammount = originAccount.Ammount - request.Ammount;
                    await accountCommandRepository.Update(originAccount);
                    
                    destAccount.Ammount = destAccount.Ammount + request.Ammount;
                    await accountCommandRepository.Update(destAccount);
                }
            }

            unitOfWork.SaveChanges();            
            response.Success = true;
            response.Message = "Transferencia creada correctamente";
        }
        catch(Exception)
        {
            unitOfWork.UndoChanges();
            response.Success = false;
            response.Message = "Error al crear la transferencia";
        }
        
        return response;
    }
}
