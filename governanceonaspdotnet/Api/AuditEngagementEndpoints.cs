
using governanceonaspdotnet.Service;
using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Api;

public static class AuditEngagementEndpoints
{
    public static IEndpointRouteBuilder MapAuditEngagementEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/auditEngagement").WithTags("AuditEngagements");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignAuditProgram", AssignAuditProgram);
        group.MapPut("/unassignAuditProgram", UnassignAuditProgram);

    group.MapPut("/addToBusinessUnits", AddToBusinessUnits);
    group.MapPut("/removeFromBusinessUnits", RemoveFromBusinessUnits);

    group.MapPut("/addToControlTests", AddToControlTests);
    group.MapPut("/removeFromControlTests", RemoveFromControlTests);

    group.MapPut("/addToWorkpapers", AddToWorkpapers);
    group.MapPut("/removeFromWorkpapers", RemoveFromWorkpapers);

    group.MapPut("/addToFindings", AddToFindings);
    group.MapPut("/removeFromFindings", RemoveFromFindings);


        return app;
    }

    private static async Task<IResult> Create(
        AuditEngagementRequest request,
        IAuditEngagementService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToAuditEngagement( request );

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
        AuditEngagementRequest request,
        IAuditEngagementService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToAuditEngagement( request );

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
        IAuditEngagementService service,
        CancellationToken cancellationToken) {

        var auditEngagement = await service.Get(identifier, cancellationToken);
        return auditEngagement is null ? Results.NotFound() : Results.Ok( auditEngagement );
    }


    private static async Task<IResult> GetAll(
        IAuditEngagementService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( AuditEngagementResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IAuditEngagementService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignAuditProgram(
        AssociationRequest request,
        IAuditEngagementService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignAuditProgram(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignAuditProgram(
    AssociationRequest request,
    IAuditEngagementService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignAuditProgram(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToBusinessUnits(
        MultipleAssociationRequest request,
        IAuditEngagementService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToBusinessUnits(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromBusinessUnits(
        MultipleAssociationRequest request,
        IAuditEngagementService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromBusinessUnits(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToControlTests(
        MultipleAssociationRequest request,
        IAuditEngagementService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToControlTests(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromControlTests(
        MultipleAssociationRequest request,
        IAuditEngagementService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromControlTests(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToWorkpapers(
        MultipleAssociationRequest request,
        IAuditEngagementService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToWorkpapers(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromWorkpapers(
        MultipleAssociationRequest request,
        IAuditEngagementService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromWorkpapers(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToFindings(
        MultipleAssociationRequest request,
        IAuditEngagementService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToFindings(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromFindings(
        MultipleAssociationRequest request,
        IAuditEngagementService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromFindings(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static AuditEngagement mapRequestToAuditEngagement( AuditEngagementRequest request ) {
        var model = new AuditEngagement
        {
            Id = request.Id,
            Title = request.Title,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Status = request.Status,
        };
        return model;
    }

}
