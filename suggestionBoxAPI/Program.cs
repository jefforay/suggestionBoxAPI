using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SuggestionAPI.Hubs;
using Common.Repositories;
using Common.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

using (var serviceProvider = builder.Services.BuildServiceProvider())
{
    var connectionOptions = serviceProvider.GetRequiredService<IOptions<ConnectionSqlOptions>>().Value;
        builder.Services.AddDbContext<SuggestionDbContext>(options =>
            options.UseSqlServer(connectionOptions.DefaultConnection, sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null);
                }) );
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
