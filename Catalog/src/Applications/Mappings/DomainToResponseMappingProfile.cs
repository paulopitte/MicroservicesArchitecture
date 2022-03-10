﻿using AutoMapper;
using Catalog.Api.Applications.Products.Commands;
using Request =  Core.Contracts.Requests;

namespace Catalog.Api.Application.Mappings
{
    public class DomainToResponseMappingProfile : Profile
    {

        public DomainToResponseMappingProfile()
        {
            CreateMap<Domain.Product, Request.Product>().ReverseMap();
            CreateMap<ProductCreateCommand, Request.Product>().ReverseMap();

        }
    }
}
