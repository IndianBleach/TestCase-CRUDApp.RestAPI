using RestAPI.Application.Extensions;
using RestAPI.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddCoreServices(builder.Configuration);

var app = builder.Build();

await AppicationDbSeed.SeedDatabaseAsync(app.Services.CreateScope());

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

await app.RunAsync();
