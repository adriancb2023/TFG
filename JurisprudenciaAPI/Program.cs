var builder = WebApplication.CreateBuilder(args);

// Define CORS policy name
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.

// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy  =>
                      {
                          policy.AllowAnyOrigin() // For development; restrict in production
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Enable CORS middleware - MUST be before UseAuthorization and MapControllers
app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();
app.MapRazorPages();

app.Run();
