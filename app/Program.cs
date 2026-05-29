using gamewiki.net.api;
using gamewiki.net.dto;
using gamewiki.net.interfaces;
using gamewiki.net.services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.SwaggerDoc("v1", new() {
        Title = "Game Wiki API",
        Version = "v1",
        Description = "Демонстрационный API игрового вики (in-memory)."
    });
});

builder.Services.AddSingleton<IDevService, DevService>();
builder.Services.AddSingleton<IGameService, GameService>();
builder.Services.AddScoped<IMapper, Mapper>();

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
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