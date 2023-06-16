using Grpc.Core;
using Northwind.Grpc.Service;

namespace Northwind.Grpc.Service.Services
{
  public class GreeterService : Greeter.GreeterBase
  {
    private readonly ILogger<GreeterService> _logger;
    public GreeterService(ILogger<GreeterService> logger)
    {
      _logger = logger;
    }

    public override async Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
      await Task.Delay(1000);

      if (Random.Shared.Next(1, 4) == 1)
      {
        return new HelloReply
        {
          Message = "Hello " + request.Name
        };
      }
      else
      {
        throw new RpcException(new Status(StatusCode.Unavailable,
          "Service is temporarily unavailable. Try again later."));
      }
    }
  }
}
