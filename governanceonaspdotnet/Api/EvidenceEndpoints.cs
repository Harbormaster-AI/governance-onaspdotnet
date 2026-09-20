using governanceonaspdotnet.Service;
using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Api;

public static class EvidenceEndpoints
{
    public static IEndpointRouteBuilder MapEvidenceEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/evidence").WithTags("Evidences");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignControlTest", AssignControlTest);
        group.MapPut("/unassignControlTest", UnassignControlTest);
        group.MapPut("/assignControl", AssignControl);
        group.MapPut("/unassignControl", UnassignControl);
        group.MapPut("/assignObligation", AssignObligation);
        group.MapPut("/unassignObligation", UnassignObligation);
        group.MapPut("/assignWorkpaper", AssignWorkpaper);
        group.MapPut("/unassignWorkpaper", UnassignWorkpaper);


        return app;
    }

    private static async Task<IResult> Create(
        EvidenceRequest request,
        IEvidenceService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToEvidence( request );

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
        EvidenceRequest request,
        IEvidenceService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToEvidence( request );

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
        IEvidenceService service,
        CancellationToken cancellationToken) {

        var evidence = await service.Get(identifier, cancellationToken);
        return evidence is null ? Results.NotFound() : Results.Ok( evidence );
    }


    private static async Task<IResult> GetAll(
        IEvidenceService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( EvidenceResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IEvidenceService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignControlTest(
        AssociationRequest request,
        IEvidenceService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignControlTest(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignControlTest(
    AssociationRequest request,
    IEvidenceService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignControlTest(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignControl(
        AssociationRequest request,
        IEvidenceService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignControl(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignControl(
    AssociationRequest request,
    IEvidenceService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignControl(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignObligation(
        AssociationRequest request,
        IEvidenceService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignObligation(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignObligation(
    AssociationRequest request,
    IEvidenceService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignObligation(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignWorkpaper(
        AssociationRequest request,
        IEvidenceService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignWorkpaper(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignWorkpaper(
    AssociationRequest request,
    IEvidenceService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignWorkpaper(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static Evidence mapRequestToEvidence( EvidenceRequest request ) {
        var model = new Evidence
        {
            Id = request.Id,
            Title = request.Title,
            LocationUrl = request.LocationUrl,
            ReceivedDate = request.ReceivedDate,
            EvidenceType = request.EvidenceType,
        };
        return model;
    }

}
