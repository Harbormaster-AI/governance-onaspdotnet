using governanceonaspdotnet.Service;
using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Api;

public static class AuditFindingEndpoints
{
    public static IEndpointRouteBuilder MapAuditFindingEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/auditFinding").WithTags("AuditFindings");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignEngagement", AssignEngagement);
        group.MapPut("/unassignEngagement", UnassignEngagement);
        group.MapPut("/assignWorkpaper", AssignWorkpaper);
        group.MapPut("/unassignWorkpaper", UnassignWorkpaper);

    group.MapPut("/addToCorrectiveActions", AddToCorrectiveActions);
    group.MapPut("/removeFromCorrectiveActions", RemoveFromCorrectiveActions);

    group.MapPut("/addToRelatedRisks", AddToRelatedRisks);
    group.MapPut("/removeFromRelatedRisks", RemoveFromRelatedRisks);

    group.MapPut("/addToRelatedControls", AddToRelatedControls);
    group.MapPut("/removeFromRelatedControls", RemoveFromRelatedControls);

    group.MapPut("/addToIssues", AddToIssues);
    group.MapPut("/removeFromIssues", RemoveFromIssues);


        return app;
    }

    private static async Task<IResult> Create(
        AuditFindingRequest request,
        IAuditFindingService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToAuditFinding( request );

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
        AuditFindingRequest request,
        IAuditFindingService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToAuditFinding( request );

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
        IAuditFindingService service,
        CancellationToken cancellationToken) {

        var auditFinding = await service.Get(identifier, cancellationToken);
        return auditFinding is null ? Results.NotFound() : Results.Ok( auditFinding );
    }


    private static async Task<IResult> GetAll(
        IAuditFindingService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( AuditFindingResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IAuditFindingService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignEngagement(
        AssociationRequest request,
        IAuditFindingService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignEngagement(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignEngagement(
    AssociationRequest request,
    IAuditFindingService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignEngagement(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignWorkpaper(
        AssociationRequest request,
        IAuditFindingService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignWorkpaper(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignWorkpaper(
    AssociationRequest request,
    IAuditFindingService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignWorkpaper(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToCorrectiveActions(
        MultipleAssociationRequest request,
        IAuditFindingService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToCorrectiveActions(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromCorrectiveActions(
        MultipleAssociationRequest request,
        IAuditFindingService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromCorrectiveActions(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToRelatedRisks(
        MultipleAssociationRequest request,
        IAuditFindingService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToRelatedRisks(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromRelatedRisks(
        MultipleAssociationRequest request,
        IAuditFindingService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromRelatedRisks(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToRelatedControls(
        MultipleAssociationRequest request,
        IAuditFindingService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToRelatedControls(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromRelatedControls(
        MultipleAssociationRequest request,
        IAuditFindingService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromRelatedControls(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToIssues(
        MultipleAssociationRequest request,
        IAuditFindingService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToIssues(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromIssues(
        MultipleAssociationRequest request,
        IAuditFindingService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromIssues(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static AuditFinding mapRequestToAuditFinding( AuditFindingRequest request ) {
        var model = new AuditFinding
        {
            Id = request.Id,
            Title = request.Title,
            Description = request.Description,
            DueDate = request.DueDate,
            Severity = request.Severity,
            Status = request.Status,
        };
        return model;
    }

}
