
using Catalog.Api.Extensions;
using Core.Common.Extensions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructureAPI(builder.Configuration);
builder.Services.AddBusinessServices(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//app.UseSwaggerConfig();

app.MapControllers();
//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapGet("/", async context => { await context.Response.WriteAsync("Catalog microservice API UP!"); });
//    endpoints.MapControllers();
//});
app.Run();
