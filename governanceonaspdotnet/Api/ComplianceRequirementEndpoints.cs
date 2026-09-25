
using governanceonaspdotnet.Service;
using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Api;

public static class ComplianceRequirementEndpoints
{
    public static IEndpointRouteBuilder MapComplianceRequirementEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/complianceRequirement").WithTags("ComplianceRequirements");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignComplianceProgram", AssignComplianceProgram);
        group.MapPut("/unassignComplianceProgram", UnassignComplianceProgram);

    group.MapPut("/addToPolicies", AddToPolicies);
    group.MapPut("/removeFromPolicies", RemoveFromPolicies);

    group.MapPut("/addToControls", AddToControls);
    group.MapPut("/removeFromControls", RemoveFromControls);

    group.MapPut("/addToObligations", AddToObligations);
    group.MapPut("/removeFromObligations", RemoveFromObligations);


        return app;
    }

    private static async Task<IResult> Create(
        ComplianceRequirementRequest request,
        IComplianceRequirementService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToComplianceRequirement( request );

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
        ComplianceRequirementRequest request,
        IComplianceRequirementService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToComplianceRequirement( request );

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
        IComplianceRequirementService service,
        CancellationToken cancellationToken) {

        var complianceRequirement = await service.Get(identifier, cancellationToken);
        return complianceRequirement is null ? Results.NotFound() : Results.Ok( complianceRequirement );
    }


    private static async Task<IResult> GetAll(
        IComplianceRequirementService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( ComplianceRequirementResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IComplianceRequirementService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignComplianceProgram(
        AssociationRequest request,
        IComplianceRequirementService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignComplianceProgram(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignComplianceProgram(
    AssociationRequest request,
    IComplianceRequirementService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignComplianceProgram(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToPolicies(
        MultipleAssociationRequest request,
        IComplianceRequirementService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToPolicies(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromPolicies(
        MultipleAssociationRequest request,
        IComplianceRequirementService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromPolicies(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToControls(
        MultipleAssociationRequest request,
        IComplianceRequirementService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToControls(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromControls(
        MultipleAssociationRequest request,
        IComplianceRequirementService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromControls(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToObligations(
        MultipleAssociationRequest request,
        IComplianceRequirementService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToObligations(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromObligations(
        MultipleAssociationRequest request,
        IComplianceRequirementService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromObligations(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static ComplianceRequirement mapRequestToComplianceRequirement( ComplianceRequirementRequest request ) {
        var model = new ComplianceRequirement
        {
            Id = request.Id,
            Name = request.Name,
            Source = request.Source,
            Citation = request.Citation,
            Applicability = request.Applicability,
            Status = request.Status,
        };
        return model;
    }

}
