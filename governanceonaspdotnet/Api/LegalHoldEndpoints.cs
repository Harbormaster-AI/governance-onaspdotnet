using governanceonaspdotnet.Service;
using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Api;

public static class LegalHoldEndpoints
{
    public static IEndpointRouteBuilder MapLegalHoldEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/legalHold").WithTags("LegalHolds");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignMatter", AssignMatter);
        group.MapPut("/unassignMatter", UnassignMatter);

    group.MapPut("/addToRepositories", AddToRepositories);
    group.MapPut("/removeFromRepositories", RemoveFromRepositories);

    group.MapPut("/addToRecords", AddToRecords);
    group.MapPut("/removeFromRecords", RemoveFromRecords);


        return app;
    }

    private static async Task<IResult> Create(
        LegalHoldRequest request,
        ILegalHoldService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToLegalHold( request );

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
        LegalHoldRequest request,
        ILegalHoldService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToLegalHold( request );

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
        ILegalHoldService service,
        CancellationToken cancellationToken) {

        var legalHold = await service.Get(identifier, cancellationToken);
        return legalHold is null ? Results.NotFound() : Results.Ok( legalHold );
    }


    private static async Task<IResult> GetAll(
        ILegalHoldService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( LegalHoldResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ILegalHoldService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignMatter(
        AssociationRequest request,
        ILegalHoldService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignMatter(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignMatter(
    AssociationRequest request,
    ILegalHoldService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignMatter(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToRepositories(
        MultipleAssociationRequest request,
        ILegalHoldService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToRepositories(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromRepositories(
        MultipleAssociationRequest request,
        ILegalHoldService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromRepositories(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToRecords(
        MultipleAssociationRequest request,
        ILegalHoldService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToRecords(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromRecords(
        MultipleAssociationRequest request,
        ILegalHoldService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromRecords(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static LegalHold mapRequestToLegalHold( LegalHoldRequest request ) {
        var model = new LegalHold
        {
            Id = request.Id,
            Name = request.Name,
            Reason = request.Reason,
            IssuedDate = request.IssuedDate,
            ReleaseDate = request.ReleaseDate,
            HoldStatus = request.HoldStatus,
        };
        return model;
    }

}
