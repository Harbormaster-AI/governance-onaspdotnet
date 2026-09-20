using governanceonaspdotnet.Service;
using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Api;

public static class RetentionScheduleEndpoints
{
    public static IEndpointRouteBuilder MapRetentionScheduleEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/retentionSchedule").WithTags("RetentionSchedules");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);


    group.MapPut("/addToRepositories", AddToRepositories);
    group.MapPut("/removeFromRepositories", RemoveFromRepositories);

    group.MapPut("/addToRecords", AddToRecords);
    group.MapPut("/removeFromRecords", RemoveFromRecords);

    group.MapPut("/addToExceptions", AddToExceptions);
    group.MapPut("/removeFromExceptions", RemoveFromExceptions);

    group.MapPut("/addToDispositionReviews", AddToDispositionReviews);
    group.MapPut("/removeFromDispositionReviews", RemoveFromDispositionReviews);


        return app;
    }

    private static async Task<IResult> Create(
        RetentionScheduleRequest request,
        IRetentionScheduleService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToRetentionSchedule( request );

        try
        {
            await service.Create(model, cancellationToken);
        }
        catch (InvalidOperationException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }

        return Results.NoContent();
    }

    private static async Task<IResult> Update(
        RetentionScheduleRequest request,
        IRetentionScheduleService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToRetentionSchedule( request );

        try
        {
            var updated = await service.Update(model, cancellationToken);
            return updated ? Results.NoContent() : Results.NotFound();
        }
        catch (InvalidOperationException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }
    }


    private static async Task<IResult> Get(
        IdentifierRequest identifier,
        IRetentionScheduleService service,
        CancellationToken cancellationToken) {

        var retentionSchedule = await service.Get(identifier, cancellationToken);
        return retentionSchedule is null ? Results.NotFound() : Results.Ok( retentionSchedule );
    }


    private static async Task<IResult> GetAll(
        IRetentionScheduleService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( RetentionScheduleResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IRetentionScheduleService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToRepositories(
        MultipleAssociationRequest request,
        IRetentionScheduleService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToRepositories(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromRepositories(
        MultipleAssociationRequest request,
        IRetentionScheduleService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromRepositories(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToRecords(
        MultipleAssociationRequest request,
        IRetentionScheduleService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToRecords(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromRecords(
        MultipleAssociationRequest request,
        IRetentionScheduleService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromRecords(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToExceptions(
        MultipleAssociationRequest request,
        IRetentionScheduleService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToExceptions(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromExceptions(
        MultipleAssociationRequest request,
        IRetentionScheduleService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromExceptions(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToDispositionReviews(
        MultipleAssociationRequest request,
        IRetentionScheduleService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToDispositionReviews(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromDispositionReviews(
        MultipleAssociationRequest request,
        IRetentionScheduleService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromDispositionReviews(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static RetentionSchedule mapRequestToRetentionSchedule( RetentionScheduleRequest request ) {
        var model = new RetentionSchedule
        {
            Id = request.Id,
            Name = request.Name,
            RetentionPeriodMonths = request.RetentionPeriodMonths,
            RetentionTrigger = request.RetentionTrigger,
            DispositionAction = request.DispositionAction,
            Status = request.Status,
        };
        return model;
    }

}
