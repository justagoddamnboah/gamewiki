using gamewiki.net.api;
using gamewiki.net.database;
using gamewiki.net.dto;
using gamewiki.net.interfaces;
using gamewiki.net.services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.SwaggerDoc("v1", new() {
            Title = "Game Wiki API",
        Version = "v1",
        Description = "Демонстрационный API игрового вики."
    });
});

var connectionString = builder.Configuration.GetConnectionString("Postgres")
    ?? throw new InvalidOperationException("Не найдена строка подключения ConnectionStrings:Postgres.");

builder.Services.AddDbContext<WikiDbContext>(options => {
    options.UseNpgsql(connectionString);
});

builder.Services.AddScoped<IDevService, DevService>();
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IMapper, Mapper>();

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    using (var scope = app.Services.CreateScope()) {
        var db = scope.ServiceProvider.GetRequiredService<WikiDbContext>();
        await db.Database.EnsureCreatedAsync();
    }

    app.UseSwagger();
    app.UseSwaggerUI(options => {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Game Wiki API v1");
        options.RoutePrefix = "swagger";
    });
}

var api = app.MapGroup("/api");
api.MapDevsEndpoints();
api.MapGamesEndpoints();

app.MapHealthCheck();

app.MapGet("/", () => Results.Ok(new {
    message = "Game Wiki API работает."
}));

await app.RunAsync();