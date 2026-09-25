
using governanceonaspdotnet.Service;
using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Api;

public static class Record_Endpoints
{
    public static IEndpointRouteBuilder MapRecord_Endpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/record_").WithTags("Record_s");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignRepository", AssignRepository);
        group.MapPut("/unassignRepository", UnassignRepository);
        group.MapPut("/assignRetentionSchedule", AssignRetentionSchedule);
        group.MapPut("/unassignRetentionSchedule", UnassignRetentionSchedule);

    group.MapPut("/addToProcessingActivities", AddToProcessingActivities);
    group.MapPut("/removeFromProcessingActivities", RemoveFromProcessingActivities);

    group.MapPut("/addToDataCategories", AddToDataCategories);
    group.MapPut("/removeFromDataCategories", RemoveFromDataCategories);

    group.MapPut("/addToLegalHolds", AddToLegalHolds);
    group.MapPut("/removeFromLegalHolds", RemoveFromLegalHolds);

    group.MapPut("/addToDataSubjectRequests", AddToDataSubjectRequests);
    group.MapPut("/removeFromDataSubjectRequests", RemoveFromDataSubjectRequests);


        return app;
    }

    private static async Task<IResult> Create(
        Record_Request request,
        IRecord_Service service,
        CancellationToken cancellationToken) {

        var model = mapRequestToRecord_( request );

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
        Record_Request request,
        IRecord_Service service,
        CancellationToken cancellationToken) {

        var model = mapRequestToRecord_( request );

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
        IRecord_Service service,
        CancellationToken cancellationToken) {

        var record_ = await service.Get(identifier, cancellationToken);
        return record_ is null ? Results.NotFound() : Results.Ok( record_ );
    }


    private static async Task<IResult> GetAll(
        IRecord_Service service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( Record_Response.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IRecord_Service service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignRepository(
        AssociationRequest request,
        IRecord_Service service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignRepository(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignRepository(
    AssociationRequest request,
    IRecord_Service service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignRepository(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignRetentionSchedule(
        AssociationRequest request,
        IRecord_Service service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignRetentionSchedule(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignRetentionSchedule(
    AssociationRequest request,
    IRecord_Service service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignRetentionSchedule(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToProcessingActivities(
        MultipleAssociationRequest request,
        IRecord_Service service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToProcessingActivities(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromProcessingActivities(
        MultipleAssociationRequest request,
        IRecord_Service service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromProcessingActivities(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToDataCategories(
        MultipleAssociationRequest request,
        IRecord_Service service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToDataCategories(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromDataCategories(
        MultipleAssociationRequest request,
        IRecord_Service service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromDataCategories(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToLegalHolds(
        MultipleAssociationRequest request,
        IRecord_Service service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToLegalHolds(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromLegalHolds(
        MultipleAssociationRequest request,
        IRecord_Service service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromLegalHolds(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToDataSubjectRequests(
        MultipleAssociationRequest request,
        IRecord_Service service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToDataSubjectRequests(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromDataSubjectRequests(
        MultipleAssociationRequest request,
        IRecord_Service service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromDataSubjectRequests(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Record_ mapRequestToRecord_( Record_Request request ) {
        var model = new Record_
        {
            Id = request.Id,
            Title = request.Title,
            CreationDate = request.CreationDate,
            RecordType = request.RecordType,
            Classification = request.Classification,
            Status = request.Status,
        };
        return model;
    }

}
