using governanceonaspdotnet.Service;
using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Api;

public static class ProcedureEndpoints
{
    public static IEndpointRouteBuilder MapProcedureEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/procedure").WithTags("Procedures");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignPolicy", AssignPolicy);
        group.MapPut("/unassignPolicy", UnassignPolicy);

    group.MapPut("/addToControls", AddToControls);
    group.MapPut("/removeFromControls", RemoveFromControls);


        return app;
    }

    private static async Task<IResult> Create(
        ProcedureRequest request,
        IProcedureService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToProcedure( request );

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
        ProcedureRequest request,
        IProcedureService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToProcedure( request );

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
        IProcedureService service,
        CancellationToken cancellationToken) {

        var procedure = await service.Get(identifier, cancellationToken);
        return procedure is null ? Results.NotFound() : Results.Ok( procedure );
    }


    private static async Task<IResult> GetAll(
        IProcedureService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( ProcedureResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IProcedureService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignPolicy(
        AssociationRequest request,
        IProcedureService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignPolicy(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignPolicy(
    AssociationRequest request,
    IProcedureService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignPolicy(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToControls(
        MultipleAssociationRequest request,
        IProcedureService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToControls(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromControls(
        MultipleAssociationRequest request,
        IProcedureService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromControls(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Procedure mapRequestToProcedure( ProcedureRequest request ) {
        var model = new Procedure
        {
            Id = request.Id,
            Title = request.Title,
            VersionLabel = request.VersionLabel,
            Status = request.Status,
        };
        return model;
    }

}
