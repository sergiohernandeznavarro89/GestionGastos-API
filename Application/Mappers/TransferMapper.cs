using Application.Commands.Transfer;
using Application.Dto.Transfer;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappers;

public class TransferMapper : Profile
{
    public TransferMapper()
    {
        CreateMap<AddTransferCommand, Transfer>();
        CreateMap<UpdateTransferCommand, Transfer>();
        CreateMap<TransferSummary, TransferResponse>();
        CreateMap<TransferSummary, PendingTransferResponse>();
        CreateMap<TransferSummary, NextMonthPendingTransferResponse>();
    }
}
