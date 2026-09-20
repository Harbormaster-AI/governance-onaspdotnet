using governanceonaspdotnet.Service;
using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Api;

public static class MatterEndpoints
{
    public static IEndpointRouteBuilder MapMatterEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/matter").WithTags("Matters");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignOrganization", AssignOrganization);
        group.MapPut("/unassignOrganization", UnassignOrganization);

    group.MapPut("/addToLegalHolds", AddToLegalHolds);
    group.MapPut("/removeFromLegalHolds", RemoveFromLegalHolds);

    group.MapPut("/addToDataBreaches", AddToDataBreaches);
    group.MapPut("/removeFromDataBreaches", RemoveFromDataBreaches);

    group.MapPut("/addToContracts", AddToContracts);
    group.MapPut("/removeFromContracts", RemoveFromContracts);


        return app;
    }

    private static async Task<IResult> Create(
        MatterRequest request,
        IMatterService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToMatter( request );

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
        MatterRequest request,
        IMatterService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToMatter( request );

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
        IMatterService service,
        CancellationToken cancellationToken) {

        var matter = await service.Get(identifier, cancellationToken);
        return matter is null ? Results.NotFound() : Results.Ok( matter );
    }


    private static async Task<IResult> GetAll(
        IMatterService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( MatterResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IMatterService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignOrganization(
        AssociationRequest request,
        IMatterService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignOrganization(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignOrganization(
    AssociationRequest request,
    IMatterService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignOrganization(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToLegalHolds(
        MultipleAssociationRequest request,
        IMatterService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToLegalHolds(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromLegalHolds(
        MultipleAssociationRequest request,
        IMatterService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromLegalHolds(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToDataBreaches(
        MultipleAssociationRequest request,
        IMatterService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToDataBreaches(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromDataBreaches(
        MultipleAssociationRequest request,
        IMatterService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromDataBreaches(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToContracts(
        MultipleAssociationRequest request,
        IMatterService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToContracts(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromContracts(
        MultipleAssociationRequest request,
        IMatterService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromContracts(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Matter mapRequestToMatter( MatterRequest request ) {
        var model = new Matter
        {
            Id = request.Id,
            MatterName = request.MatterName,
            LeadCounsel = request.LeadCounsel,
            MatterType = request.MatterType,
            Status = request.Status,
        };
        return model;
    }

}
