using System.Diagnostics;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StackExchange.Redis;
using SiteContagem.Logging;

namespace SiteContagem.Pages;

public class IndexModel : PageModel
{
    public void OnGet([FromServices] ILogger<IndexModel> logger,
        [FromServices] IConfiguration configuration,
        [FromServices] ConnectionMultiplexer connectionRedis,
        [FromServices] TelemetryConfiguration telemetryConfig)
    {
        DateTimeOffset inicio = DateTime.Now;
        var watch = new Stopwatch();
        watch.Start();
        var valorAtual =
            (int)connectionRedis.GetDatabase().StringIncrement("APIContagem");;
        logger.LogValorAtual(valorAtual);
        watch.Stop();
        TelemetryClient client = new (telemetryConfig);
        client.TrackDependency(
            "Redis", "Get", $"valor = {valorAtual}", inicio, watch.Elapsed, true);

        TempData["Contador"] = valorAtual;
        TempData["Local"] = InfoContador.Local;
        TempData["Kernel"] = InfoContador.Kernel;
        TempData["Framework"] = InfoContador.Framework;
        TempData["MensagemFixa"] = "Teste";
        TempData["MensagemVariavel"] = configuration["MensagemVariavel"];
    }
}