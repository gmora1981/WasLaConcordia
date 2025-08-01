using Identity.Api.Interfaces;
using LaConcordia;
using LaConcordia.Auth;
using LaConcordia.Helpers;
using LaConcordia.Interface;
using LaConcordia.Repository;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
// CONFIGURACIÓN CORREGIDA PARA DESARROLLO Y PRODUCCIÓN
#if DEBUG
// DESARROLLO: Tu API local debe estar corriendo en puerto 5191
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("http://localhost:5191/"),
    // BaseAddress = new Uri("https://api.laconcordia.compugtech.com/"),
    Timeout = TimeSpan.FromSeconds(30)
});
Console.WriteLine("🔧 DESARROLLO - API: http://localhost:5191/");
#else
    // PRODUCCIÓN: SmartASP.NET
    builder.Services.AddScoped(sp => new HttpClient 
    { 
        BaseAddress = new Uri("https://api.laconcordia.compugtech.com/"),
        Timeout = TimeSpan.FromSeconds(60)
    });
    Console.WriteLine("🔧 PRODUCCIÓN - API: https://api.laconcordia.compugtech.com/");
#endif

//builder.Services.AddTelerikBlazor();
builder.Services.AddScoped<IEmpresa, EmpresaRepository>();
builder.Services.AddScoped<ITipolicencium, TipolicenciumRepository>();
builder.Services.AddScoped<INacionalidad, NacionalidadRepository>();
builder.Services.AddScoped<IEstadoCivil, EstadoCivilRepository>();
builder.Services.AddScoped<INiveleducacion, NiveleducacionRepository>();
builder.Services.AddScoped<ICargo, CargoRepository>();
builder.Services.AddScoped<IParentesco, ParentescoRepository>();
builder.Services.AddScoped<IDuenopuesto, DuenopuestoRepository>();
builder.Services.AddScoped<IUnidad, UnidadRepository>();
builder.Services.AddScoped<IFichapersonal, FichapersonalRepository>();

configureservices(builder.Services);

await builder.Build().RunAsync();

static void configureservices(IServiceCollection services)
{
    services.AddScoped<IHttpService, HttpService>();
    services.AddScoped<IAccountsRepository, AccountsRepository>();
    services.AddScoped<INavigationRepository, NavigationRepository>();
    services.AddScoped<GenericoRepositorio>();
    services.AddScoped<IGenericoRepositorio, GenericoRepositorio>();
    services.AddScoped<IDisplayMessage, DisplayMessage>();
    services.AddScoped<IUsersRepository, UserRepository>();
    // NUEVO: Servicio de Permisos
    services.AddScoped<IPermissionService, PermissionService>();
    services.AddAuthorizationCore();
    services.AddScoped<TokenRenewer>();
    services.AddScoped<JWTAuthenticationStateProvider>();
    services.AddScoped<AuthenticationStateProvider, JWTAuthenticationStateProvider>(provider => provider.GetRequiredService<JWTAuthenticationStateProvider>());
    services.AddScoped<ILoginService, JWTAuthenticationStateProvider>(provider => provider.GetRequiredService<JWTAuthenticationStateProvider>());
}