using AuthSample;
using AuthSample.MockServices;
using AuthSample.MocksInterfaces;
using AuthUsage.Extentions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ISessionServices, SessionServices>();
builder.Services.AddScoped<TokenValidator>();
builder.Services.AddScoped<AuthenticationManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseTokenExtraction();
app.UseAuthorization();

app.MapControllers();

app.Run();
