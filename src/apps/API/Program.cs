using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Poc.Method.CompanyManagerService.Extensions;
using Poc.Method.Dal.Sql;
using Poc.Method.Dal.Sql.Extensions;
using Poc.Method.PersonManagerService.Extensions;
using Poc.Method.Service.ContextStorageAccess.Extensions;
using Poc.Method.Service.ExternalAppRedAccess;
using Poc.Method.Service.ExternalAppRedAccess.Extensions;
using Poc.Method.Service.ExternalAppYellowAccess;
using Poc.Method.Service.ExternalAppYellowAccess.Extensions;

var builder = WebApplication.CreateBuilder(args);

const string httpClientApplicationRedName = "poc-method-appredapi";
const string httpClientApplicationYellowName = "poc-method-appyellowapi";

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new Poc.Method.Service.ContextStorageAccess.Mappers.MappingProfile());
    mc.AddProfile(new Poc.Method.Service.ExternalAppRedAccess.Mappers.MappingProfile());
});

builder.Services.AddSingleton(mapperConfig.CreateMapper());

builder.Services.RegisterStorageContext(builder.Configuration);

builder.Services
    .RegisterCompanyManager()
    .RegisterPersonManager()
    .RegisterContextStorageResourceAccess()
    .RegisterExternalApplicationRedResourceAccess()
    .RegisterExternalApplicationYellowResourceAccess();

ConfigureExternalRedApplication(builder);
ConfigureExternalYellowApplication(builder);

builder.Services.AddSingleton<IHttpClientAppYellow>(sp =>
{
    var http = sp.GetRequiredService<IHttpClientFactory>().CreateClient(httpClientApplicationYellowName);

    if (http is null) throw new Exception();

    return new HttpClientAppYellow(http);
});

builder.Services.AddSingleton(sp =>
{
    var http = sp.GetRequiredService<IHttpClientFactory>().CreateClient(httpClientApplicationRedName);

    if (http is null) throw new Exception();

    return new HttpClientAppRed(http);
});



var app = builder.Build();

// migrate any database changes on startup (includes initial db creation)
using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<StorageContext>();
    dataContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static void ConfigureExternalRedApplication(WebApplicationBuilder builder)
{
    var applicationUrl = builder.Configuration.GetServiceUri(httpClientApplicationRedName);

    if (applicationUrl is not null)
    {
        // We're in Tye, so we use the provided endpoint

        builder.Services.AddHttpClient(httpClientApplicationRedName)
            .ConfigureHttpClient(http => http.BaseAddress = builder.Configuration.GetServiceUri(httpClientApplicationRedName));
    }
    else
    {
        // We're not in Tye, so we use configuration endpoint

        builder.Services.AddHttpClient(httpClientApplicationRedName)
            .ConfigureHttpClient(http =>
                http.BaseAddress =
                    new Uri(builder.Configuration.GetValue<string>("ExternalSources:ExternalRedApplicationUrl")));
    }
}

static void ConfigureExternalYellowApplication(WebApplicationBuilder builder)
{
    var applicationUrl = builder.Configuration.GetServiceUri(httpClientApplicationYellowName);

    if (applicationUrl is not null)
    {
        // We're in Tye, so we use the provided endpoint

        builder.Services.AddHttpClient(httpClientApplicationYellowName)
            .ConfigureHttpClient(http => http.BaseAddress = builder.Configuration.GetServiceUri(httpClientApplicationYellowName));
    }
    else
    {
        // We're not in Tye, so we use configuration endpoint

        builder.Services.AddHttpClient(httpClientApplicationYellowName)
            .ConfigureHttpClient(http =>
                http.BaseAddress =
                    new Uri(builder.Configuration.GetValue<string>("ExternalSources:ExternalYellowApplicationUrl")));
    }
}