using Web.Services;

const string httpClientApiConfigurationName = "api";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddHttpClient(httpClientApiConfigurationName)
    .ConfigureHttpClient(http => http.BaseAddress = builder.Configuration.GetServiceUri(httpClientApiConfigurationName));

builder.Services.AddSingleton<IHttpApiClient>(sp =>
{
    var http = sp.GetRequiredService<IHttpClientFactory>().CreateClient(httpClientApiConfigurationName);

    if (http is null) throw new Exception();

    return new HttpApiClient(http);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
