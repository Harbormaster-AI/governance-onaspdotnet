
using governanceonaspdotnet.Service;
using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Api;

public static class ComplianceProgramEndpoints
{
    public static IEndpointRouteBuilder MapComplianceProgramEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/complianceProgram").WithTags("CompliancePrograms");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignOrganization", AssignOrganization);
        group.MapPut("/unassignOrganization", UnassignOrganization);

    group.MapPut("/addToRequirements", AddToRequirements);
    group.MapPut("/removeFromRequirements", RemoveFromRequirements);

    group.MapPut("/addToControls", AddToControls);
    group.MapPut("/removeFromControls", RemoveFromControls);

    group.MapPut("/addToAttestations", AddToAttestations);
    group.MapPut("/removeFromAttestations", RemoveFromAttestations);

    group.MapPut("/addToRegulations", AddToRegulations);
    group.MapPut("/removeFromRegulations", RemoveFromRegulations);


        return app;
    }

    private static async Task<IResult> Create(
        ComplianceProgramRequest request,
        IComplianceProgramService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToComplianceProgram( request );

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
        ComplianceProgramRequest request,
        IComplianceProgramService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToComplianceProgram( request );

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
        IComplianceProgramService service,
        CancellationToken cancellationToken) {

        var complianceProgram = await service.Get(identifier, cancellationToken);
        return complianceProgram is null ? Results.NotFound() : Results.Ok( complianceProgram );
    }


    private static async Task<IResult> GetAll(
        IComplianceProgramService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( ComplianceProgramResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IComplianceProgramService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignOrganization(
        AssociationRequest request,
        IComplianceProgramService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignOrganization(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignOrganization(
    AssociationRequest request,
    IComplianceProgramService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignOrganization(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToRequirements(
        MultipleAssociationRequest request,
        IComplianceProgramService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToRequirements(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromRequirements(
        MultipleAssociationRequest request,
        IComplianceProgramService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromRequirements(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToControls(
        MultipleAssociationRequest request,
        IComplianceProgramService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToControls(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromControls(
        MultipleAssociationRequest request,
        IComplianceProgramService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromControls(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToAttestations(
        MultipleAssociationRequest request,
        IComplianceProgramService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToAttestations(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromAttestations(
        MultipleAssociationRequest request,
        IComplianceProgramService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromAttestations(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToRegulations(
        MultipleAssociationRequest request,
        IComplianceProgramService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToRegulations(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromRegulations(
        MultipleAssociationRequest request,
        IComplianceProgramService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromRegulations(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static ComplianceProgram mapRequestToComplianceProgram( ComplianceProgramRequest request ) {
        var model = new ComplianceProgram
        {
            Id = request.Id,
            Name = request.Name,
            Framework = request.Framework,
            Status = request.Status,
        };
        return model;
    }

}
