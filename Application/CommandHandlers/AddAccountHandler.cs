using Domain.Repositories.Command;
using static Domain.Configuration.Sql;

namespace Application.CommandHandlers;

public class AddAccountHandler : IRequestHandler<AddAccountCommand, AddAccountResponse>
{

    private readonly IUnitOfWorkFactory _unitOfWorkFactory;
    private readonly IMapper _mapper;

    public AddAccountHandler(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper)
    {
        _unitOfWorkFactory = unitOfWorkFactory;
        _mapper = mapper;
    }

    public async Task<AddAccountResponse> Handle(AddAccountCommand request, CancellationToken cancellationToken)
    {
        AddAccountResponse response = new AddAccountResponse();

        var unitOfWork = _unitOfWorkFactory.Create();
        try
        {
            var _accountCommandRepository = unitOfWork.GetRepository<IAccountCommandRepository>();

            int result = await _accountCommandRepository.Add(_mapper.Map<Account>(request));

            unitOfWork.SaveChanges();            
            response.AccountId = result;
            response.Success = true;
            response.Message = "Account created succesfully";
        }
        catch(Exception ex)
        {
            unitOfWork.UndoChanges();
            response.Success = false;
            response.Message = "Error to create Account";
        }
        return response;
    }
}
