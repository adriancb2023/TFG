using Microsoft.AspNetCore.Localization;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Define CORS policy name
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Configuración radical de culturas (versión corregida)
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("es-ES");
    options.FallBackToParentCultures = false;
    options.FallBackToParentUICultures = false;
    options.RequestCultureProviders.Clear();
    options.RequestCultureProviders.Add(new CustomRequestCultureProvider(async context =>  // Añadido 'async'
        await Task.FromResult(new ProviderCultureResult("es-ES"))));  // Añadido 'await'
});

// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins, policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Configuración básica de servicios API
builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRazorPages();
builder.Services.AddMemoryCache();
builder.Services.AddHttpClient();

var app = builder.Build();

// Middleware simplificado
app.Use(async (context, next) =>
{
    context.Request.Headers.Remove("Accept-Language");
    await next();
});

app.UseRequestLocalization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    // Endpoint solo en desarrollo
    app.MapGet("/culture-debug", (HttpContext context) =>
    {
        return new
        {
            Culture = CultureInfo.CurrentCulture.Name,
            Headers = context.Request.Headers
                .Where(h => h.Key == "Accept-Language")
                .ToDictionary(h => h.Key, h => h.Value.ToString())
        };
    });
}

app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization();
app.MapControllers();
app.MapRazorPages();

app.Run();