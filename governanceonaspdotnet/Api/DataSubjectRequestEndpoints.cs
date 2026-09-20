using governanceonaspdotnet.Service;
using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Api;

public static class DataSubjectRequestEndpoints
{
    public static IEndpointRouteBuilder MapDataSubjectRequestEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/dataSubjectRequest").WithTags("DataSubjectRequests");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignOrganization", AssignOrganization);
        group.MapPut("/unassignOrganization", UnassignOrganization);

    group.MapPut("/addToProcessingActivities", AddToProcessingActivities);
    group.MapPut("/removeFromProcessingActivities", RemoveFromProcessingActivities);

    group.MapPut("/addToRecords", AddToRecords);
    group.MapPut("/removeFromRecords", RemoveFromRecords);


        return app;
    }

    private static async Task<IResult> Create(
        DataSubjectRequestRequest request,
        IDataSubjectRequestService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToDataSubjectRequest( request );

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
        DataSubjectRequestRequest request,
        IDataSubjectRequestService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToDataSubjectRequest( request );

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
        IDataSubjectRequestService service,
        CancellationToken cancellationToken) {

        var dataSubjectRequest = await service.Get(identifier, cancellationToken);
        return dataSubjectRequest is null ? Results.NotFound() : Results.Ok( dataSubjectRequest );
    }


    private static async Task<IResult> GetAll(
        IDataSubjectRequestService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( DataSubjectRequestResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IDataSubjectRequestService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignOrganization(
        AssociationRequest request,
        IDataSubjectRequestService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignOrganization(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignOrganization(
    AssociationRequest request,
    IDataSubjectRequestService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignOrganization(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToProcessingActivities(
        MultipleAssociationRequest request,
        IDataSubjectRequestService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToProcessingActivities(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromProcessingActivities(
        MultipleAssociationRequest request,
        IDataSubjectRequestService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromProcessingActivities(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToRecords(
        MultipleAssociationRequest request,
        IDataSubjectRequestService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToRecords(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromRecords(
        MultipleAssociationRequest request,
        IDataSubjectRequestService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromRecords(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static DataSubjectRequest mapRequestToDataSubjectRequest( DataSubjectRequestRequest request ) {
        var model = new DataSubjectRequest
        {
            Id = request.Id,
            ReceivedDate = request.ReceivedDate,
            DueDate = request.DueDate,
            RequesterCountry = request.RequesterCountry,
            RequestType = request.RequestType,
            Status = request.Status,
        };
        return model;
    }

}
