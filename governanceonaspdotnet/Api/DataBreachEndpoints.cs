using governanceonaspdotnet.Service;
using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Api;

public static class DataBreachEndpoints
{
    public static IEndpointRouteBuilder MapDataBreachEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/dataBreach").WithTags("DataBreachs");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignOrganization", AssignOrganization);
        group.MapPut("/unassignOrganization", UnassignOrganization);
        group.MapPut("/assignMatter", AssignMatter);
        group.MapPut("/unassignMatter", UnassignMatter);

    group.MapPut("/addToProcessingActivities", AddToProcessingActivities);
    group.MapPut("/removeFromProcessingActivities", RemoveFromProcessingActivities);

    group.MapPut("/addToDataCategories", AddToDataCategories);
    group.MapPut("/removeFromDataCategories", RemoveFromDataCategories);

    group.MapPut("/addToThirdParties", AddToThirdParties);
    group.MapPut("/removeFromThirdParties", RemoveFromThirdParties);


        return app;
    }

    private static async Task<IResult> Create(
        DataBreachRequest request,
        IDataBreachService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToDataBreach( request );

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
        DataBreachRequest request,
        IDataBreachService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToDataBreach( request );

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
        IDataBreachService service,
        CancellationToken cancellationToken) {

        var dataBreach = await service.Get(identifier, cancellationToken);
        return dataBreach is null ? Results.NotFound() : Results.Ok( dataBreach );
    }


    private static async Task<IResult> GetAll(
        IDataBreachService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( DataBreachResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IDataBreachService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignOrganization(
        AssociationRequest request,
        IDataBreachService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignOrganization(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignOrganization(
    AssociationRequest request,
    IDataBreachService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignOrganization(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignMatter(
        AssociationRequest request,
        IDataBreachService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignMatter(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignMatter(
    AssociationRequest request,
    IDataBreachService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignMatter(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToProcessingActivities(
        MultipleAssociationRequest request,
        IDataBreachService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToProcessingActivities(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromProcessingActivities(
        MultipleAssociationRequest request,
        IDataBreachService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromProcessingActivities(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToDataCategories(
        MultipleAssociationRequest request,
        IDataBreachService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToDataCategories(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromDataCategories(
        MultipleAssociationRequest request,
        IDataBreachService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromDataCategories(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToThirdParties(
        MultipleAssociationRequest request,
        IDataBreachService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToThirdParties(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromThirdParties(
        MultipleAssociationRequest request,
        IDataBreachService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromThirdParties(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static DataBreach mapRequestToDataBreach( DataBreachRequest request ) {
        var model = new DataBreach
        {
            Id = request.Id,
            IncidentDate = request.IncidentDate,
            Description = request.Description,
            RecordsAffected = request.RecordsAffected,
            NotificationRequired = request.NotificationRequired,
            Severity = request.Severity,
            Status = request.Status,
        };
        return model;
    }

}
