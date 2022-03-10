using AutoMapper;
using Catalog.Api.Applications.Products.Commands;
using Request = Core.Contracts.Requests;

namespace Catalog.Api.Application.Mappings
{
 
    public class RequestToCommandMappingProfile : Profile
    {
        public RequestToCommandMappingProfile()
        {
            CreateMap<Request.Product, ProductCreateCommand>().ReverseMap();
            CreateMap<Request.Product, Domain.Product>().ReverseMap();
        }
    }
}
