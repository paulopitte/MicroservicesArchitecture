using Basket.Api.Extensions;
using Sigc.Core.Caching.ComponentModel;
using Sigc.Core.Caching.Extensions;


/// <summary>
/// Nome da politica utilizada para o CORS.
/// </summary>
const string CorsPolicyName = "KnownHostsOnly"; //"AllowKnownHosts";



var builder = WebApplication.CreateBuilder(args);


builder.Services.AddInfrastructureAPI(builder.Configuration);
builder.Services.AddCorsAPI(CorsPolicyName);
builder.Services.AddBusinessServices(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();


app.UseRedisDistributedCache(ApplicationType.Catalog);

app.Run();
