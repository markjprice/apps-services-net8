using AutoMapper; // To use MapperConfiguration.
using AutoMapper.Internal; // To use the Internal() extension method.
using Northwind.EntityModels; // To use Cart.
using Northwind.ViewModels; // To use Summary.

namespace MappingObjects.Mappers;

public static class CartToSummaryMapper
{
  public static MapperConfiguration GetMapperConfiguration()
  {
    MapperConfiguration config = new(cfg =>
    {
      // To fix an issue with the MaxInteger method:
      // https://github.com/AutoMapper/AutoMapper/issues/3988
      cfg.Internal().MethodMappingEnabled = false;

      // Configure the mapper using projections.
      cfg.CreateMap<Cart, Summary>()
       // Map the first and last names formatted to the full name.
       .ForMember(dest => dest.FullName, opt => opt.MapFrom(src =>
          string.Format("{0} {1}",
            src.Customer.FirstName,
            src.Customer.LastName)
        ))
        // Map the sum of items to the Total member.
        .ForMember(dest => dest.Total, opt => opt.MapFrom(
          src => src.Items.Sum(item => item.UnitPrice * item.Quantity)));
    });
    return config;
  }
}
