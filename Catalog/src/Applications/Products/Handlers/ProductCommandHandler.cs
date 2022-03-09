using Catalog.Api.Applications.Products.Commands;
using Catalog.Api.Domain;
using Catalog.Api.Repository;
using Core.Common.Exceptions;
using Core.Common.Messaging;
using FluentValidation.Results;
using MediatR;
using Serilog.Context;

namespace Catalog.Api.Applications.Products.Handlers
{
    public class ProductCommandHandler : CommandHandler,
                                        IRequestHandler<ProductCreateCommand, ValidationResult>//,
                                       // IRequestHandler<ProductUpdateCommand, ValidationResult>,
                                      //  IRequestHandler<ProductDeleteCommand, ValidationResult>
    {

        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductCommandHandler> _logger;
        private static readonly string ProviderTypeName = typeof(ProductCommandHandler).Name;


        public ProductCommandHandler(IProductRepository productRepository, ILogger<ProductCommandHandler> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }


        public async Task<ValidationResult> Handle(ProductCreateCommand request, CancellationToken cancellationToken)
        {
            LogReceivedMessage(LogLevel.Information, request,
                           "[{ProviderTypeName}/{ProviderActionType}] Notificação de inclusão de produto recebida. Iniciando processamento...",
                           ProviderTypeName, "ProductInsertCommand");


            if (await CheckHasProduct(request.Sku))
            {
                _logger.LogWarning("Product already in the system.");
                AddError("Produto ja existente");
                return ValidationResult;
            }

            return await SaveAsync(request);
        }

        //public async Task<ValidationResult> Handle(ProductUpdateCommand request, CancellationToken cancellationToken)
        //{
        //    LogReceivedMessage(LogLevel.Information, request,
        //                    "[{ProviderTypeName}/{ProviderActionType}] Notificação de Atualização de produto recebida. Iniciando processamento...",
        //                    ProviderTypeName, "ProductInsertCommand");

        //    if (!await CheckHasProduct(request.Sku))
        //    {
        //        _logger.LogWarning("Product not found.");
        //        AddError("Produto não encontrado.");
        //        return ValidationResult;
        //    }

        //    return await SaveAsync(request);
        //}

        //public async Task<ValidationResult> Handle(ProductDeleteCommand request, CancellationToken cancellationToken)
        //{
        //    LogReceivedMessage(LogLevel.Information, request,
        //                  "[{ProviderTypeName}/{ProviderActionType}] Notificação de Exclusão de produto recebida. Iniciando processamento...",
        //                  ProviderTypeName, "ProductInsertCommand");

        //    var product = await _productRepository.GetBySkuAsync(request.Sku, request.ChannelId);

        //    if (product is null)
        //    {
        //        _logger.LogWarning("Product not found.");
        //        AddError("Produto não encontrado.");
        //        return ValidationResult;
        //    }


        //    _productRepository.Delete(product);

        //    //TODO: LIMPA CACHE
        //    await _productRepository.CleanProductCacheBySku(request.Sku, request.ChannelId);

        //    return await Commit(_productRepository.UnitOfWork);
        //}


        private async Task<bool> CheckHasProduct(string sku) =>
            await _productRepository.CheckExistsAsync(sku);


        protected void LogReceivedMessage<T>(LogLevel logLevel, T receivedMessage, string logMessage, params object[] args)
        {
            using (LogContext.PushProperty("ReceivedMessage", receivedMessage, true))
            {
                _logger.Log(logLevel, logMessage, args);
            }
        }


        private async Task<ValidationResult> SaveAsync(ProductCommand request)
        {

            Product product = new(request.Sku,  request.Title, request.Price);


            if (product is null)
            {
                throw new DomainValidationException(string.Empty, "Falha ao tentar criar um novo Objeto Product.", "Product.Create");
            }
            else
            {
                try
                {
                    await _productRepository.SaveAsync(product);
                    //ValidationResult = await Commit(_productRepository.UnitOfWork);


                    if (ValidationResult.IsValid)
                    {
                        //TODO: LIMPA CACHE
                    //    await _productRepository.CleanProductCacheBySku(request.Sku, request.ChannelId);
                    }
                    return ValidationResult;
                }
                catch (Exception ex)
                {
                    LogReceivedMessage(LogLevel.Critical, ex,
                                              "[{ProviderTypeName}/{ProviderActionType}] Falha Critica ao Executar uma operação no banco de dados. Finalizando processamento...",
                                               ProviderTypeName, "SaveAsync");

                    AddError("Falha ao tentar atualizar um produto. Finalizando processamento...");
                    return ValidationResult;
                }

            }



            //  var channel = await _configurationRepository.GetChannelByIdAsync(message.SellerId);
            //  var product = message.Product.Map(message.SellerId);

            ////  _productService.SetDefaultValuesForVariations(product, channel);

            //  await _productRepository.SaveAsync(product, saveAssociate);

            //  // REMOVE CACHEKEY
            //  this.RemoveCacheKey(product.Sku, message.SellerId);

            //  _logger.LogInformation("Apply bussiness rule and save product.");

            //  //TODO: Cria mensagens para HubFrete

            //  await UpdateStockOrder(message.Product, message.SellerId, true, headers);

            //  var eventSaved = new ProductSavedNotification(message.Product, message.SellerId, isNew);
            //  await _busProductNotification.Publish(eventSaved, headers.GetKaiveHeaders());

            // _logger.LogInformation("Publish message ProductSavedNotification.");
        }

    }
}
