using governanceonaspdotnet.Service;
using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Api;

public static class ObligationEndpoints
{
    public static IEndpointRouteBuilder MapObligationEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/obligation").WithTags("Obligations");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignRegulation", AssignRegulation);
        group.MapPut("/unassignRegulation", UnassignRegulation);

    group.MapPut("/addToControls", AddToControls);
    group.MapPut("/removeFromControls", RemoveFromControls);

    group.MapPut("/addToPolicies", AddToPolicies);
    group.MapPut("/removeFromPolicies", RemoveFromPolicies);

    group.MapPut("/addToContracts", AddToContracts);
    group.MapPut("/removeFromContracts", RemoveFromContracts);


        return app;
    }

    private static async Task<IResult> Create(
        ObligationRequest request,
        IObligationService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToObligation( request );

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
        ObligationRequest request,
        IObligationService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToObligation( request );

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
        IObligationService service,
        CancellationToken cancellationToken) {

        var obligation = await service.Get(identifier, cancellationToken);
        return obligation is null ? Results.NotFound() : Results.Ok( obligation );
    }


    private static async Task<IResult> GetAll(
        IObligationService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( ObligationResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IObligationService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignRegulation(
        AssociationRequest request,
        IObligationService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignRegulation(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignRegulation(
    AssociationRequest request,
    IObligationService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignRegulation(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToControls(
        MultipleAssociationRequest request,
        IObligationService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToControls(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromControls(
        MultipleAssociationRequest request,
        IObligationService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromControls(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToPolicies(
        MultipleAssociationRequest request,
        IObligationService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToPolicies(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromPolicies(
        MultipleAssociationRequest request,
        IObligationService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromPolicies(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToContracts(
        MultipleAssociationRequest request,
        IObligationService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToContracts(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromContracts(
        MultipleAssociationRequest request,
        IObligationService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromContracts(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Obligation mapRequestToObligation( ObligationRequest request ) {
        var model = new Obligation
        {
            Id = request.Id,
            ReferenceNumber = request.ReferenceNumber,
            DescriptionText = request.DescriptionText,
            ObligationType = request.ObligationType,
            ReviewFrequency = request.ReviewFrequency,
        };
        return model;
    }

}
