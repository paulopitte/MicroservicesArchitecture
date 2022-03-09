
using Catalog.Api.Extensions;
using Core.Common.Extensions;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System.Globalization;


var builder = WebApplication.CreateBuilder(args);

/// <summary>
/// Nome da politica utilizada para o CORS.
/// </summary>
const string CorsPolicyName = "KnownHostsOnly"; //"AllowKnownHosts";

builder.Services.AddInfrastructureAPI(builder.Configuration);
builder.Services.AddCorsAPI(CorsPolicyName);
builder.Services.AddBusinessServices(builder.Configuration);






var app = builder.Build();
var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();


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

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseHealthChecks("/status");
app.UseRouting();
app.UseSwaggerConfig(provider);
app.MapControllers();

// Terceiro passo: Habilita o Compartilhamento de Recursos entre Origens para qualquer URL solicitada.
// (CORS: Cross-Origin Resource Sharing)
app.UseCors(CorsPolicyName);

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapGet("/", async context => { await context.Response.WriteAsync("Catalog microservice API UP!"); });
//    endpoints.MapControllers();
//});




const string ptBr = "pt-BR";
var brazilianPortuguese = new CultureInfo(ptBr);
var supportedCultures = new[] { brazilianPortuguese };
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(ptBr),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
});

// ultimo passo: Habilita o fornecimento de arquivos estáticos, presentes na pasta "wwwroot".
app.UseStaticFiles();
app.Run();
