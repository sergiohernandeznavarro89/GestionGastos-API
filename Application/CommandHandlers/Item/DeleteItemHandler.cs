using Application.Commands;
using Application.Dto;
using Domain.Configuration;
using Domain.Repositories.Command;
using MediatR;

namespace Application.CommandHandlers;

public class DeleteItemHandler : IRequestHandler<DeleteItemCommand, DeleteItemResponse>
{
    private readonly Sql.IUnitOfWorkFactory _unitOfWorkFactory;
    private readonly IItemQueryRepository _itemQueryRepository;
    private readonly IAccountQueryRepository _accountQueryRepository;

    public DeleteItemHandler(Sql.IUnitOfWorkFactory unitOfWorkFactory, IItemQueryRepository itemQueryRepository, IAccountQueryRepository accountQueryRepository)
    {
        _unitOfWorkFactory = unitOfWorkFactory;
        _itemQueryRepository = itemQueryRepository;
        _accountQueryRepository = accountQueryRepository;
    }

    public async Task<DeleteItemResponse> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
    {
        DeleteItemResponse response = new DeleteItemResponse();

        var unitOfWork = _unitOfWorkFactory.Create();
        try
        {
            var itemCommandRepository = unitOfWork.GetRepository<IItemCommandRepository>();
            var accountCommandRepository = unitOfWork.GetRepository<IAccountCommandRepository>();
            
            var item = await _itemQueryRepository.FindById(request.ItemId);
            int result = 0;
            if (item != null)
            {
                if (item.PeriodTypeId == (int)PeriodTypeEnum.Exporadico)
                {
                    var account = await _accountQueryRepository.FindById(item.AccountId);
                    if (account != null)
                    {
                        if (item.ItemTypeId == (int)ItemTypeEnum.Gasto)
                        {
                            account.Ammount += item.Ammount;
                        }
                        else if (item.ItemTypeId == (int)ItemTypeEnum.Ingreso)
                        {
                            account.Ammount -= item.Ammount;
                        }
                        await accountCommandRepository.Update(account);
                    }
                    result = await itemCommandRepository.Delete(request.ItemId);
                }
                else
                {
                    item.Cancelled = true;
                    result = await itemCommandRepository.Update(item);
                }
            }

            if (result > 0)
            {
                unitOfWork.SaveChanges();
                response.Success = true;
                response.Message = "Item deleted successfully";
            }
            else
            {
                unitOfWork.UndoChanges();
                response.Success = false;
                response.Message = "Item not found or could not be deleted";
            }
        }
        catch (Exception ex)
        {
            unitOfWork.UndoChanges();
            response.Success = false;
            response.Message = $"Error deleting Item: {ex.Message}";
        }

        return response;
    }
}
