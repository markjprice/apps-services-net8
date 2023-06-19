/* Shared classes can be referenced by both the Client and Server */

namespace Northwind.Shared;

public static class NorthwindExtensionMethods
{
  public static string ConvertToBase64Jpeg(this byte[] picture)
  {
    return string.Format("data:image/jpg;base64,{0}",
      Convert.ToBase64String(picture));
  }
}
