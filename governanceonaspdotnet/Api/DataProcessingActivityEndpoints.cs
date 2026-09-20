using governanceonaspdotnet.Service;
using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Api;

public static class DataProcessingActivityEndpoints
{
    public static IEndpointRouteBuilder MapDataProcessingActivityEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/dataProcessingActivity").WithTags("DataProcessingActivitys");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignOrganization", AssignOrganization);
        group.MapPut("/unassignOrganization", UnassignOrganization);

    group.MapPut("/addToDataCategories", AddToDataCategories);
    group.MapPut("/removeFromDataCategories", RemoveFromDataCategories);

    group.MapPut("/addToSystems", AddToSystems);
    group.MapPut("/removeFromSystems", RemoveFromSystems);

    group.MapPut("/addToRecords", AddToRecords);
    group.MapPut("/removeFromRecords", RemoveFromRecords);

    group.MapPut("/addToPrivacyNotices", AddToPrivacyNotices);
    group.MapPut("/removeFromPrivacyNotices", RemoveFromPrivacyNotices);

    group.MapPut("/addToThirdParties", AddToThirdParties);
    group.MapPut("/removeFromThirdParties", RemoveFromThirdParties);

    group.MapPut("/addToConsents", AddToConsents);
    group.MapPut("/removeFromConsents", RemoveFromConsents);

    group.MapPut("/addToDataBreaches", AddToDataBreaches);
    group.MapPut("/removeFromDataBreaches", RemoveFromDataBreaches);

    group.MapPut("/addToDataSubjectRequests", AddToDataSubjectRequests);
    group.MapPut("/removeFromDataSubjectRequests", RemoveFromDataSubjectRequests);


        return app;
    }

    private static async Task<IResult> Create(
        DataProcessingActivityRequest request,
        IDataProcessingActivityService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToDataProcessingActivity( request );

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
        DataProcessingActivityRequest request,
        IDataProcessingActivityService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToDataProcessingActivity( request );

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
        IDataProcessingActivityService service,
        CancellationToken cancellationToken) {

        var dataProcessingActivity = await service.Get(identifier, cancellationToken);
        return dataProcessingActivity is null ? Results.NotFound() : Results.Ok( dataProcessingActivity );
    }


    private static async Task<IResult> GetAll(
        IDataProcessingActivityService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( DataProcessingActivityResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IDataProcessingActivityService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignOrganization(
        AssociationRequest request,
        IDataProcessingActivityService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignOrganization(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignOrganization(
    AssociationRequest request,
    IDataProcessingActivityService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignOrganization(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToDataCategories(
        MultipleAssociationRequest request,
        IDataProcessingActivityService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToDataCategories(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromDataCategories(
        MultipleAssociationRequest request,
        IDataProcessingActivityService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromDataCategories(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToSystems(
        MultipleAssociationRequest request,
        IDataProcessingActivityService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToSystems(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromSystems(
        MultipleAssociationRequest request,
        IDataProcessingActivityService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromSystems(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToRecords(
        MultipleAssociationRequest request,
        IDataProcessingActivityService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToRecords(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromRecords(
        MultipleAssociationRequest request,
        IDataProcessingActivityService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromRecords(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToPrivacyNotices(
        MultipleAssociationRequest request,
        IDataProcessingActivityService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToPrivacyNotices(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromPrivacyNotices(
        MultipleAssociationRequest request,
        IDataProcessingActivityService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromPrivacyNotices(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToThirdParties(
        MultipleAssociationRequest request,
        IDataProcessingActivityService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToThirdParties(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromThirdParties(
        MultipleAssociationRequest request,
        IDataProcessingActivityService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromThirdParties(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToConsents(
        MultipleAssociationRequest request,
        IDataProcessingActivityService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToConsents(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromConsents(
        MultipleAssociationRequest request,
        IDataProcessingActivityService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromConsents(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToDataBreaches(
        MultipleAssociationRequest request,
        IDataProcessingActivityService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToDataBreaches(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromDataBreaches(
        MultipleAssociationRequest request,
        IDataProcessingActivityService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromDataBreaches(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToDataSubjectRequests(
        MultipleAssociationRequest request,
        IDataProcessingActivityService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToDataSubjectRequests(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromDataSubjectRequests(
        MultipleAssociationRequest request,
        IDataProcessingActivityService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromDataSubjectRequests(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static DataProcessingActivity mapRequestToDataProcessingActivity( DataProcessingActivityRequest request ) {
        var model = new DataProcessingActivity
        {
            Id = request.Id,
            Name = request.Name,
            Purpose = request.Purpose,
            StartDate = request.StartDate,
            LawfulBasis = request.LawfulBasis,
        };
        return model;
    }

}
