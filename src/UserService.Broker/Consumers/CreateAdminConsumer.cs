using HerzenHelper.Core.BrokerSupport.Broker;
using HerzenHelper.Models.Broker.Requests.User;
using HerzenHelper.UserService.Data.Interfaces;
using HerzenHelper.UserService.Mappers.Db.Interfaces;
using HerzenHelper.UserService.Models.Db;
using MassTransit;
using System.Threading.Tasks;

namespace HerzenHelper.UserService.Broker.Consumers
{
  public class CreateAdminConsumer : IConsumer<ICreateAdminRequest>
  {
    private readonly IUserRepository _userRepository;
    private readonly IUserCredentialsRepository _credentialsRepository;
    private readonly IDbUserMapper _mapper;
    private readonly IDbUserCredentialsMapper _credentialsMapper;

    private async Task<object> CreateAdmin(ICreateAdminRequest request)
    {
      DbUser admin = _mapper.Map(request);
      await _userRepository.CreateAsync(admin);
      await _credentialsRepository
        .CreateAsync(_credentialsMapper.Map(userId: admin.Id, login: request.Login, password: request.Password));

      return true;
    }

    public CreateAdminConsumer(
      IUserRepository userRepository,
      IUserCredentialsRepository credentialsRepository,
      IDbUserMapper mapper,
      IDbUserCredentialsMapper credentialsMapper)
    {
      _userRepository = userRepository;
      _credentialsRepository = credentialsRepository;
      _mapper = mapper;
      _credentialsMapper = credentialsMapper;
    }

    public async Task Consume(ConsumeContext<ICreateAdminRequest> context)
    {
      object response = OperationResultWrapper.CreateResponse(CreateAdmin, context.Message);

      await context.RespondAsync<IOperationResult<bool>>(response);
    }
  }
}
