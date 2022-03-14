using Core.Common.Extensions;
using Discount.Api.Repositories;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();

builder.Services.AddOptions();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHealthChecks();
builder.Services.AddMvc(); // ==>  Necessario para uso da Interface IDistributedCache


builder.Services.AddApplicationEvents();
builder.Services.AddApiVersioningConfig();
builder.Services.AddJwtconfig(builder.Configuration, null);
builder.Services.AddSwaggerConfig();


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
}

// Segundo Passo:  Configuro do Pipeline de requisições:
app.UseDefaultFiles(new DefaultFilesOptions { DefaultFileNames = new List<string> { "index.html" } });
app.UseAuthorization();
app.UseHealthChecks("/status");
app.UseRouting();
app.MapControllers();

// Quarto passo: Configura e Habilita a exposição de documentação da API via "Swagger".
app.UseSwaggerConfig(provider);
app.UseAuthorization();
app.UseRouting();
app.MapControllers();
app.Run();
