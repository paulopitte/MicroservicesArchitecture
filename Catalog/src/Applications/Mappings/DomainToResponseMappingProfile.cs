using AutoMapper;
using Catalog.Api.Applications.Products.Commands;
using Request =  Core.Contracts.Requests;

namespace Catalog.Api.Application.Mappings
{
    public class DomainToResponseMappingProfile : Profile
    {

        public DomainToResponseMappingProfile()
        {
            CreateMap<Domain.Product, Request.Product>().ReverseMap();
            CreateMap<IEnumerable<Domain.Product>, List<Request.Product>>();
            CreateMap<ProductCreateCommand, Request.Product>().ReverseMap();
            CreateMap<ProductUpdateCommand, Request.Product>().ReverseMap();
            CreateMap<ProductDeleteCommand, Request.Product>().ReverseMap();

        }
    }
}
