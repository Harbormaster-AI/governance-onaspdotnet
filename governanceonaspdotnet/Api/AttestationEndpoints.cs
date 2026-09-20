using governanceonaspdotnet.Service;
using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Api;

public static class AttestationEndpoints
{
    public static IEndpointRouteBuilder MapAttestationEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/attestation").WithTags("Attestations");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignControl", AssignControl);
        group.MapPut("/unassignControl", UnassignControl);
        group.MapPut("/assignPolicy", AssignPolicy);
        group.MapPut("/unassignPolicy", UnassignPolicy);
        group.MapPut("/assignComplianceProgram", AssignComplianceProgram);
        group.MapPut("/unassignComplianceProgram", UnassignComplianceProgram);


        return app;
    }

    private static async Task<IResult> Create(
        AttestationRequest request,
        IAttestationService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToAttestation( request );

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
        AttestationRequest request,
        IAttestationService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToAttestation( request );

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
        IAttestationService service,
        CancellationToken cancellationToken) {

        var attestation = await service.Get(identifier, cancellationToken);
        return attestation is null ? Results.NotFound() : Results.Ok( attestation );
    }


    private static async Task<IResult> GetAll(
        IAttestationService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( AttestationResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IAttestationService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignControl(
        AssociationRequest request,
        IAttestationService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignControl(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignControl(
    AssociationRequest request,
    IAttestationService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignControl(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignPolicy(
        AssociationRequest request,
        IAttestationService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignPolicy(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignPolicy(
    AssociationRequest request,
    IAttestationService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignPolicy(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignComplianceProgram(
        AssociationRequest request,
        IAttestationService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignComplianceProgram(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignComplianceProgram(
    AssociationRequest request,
    IAttestationService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignComplianceProgram(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static Attestation mapRequestToAttestation( AttestationRequest request ) {
        var model = new Attestation
        {
            Id = request.Id,
            Statement = request.Statement,
            Attestor = request.Attestor,
            DateSigned = request.DateSigned,
            Result = request.Result,
        };
        return model;
    }

}
