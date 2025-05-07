using FrontEndGestionMaquinasVirtuales;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Blazored.LocalStorage;
using FrontEndGestionMaquinasVirtuales.Servicios;
using Microsoft.Azure.SignalR;

var builder = WebApplication.CreateBuilder(args);

// Cargar configuración desde appsettings.json (ya está por defecto en Blazor Server)
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Registrar configuración como singleton
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

// Servicios Blazor Server
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Servicios personalizados
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<ApiGestionMaquinas>();
builder.Services.AddSingleton<SignalRService>();

// HttpClient configurado con la URL desde ApiSettings
builder.Services.AddScoped(sp =>
    new HttpClient { BaseAddress = new Uri(builder.Configuration["ApiSettings:BaseUrl"] ?? "https://localhost:5001/api") }
);
builder.Services.AddSignalR().AddAzureSignalR("Endpoint=https://apigestionmaquinasvirtuales.service.signalr.net;AccessKey=3xLHyuOmuR2YK4pGUVJ7Xsp5vBQmRYj4E1tA541QJr799cdAjpaaJQQJ99BEACBsN54XJ3w3AAAAASRSNqHc;Version=1.0;");

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
