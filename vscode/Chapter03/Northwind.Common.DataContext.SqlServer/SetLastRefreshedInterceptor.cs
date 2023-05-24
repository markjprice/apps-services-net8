// IMaterializationInterceptor, MaterializationInterceptionData
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Northwind.EntityModels;

public class SetLastRefreshedInterceptor : IMaterializationInterceptor
{
  public object InitializedInstance(
    MaterializationInterceptionData materializationData,
    object entity)
  {
    if (entity is IHasLastRefreshed entityWithLastRefreshed)
    {
      entityWithLastRefreshed.LastRefreshed = DateTimeOffset.UtcNow;
    }
    return entity;
  }
}
