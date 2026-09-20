using governanceonaspdotnet.Service;
using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Api;

public static class ContractEndpoints
{
    public static IEndpointRouteBuilder MapContractEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/contract").WithTags("Contracts");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignThirdParty", AssignThirdParty);
        group.MapPut("/unassignThirdParty", UnassignThirdParty);
        group.MapPut("/assignMatter", AssignMatter);
        group.MapPut("/unassignMatter", UnassignMatter);

    group.MapPut("/addToObligations", AddToObligations);
    group.MapPut("/removeFromObligations", RemoveFromObligations);

    group.MapPut("/addToDataProcessingActivities", AddToDataProcessingActivities);
    group.MapPut("/removeFromDataProcessingActivities", RemoveFromDataProcessingActivities);


        return app;
    }

    private static async Task<IResult> Create(
        ContractRequest request,
        IContractService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToContract( request );

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
        ContractRequest request,
        IContractService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToContract( request );

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
        IContractService service,
        CancellationToken cancellationToken) {

        var contract = await service.Get(identifier, cancellationToken);
        return contract is null ? Results.NotFound() : Results.Ok( contract );
    }


    private static async Task<IResult> GetAll(
        IContractService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( ContractResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IContractService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignThirdParty(
        AssociationRequest request,
        IContractService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignThirdParty(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignThirdParty(
    AssociationRequest request,
    IContractService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignThirdParty(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignMatter(
        AssociationRequest request,
        IContractService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignMatter(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignMatter(
    AssociationRequest request,
    IContractService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignMatter(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToObligations(
        MultipleAssociationRequest request,
        IContractService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToObligations(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromObligations(
        MultipleAssociationRequest request,
        IContractService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromObligations(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToDataProcessingActivities(
        MultipleAssociationRequest request,
        IContractService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToDataProcessingActivities(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromDataProcessingActivities(
        MultipleAssociationRequest request,
        IContractService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromDataProcessingActivities(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Contract mapRequestToContract( ContractRequest request ) {
        var model = new Contract
        {
            Id = request.Id,
            Title = request.Title,
            EffectiveDate = request.EffectiveDate,
            ExpiryDate = request.ExpiryDate,
            RepositoryUrl = request.RepositoryUrl,
            Status = request.Status,
        };
        return model;
    }

}
