using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using Borowik;
using Borowik.Database.Dexie;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Borowik.Gui.Wasm;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services
    .AddBorowik()
    .AddBorowikDexie()
    .AddBlazorise(o => { o.Immediate = true; })
    .AddBootstrap5Providers()
    .AddFontAwesomeIcons()
    .AddScoped(_ => new HttpClient
    {
        BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
    });

await builder.Build().RunAsync();