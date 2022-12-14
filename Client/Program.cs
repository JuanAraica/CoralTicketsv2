using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;
using CoralTickets.Client;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();
builder.Services.AddTransient(sp => new HttpClient{BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)});
builder.Services.AddScoped<CoralTickets.Client.db_a905b1_coraldbService>();
builder.Services.AddAuthorizationCore();
builder.Services.AddHttpClient("CoralTickets.Server", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
builder.Services.AddTransient(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("CoralTickets.Server"));
builder.Services.AddScoped<CoralTickets.Client.SecurityService>();
builder.Services.AddScoped<AuthenticationStateProvider, CoralTickets.Client.ApplicationAuthenticationStateProvider>();
var host = builder.Build();
await host.RunAsync();