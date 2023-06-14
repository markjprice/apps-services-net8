using Grpc.Core.Interceptors; // To use Interceptor and so on.
using Grpc.Core; // To use AsyncUnaryCall<T>.

namespace Northwind.Grpc.Client.Mvc.Interceptors;

public class ClientLoggingInterceptor : Interceptor
{
  private readonly ILogger _logger;

  public ClientLoggingInterceptor(ILoggerFactory loggerFactory)
  {
    _logger = loggerFactory.CreateLogger<ClientLoggingInterceptor>();
  }

  public override AsyncUnaryCall<TResponse> 
    AsyncUnaryCall<TRequest, TResponse>(TRequest request,
    ClientInterceptorContext<TRequest, TResponse> context,
    AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
  {
    _logger.LogWarning("Starting call. Type: {0}. Method: {1}.",
      context.Method.Type, context.Method.Name);
    
    return continuation(request, context);
  }
}
