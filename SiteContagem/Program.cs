using StackExchange.Redis;
using SiteContagem.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddHealthChecks();

builder.Services.AddSingleton<ConnectionMultiplexer>(
    ConnectionMultiplexer.Connect(
        builder.Configuration.GetConnectionString("Redis"))
);

builder.Services.AddApplicationInsightsTelemetry(
    builder.Configuration);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHealthChecks("/status", HealthChecksExtensions.GetJsonReturn());

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
