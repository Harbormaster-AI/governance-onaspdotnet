using governanceonaspdotnet.Service;
using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Api;

public static class AuditWorkpaperEndpoints
{
    public static IEndpointRouteBuilder MapAuditWorkpaperEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/auditWorkpaper").WithTags("AuditWorkpapers");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignEngagement", AssignEngagement);
        group.MapPut("/unassignEngagement", UnassignEngagement);

    group.MapPut("/addToEvidence", AddToEvidence);
    group.MapPut("/removeFromEvidence", RemoveFromEvidence);

    group.MapPut("/addToFindings", AddToFindings);
    group.MapPut("/removeFromFindings", RemoveFromFindings);


        return app;
    }

    private static async Task<IResult> Create(
        AuditWorkpaperRequest request,
        IAuditWorkpaperService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToAuditWorkpaper( request );

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
        AuditWorkpaperRequest request,
        IAuditWorkpaperService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToAuditWorkpaper( request );

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
        IAuditWorkpaperService service,
        CancellationToken cancellationToken) {

        var auditWorkpaper = await service.Get(identifier, cancellationToken);
        return auditWorkpaper is null ? Results.NotFound() : Results.Ok( auditWorkpaper );
    }


    private static async Task<IResult> GetAll(
        IAuditWorkpaperService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( AuditWorkpaperResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IAuditWorkpaperService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignEngagement(
        AssociationRequest request,
        IAuditWorkpaperService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignEngagement(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignEngagement(
    AssociationRequest request,
    IAuditWorkpaperService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignEngagement(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToEvidence(
        MultipleAssociationRequest request,
        IAuditWorkpaperService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToEvidence(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromEvidence(
        MultipleAssociationRequest request,
        IAuditWorkpaperService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromEvidence(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToFindings(
        MultipleAssociationRequest request,
        IAuditWorkpaperService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToFindings(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromFindings(
        MultipleAssociationRequest request,
        IAuditWorkpaperService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromFindings(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static AuditWorkpaper mapRequestToAuditWorkpaper( AuditWorkpaperRequest request ) {
        var model = new AuditWorkpaper
        {
            Id = request.Id,
            WorkpaperRef = request.WorkpaperRef,
            Subject = request.Subject,
            WorkpaperUrl = request.WorkpaperUrl,
        };
        return model;
    }

}
