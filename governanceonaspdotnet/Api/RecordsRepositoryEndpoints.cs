using governanceonaspdotnet.Service;
using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Api;

public static class RecordsRepositoryEndpoints
{
    public static IEndpointRouteBuilder MapRecordsRepositoryEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/recordsRepository").WithTags("RecordsRepositorys");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignOrganization", AssignOrganization);
        group.MapPut("/unassignOrganization", UnassignOrganization);

    group.MapPut("/addToRecords", AddToRecords);
    group.MapPut("/removeFromRecords", RemoveFromRecords);

    group.MapPut("/addToSystems", AddToSystems);
    group.MapPut("/removeFromSystems", RemoveFromSystems);

    group.MapPut("/addToRetentionSchedules", AddToRetentionSchedules);
    group.MapPut("/removeFromRetentionSchedules", RemoveFromRetentionSchedules);

    group.MapPut("/addToLegalHolds", AddToLegalHolds);
    group.MapPut("/removeFromLegalHolds", RemoveFromLegalHolds);


        return app;
    }

    private static async Task<IResult> Create(
        RecordsRepositoryRequest request,
        IRecordsRepositoryService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToRecordsRepository( request );

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
        RecordsRepositoryRequest request,
        IRecordsRepositoryService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToRecordsRepository( request );

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
        IRecordsRepositoryService service,
        CancellationToken cancellationToken) {

        var recordsRepository = await service.Get(identifier, cancellationToken);
        return recordsRepository is null ? Results.NotFound() : Results.Ok( recordsRepository );
    }


    private static async Task<IResult> GetAll(
        IRecordsRepositoryService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( RecordsRepositoryResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IRecordsRepositoryService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignOrganization(
        AssociationRequest request,
        IRecordsRepositoryService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignOrganization(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignOrganization(
    AssociationRequest request,
    IRecordsRepositoryService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignOrganization(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToRecords(
        MultipleAssociationRequest request,
        IRecordsRepositoryService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToRecords(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromRecords(
        MultipleAssociationRequest request,
        IRecordsRepositoryService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromRecords(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToSystems(
        MultipleAssociationRequest request,
        IRecordsRepositoryService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToSystems(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromSystems(
        MultipleAssociationRequest request,
        IRecordsRepositoryService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromSystems(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToRetentionSchedules(
        MultipleAssociationRequest request,
        IRecordsRepositoryService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToRetentionSchedules(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromRetentionSchedules(
        MultipleAssociationRequest request,
        IRecordsRepositoryService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromRetentionSchedules(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToLegalHolds(
        MultipleAssociationRequest request,
        IRecordsRepositoryService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToLegalHolds(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromLegalHolds(
        MultipleAssociationRequest request,
        IRecordsRepositoryService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromLegalHolds(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static RecordsRepository mapRequestToRecordsRepository( RecordsRepositoryRequest request ) {
        var model = new RecordsRepository
        {
            Id = request.Id,
            Name = request.Name,
            Location = request.Location,
            OwnerDepartment = request.OwnerDepartment,
            RepositoryType = request.RepositoryType,
        };
        return model;
    }

}
