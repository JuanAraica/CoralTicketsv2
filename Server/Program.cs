using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Radzen;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;
using Microsoft.AspNetCore.OData;
using CoralTickets.Server.Data;
using Microsoft.AspNetCore.Identity;
using CoralTickets.Server.Models;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();
builder.Services.AddSingleton(sp =>
{
    // Get the address that the app is currently running at
    var server = sp.GetRequiredService<IServer>();
    var addressFeature = server.Features.Get<IServerAddressesFeature>();
    string baseAddress = addressFeature.Addresses.First();
    return new HttpClient{BaseAddress = new Uri(baseAddress)};
});
builder.Services.AddScoped<CoralTickets.Server.db_a905b1_coraldbService>();
builder.Services.AddDbContext<CoralTickets.Server.Data.db_a905b1_coraldbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("db_a905b1_coraldbConnection"));
});
builder.Services.AddControllers().AddOData(opt =>
{
    var oDataBuilderdb_a905b1_coraldb = new ODataConventionModelBuilder();
    oDataBuilderdb_a905b1_coraldb.EntitySet<CoralTickets.Server.Models.db_a905b1_coraldb.Coralticket>("Coraltickets");
    oDataBuilderdb_a905b1_coraldb.EntitySet<CoralTickets.Server.Models.db_a905b1_coraldb.Equipo>("Equipos");
    oDataBuilderdb_a905b1_coraldb.EntitySet<CoralTickets.Server.Models.db_a905b1_coraldb.History>("Histories");
    oDataBuilderdb_a905b1_coraldb.EntitySet<CoralTickets.Server.Models.db_a905b1_coraldb.Mantenimiento>("Mantenimientos");
    oDataBuilderdb_a905b1_coraldb.EntitySet<CoralTickets.Server.Models.db_a905b1_coraldb.TicketUser>("TicketUsers");
    opt.AddRouteComponents("odata/db_a905b1_coraldb", oDataBuilderdb_a905b1_coraldb.GetEdmModel()).Count().Filter().OrderBy().Expand().Select().SetMaxTop(null).TimeZone = TimeZoneInfo.Utc;
});
builder.Services.AddScoped<CoralTickets.Client.db_a905b1_coraldbService>();
builder.Services.AddHttpClient("CoralTickets.Server").AddHeaderPropagation(o => o.Headers.Add("Cookie"));
builder.Services.AddHeaderPropagation(o => o.Headers.Add("Cookie"));
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddScoped<CoralTickets.Client.SecurityService>();
builder.Services.AddDbContext<ApplicationIdentityDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("db_a905b1_coraldbConnection"));
});
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>().AddEntityFrameworkStores<ApplicationIdentityDbContext>().AddDefaultTokenProviders();
builder.Services.AddControllers().AddOData(o =>
{
    var oDataBuilder = new ODataConventionModelBuilder();
    oDataBuilder.EntitySet<ApplicationUser>("ApplicationUsers");
    var usersType = oDataBuilder.StructuralTypes.First(x => x.ClrType == typeof(ApplicationUser));
    usersType.AddProperty(typeof(ApplicationUser).GetProperty(nameof(ApplicationUser.Password)));
    usersType.AddProperty(typeof(ApplicationUser).GetProperty(nameof(ApplicationUser.ConfirmPassword)));
    oDataBuilder.EntitySet<ApplicationRole>("ApplicationRoles");
    o.AddRouteComponents("odata/Identity", oDataBuilder.GetEdmModel()).Count().Filter().OrderBy().Expand().Select().SetMaxTop(null).TimeZone = TimeZoneInfo.Utc;
});
builder.Services.AddScoped<AuthenticationStateProvider, CoralTickets.Client.ApplicationAuthenticationStateProvider>();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseHeaderPropagation();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllers();
app.MapFallbackToPage("/_Host");
app.Services.CreateScope().ServiceProvider.GetRequiredService<ApplicationIdentityDbContext>().Database.Migrate();
app.Run();