using Microsoft.EntityFrameworkCore;
using SuggestionAPI.Hubs;
using Common.Repositories;
using Common.Options;
using SuggestionAPI.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

builder.Services.AddScoped<ISuggestionRepository, SuggestionRepository>(); 

builder.Services.AddScoped<ISuggestionService, SuggestionService>();

var connectionOptions = builder.Configuration.GetSection("ConnectionSqlOptions").Get<ConnectionSqlOptions>();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<SuggestionDbContext>(options =>
            options.UseInMemoryDatabase("SuggestionBox"));
}
else
{
    builder.Services.Configure<ConnectionSqlOptions>(builder.Configuration.GetSection("ConnectionSqlOptions"));
    builder.Services.AddDbContext<SuggestionDbContext>(options =>
        options.UseSqlServer(connectionOptions.DefaultConnection, sqlServerOptionsAction: sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null);
        }));
}

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseResponseCaching();
app.UseAuthorization();

app.MapControllers();
app.MapHub<SuggestionHub>("/suggestionhub").AllowAnonymous();

app.Run();