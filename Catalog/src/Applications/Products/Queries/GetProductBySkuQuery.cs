﻿using Catalog.Api.Domain;
using MediatR;
 
namespace Sigc.MktHub.Catalog.Core.Application.Products.Queries
{
    public struct GetProductBySkuQuery : IRequest<Product>
    {
        public string Sku { get; set; }
 
        public GetProductBySkuQuery(string sku) =>       
            Sku = sku;
         
    }
}
