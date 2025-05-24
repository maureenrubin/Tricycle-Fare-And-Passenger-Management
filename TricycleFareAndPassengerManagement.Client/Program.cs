using Blazored.LocalStorage;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.IdentityModel.Tokens;
using MudBlazor.Services;
using TricycleFareAndPassengerManagement.Client.Components;
using TricycleFareAndPassengerManagement.Client.Security;
using TricycleFareAndPassengerManagement.Client.Services;
using TricycleFareAndPassengerManagement.Client.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// HTTP Client configuration
builder.Services.AddHttpClient("API", client =>
{
    client.BaseAddress = new Uri("https://localhost:7224");
}).AddHttpMessageHandler<AuthTokenHandler>();

// Authentication configuration (FIXED - removed duplicate)
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false, // You might want to configure this properly
            ValidateLifetime = true,
            ValidateIssuerSigningKey = false // Configure this based on your needs
        };
    });

builder.Services.AddAuthorization();

// Blazor services
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Third-party services
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddMudServices();

// Custom services
builder.Services.AddScoped<AuthTokenHandler>();
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("API"));
builder.Services.AddScoped<CustomAuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider =>
    provider.GetRequiredService<CustomAuthStateProvider>());
builder.Services.AddScoped<IAuthService, AuthService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

// IMPORTANT: Add authentication middleware (MISSING)
app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();