using AutoMapper;
using WA.Application.Contracts.Request.RequestCategory;
using WA.Application.Contracts.Request.RequestCustomer;
using WA.Application.Contracts.Request.RequestOrder;
using WA.Application.Contracts.Request.RequestProduct;
using WA.Application.Contracts.Response.ResponseOrder;
using WA.Domain.Entities;
using WA.Domain.Enums;

namespace WA.Application.Mapper
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<Customer, CustomerPostRequest>().ReverseMap();
            CreateMap<Customer, CustomerPutRequest>().ReverseMap();
            CreateMap<Customer, CustomerGetRequest>().ReverseMap();
            CreateMap<Category, CategoryPostRequest>().ReverseMap();
            CreateMap<Category, CategoryPutRequest>().ReverseMap();
            CreateMap<ProductRequest, Product>().ReverseMap();
            CreateMap<Order, OrderRequest>().ReverseMap();
            CreateMap<Order, OrderPostRequest>().ReverseMap();
            CreateMap<Orderitem, OrderItemRequest>().ReverseMap();
            CreateMap<Order, OrderResponse>()
                 .ForMember(dest => dest.StatusPagamento, opt => opt.MapFrom(src => (EnumStatusPayment)src.Payments.FirstOrDefault().PaymentStatus)).ReverseMap();
            CreateMap<Orderitem, OrderItemResponse>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ReverseMap();
        }
    }
}
