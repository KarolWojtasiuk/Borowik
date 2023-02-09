using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.FluentValidation;
using Blazorise.Icons.FontAwesome;
using Blazorise.LoadingIndicator;
using Borowik;
using Borowik.Database.Dexie;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Borowik.Gui.Wasm;
using Borowik.Gui.Wasm.Services;
using FluentValidation;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services
    .AddBorowik()
    .AddBorowikDexie()
    .AddScoped<IExceptionHandler, ExceptionHandler>()
    .AddBlazorise(o => { o.Immediate = true; })
    .AddBootstrap5Providers()
    .AddFontAwesomeIcons()
    .AddLoadingIndicator()
    .AddBlazoriseFluentValidation()
    .AddValidatorsFromAssemblyContaining<Program>()
    .AddScoped(_ => new HttpClient
    {
        BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
    });

await builder.Build().RunAsync();