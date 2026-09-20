using governanceonaspdotnet.Service;
using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Api;

public static class System_Endpoints
{
    public static IEndpointRouteBuilder MapSystem_Endpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/system_").WithTags("System_s");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);


    group.MapPut("/addToProcessingActivities", AddToProcessingActivities);
    group.MapPut("/removeFromProcessingActivities", RemoveFromProcessingActivities);

    group.MapPut("/addToRecordsRepositories", AddToRecordsRepositories);
    group.MapPut("/removeFromRecordsRepositories", RemoveFromRecordsRepositories);


        return app;
    }

    private static async Task<IResult> Create(
        System_Request request,
        ISystem_Service service,
        CancellationToken cancellationToken) {

        var model = mapRequestToSystem_( request );

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
        System_Request request,
        ISystem_Service service,
        CancellationToken cancellationToken) {

        var model = mapRequestToSystem_( request );

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
        ISystem_Service service,
        CancellationToken cancellationToken) {

        var system_ = await service.Get(identifier, cancellationToken);
        return system_ is null ? Results.NotFound() : Results.Ok( system_ );
    }


    private static async Task<IResult> GetAll(
        ISystem_Service service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( System_Response.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ISystem_Service service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToProcessingActivities(
        MultipleAssociationRequest request,
        ISystem_Service service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToProcessingActivities(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromProcessingActivities(
        MultipleAssociationRequest request,
        ISystem_Service service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromProcessingActivities(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToRecordsRepositories(
        MultipleAssociationRequest request,
        ISystem_Service service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToRecordsRepositories(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromRecordsRepositories(
        MultipleAssociationRequest request,
        ISystem_Service service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromRecordsRepositories(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static System_ mapRequestToSystem_( System_Request request ) {
        var model = new System_
        {
            Id = request.Id,
            Name = request.Name,
            OwnerDepartment = request.OwnerDepartment,
            SystemType = request.SystemType,
        };
        return model;
    }

}
