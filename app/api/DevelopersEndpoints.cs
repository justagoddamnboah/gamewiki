using gamewiki.net.interfaces;
using gamewiki.net.dto;
using gamewiki.net.dto.request;
using gamewiki.net.dto.response;

namespace gamewiki.net.api;

public static class DevelopersEndpoints {
    public static RouteGroupBuilder MapDevsEndpoints(this RouteGroupBuilder api) {
        var group = api.MapGroup("/devs").WithTags("Devs");

        group.MapGet("/", async (IDevService devs, IMapper mapper) => {
                var result = await devs.GetAll();
                return Results.Ok(result.Select(mapper.Map));
            })
            .WithSummary("Получить список разработчиков")
            .WithDescription("Возвращает всех разработчиков и количество их игр.")
            .Produces<IEnumerable<DevResponse>>(StatusCodes.Status200OK);

        
        
        group.MapGet("/{devId:guid}",
                async (Guid devId, IDevService devs, IMapper mapper) => {
                    var dev = await devs.GetById(devId);
                    return dev is null
                        ? Results.NotFound(new ErrorResponse { Message = "Разработчик не найден." })
                        : Results.Ok(mapper.Map(dev));
                })
            .WithSummary("Получить разработчика по идентификатору")
            .Produces<DevResponse>(StatusCodes.Status200OK)
            .Produces<ErrorResponse>(StatusCodes.Status404NotFound);
        
        
        
        group.MapPost("/", async (CreateDevRequest body, IDevService devs, IMapper mapper) => {
                try {
                    var created = await devs.Add(body);
                    return Results.Created($"/api/devs/{created.Id}", mapper.Map(created));
                }
                catch (Exception ex) when (ex is ArgumentException or InvalidOperationException) {
                    return Results.BadRequest(new ErrorResponse { Message = ex.Message });
                }
            })
            .WithSummary("Добавить разработчика")
            .Produces<DevResponse>(StatusCodes.Status201Created)
            .Produces<ErrorResponse>(StatusCodes.Status400BadRequest);

        group.MapPut("/{devId:guid}",
                async (Guid devId, UpdateDevRequest body, IDevService devs, IMapper mapper) => {
                try {
                    var updated = await devs.Update(devId, body);
                    return updated is null
                        ? Results.NotFound(new ErrorResponse { Message = "Разработчик не найден." })
                        : Results.Ok(mapper.Map(updated));
                }
                catch (ArgumentException ex) {
                    return Results.BadRequest(new ErrorResponse { Message = ex.Message });
                }
            })
            .WithSummary("Изменить данные разработчика")
            .Produces<DevResponse>(StatusCodes.Status200OK)
            .Produces<ErrorResponse>(StatusCodes.Status400BadRequest)
            .Produces<ErrorResponse>(StatusCodes.Status404NotFound);

        group.MapDelete("/{devId:guid}",
                async (Guid devId, IDevService devs) => {
                try {
                    var deleted = await devs.Delete(devId);
                    return deleted
                        ? Results.NoContent()
                        : Results.NotFound(new ErrorResponse { Message = "Разработчик не найден." });
                }
                catch (InvalidOperationException ex) {
                    return Results.BadRequest(new ErrorResponse { Message = ex.Message });
                }
            })
            .WithSummary("Удалить разработчика")
            .Produces(StatusCodes.Status204NoContent)
            .Produces<ErrorResponse>(StatusCodes.Status400BadRequest)
            .Produces<ErrorResponse>(StatusCodes.Status404NotFound);

        return api;
    }
}