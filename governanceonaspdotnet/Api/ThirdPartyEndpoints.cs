using governanceonaspdotnet.Service;
using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Api;

public static class ThirdPartyEndpoints
{
    public static IEndpointRouteBuilder MapThirdPartyEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/thirdParty").WithTags("ThirdPartys");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignOrganization", AssignOrganization);
        group.MapPut("/unassignOrganization", UnassignOrganization);

    group.MapPut("/addToProcessingActivities", AddToProcessingActivities);
    group.MapPut("/removeFromProcessingActivities", RemoveFromProcessingActivities);

    group.MapPut("/addToAssessments", AddToAssessments);
    group.MapPut("/removeFromAssessments", RemoveFromAssessments);

    group.MapPut("/addToContracts", AddToContracts);
    group.MapPut("/removeFromContracts", RemoveFromContracts);

    group.MapPut("/addToObligations", AddToObligations);
    group.MapPut("/removeFromObligations", RemoveFromObligations);

    group.MapPut("/addToDataBreaches", AddToDataBreaches);
    group.MapPut("/removeFromDataBreaches", RemoveFromDataBreaches);


        return app;
    }

    private static async Task<IResult> Create(
        ThirdPartyRequest request,
        IThirdPartyService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToThirdParty( request );

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
        ThirdPartyRequest request,
        IThirdPartyService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToThirdParty( request );

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
        IThirdPartyService service,
        CancellationToken cancellationToken) {

        var thirdParty = await service.Get(identifier, cancellationToken);
        return thirdParty is null ? Results.NotFound() : Results.Ok( thirdParty );
    }


    private static async Task<IResult> GetAll(
        IThirdPartyService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( ThirdPartyResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IThirdPartyService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignOrganization(
        AssociationRequest request,
        IThirdPartyService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignOrganization(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignOrganization(
    AssociationRequest request,
    IThirdPartyService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignOrganization(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToProcessingActivities(
        MultipleAssociationRequest request,
        IThirdPartyService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToProcessingActivities(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromProcessingActivities(
        MultipleAssociationRequest request,
        IThirdPartyService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromProcessingActivities(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToAssessments(
        MultipleAssociationRequest request,
        IThirdPartyService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToAssessments(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromAssessments(
        MultipleAssociationRequest request,
        IThirdPartyService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromAssessments(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToContracts(
        MultipleAssociationRequest request,
        IThirdPartyService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToContracts(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromContracts(
        MultipleAssociationRequest request,
        IThirdPartyService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromContracts(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToObligations(
        MultipleAssociationRequest request,
        IThirdPartyService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToObligations(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromObligations(
        MultipleAssociationRequest request,
        IThirdPartyService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromObligations(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToDataBreaches(
        MultipleAssociationRequest request,
        IThirdPartyService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToDataBreaches(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromDataBreaches(
        MultipleAssociationRequest request,
        IThirdPartyService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromDataBreaches(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static ThirdParty mapRequestToThirdParty( ThirdPartyRequest request ) {
        var model = new ThirdParty
        {
            Id = request.Id,
            Name = request.Name,
            Country = request.Country,
            ContactEmail = request.ContactEmail,
            ThirdPartyType = request.ThirdPartyType,
            Criticality = request.Criticality,
        };
        return model;
    }

}
