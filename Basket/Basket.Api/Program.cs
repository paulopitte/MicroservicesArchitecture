using Basket.Api.Extensions;
using Core.Common.Extensions;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

#region Constantes
/// <summary>
/// Nome da politica utilizada para o CORS.
/// </summary>
const string CorsPolicyName = "AllowKnownHosts"; // "KnownHostsOnly"

const string ptBr = "pt-BR";
var brazilianPortuguese = new CultureInfo(ptBr);
var supportedCultures = new[] { brazilianPortuguese };

#endregion

builder.Services.AddApplicationEvents();
builder.Services.AddLogging(builder.Configuration);
builder.Services.AddInfrastructureAPI(builder.Configuration);
builder.Services.AddCorsAPI(CorsPolicyName);
builder.Services.AddBusinessServices(builder.Configuration);


var app = builder.Build();

var applicationBuilder = app.Services.GetRequiredService<IServiceProvider>();
var appLifetime = app.Services.GetRequiredService<IHostApplicationLifetime>();
var loggerFactory = app.Services.GetRequiredService<ILoggerFactory>();
var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

// Primeiro Passo: Registro os eventos de ciclo de vida da aplicação.
appLifetime.RegisterApplicationEvents(applicationBuilder, loggerFactory);



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Segundo Passo:  Configuro do Pipeline de requisições:
app.UseHttpsRedirection();
app.UseDefaultFiles(new DefaultFilesOptions { DefaultFileNames = new List<string> { "index.html" } });
app.UseAuthorization();
app.UseHealthChecks("/status");
app.UseRouting();
app.MapControllers();





// Terceiro passo: Habilita o Compartilhamento de Recursos entre Origens para qualquer URL solicitada.
// (CORS: Cross-Origin Resource Sharing)
app.UseCors(CorsPolicyName);


// Quarto passo: Configura e Habilita a exposição de documentação da API via "Swagger".
app.UseSwaggerConfig(provider);



// Quinto Passo: Ativo a compressão dos responses
app.UseResponseCompression();

//app.UseRedisDistributedCache(ApplicationType.Catalog);

app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(ptBr),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
});

// ultimo passo: Habilita o fornecimento de arquivos estáticos, presentes na pasta "wwwroot".
app.UseStaticFiles();
app.Run();
