using Core.Common.Extensions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Sigc.Core.Caching.ComponentModel;
using Sigc.Core.Caching.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOptions();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHealthChecks();
builder.Services.AddMvc(); // ==>  Necessario para uso da Interface IDistributedCache


builder.Services.AddApplicationEvents();
builder.Services.AddApiVersioningConfig();
builder.Services.AddJwtconfig(builder.Configuration, null);
builder.Services.AddSwaggerConfig();
//builder.Services.AddCaching(builder.Configuration);


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

// Quarto passo: Configura e Habilita a exposição de documentação da API via "Swagger".
app.UseSwaggerConfig(provider);


app.UseAuthorization();
app.UseRouting();
app.MapControllers();

//app.UseRedisDistributedCache(ApplicationType.SalesPromotions);

app.Run();
