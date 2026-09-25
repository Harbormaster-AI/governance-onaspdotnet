
using governanceonaspdotnet.Service;
using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Api;

public static class RiskEndpoints
{
    public static IEndpointRouteBuilder MapRiskEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/risk").WithTags("Risks");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignOrganization", AssignOrganization);
        group.MapPut("/unassignOrganization", UnassignOrganization);

    group.MapPut("/addToControls", AddToControls);
    group.MapPut("/removeFromControls", RemoveFromControls);

    group.MapPut("/addToAssessments", AddToAssessments);
    group.MapPut("/removeFromAssessments", RemoveFromAssessments);

    group.MapPut("/addToIssues", AddToIssues);
    group.MapPut("/removeFromIssues", RemoveFromIssues);

    group.MapPut("/addToFindings", AddToFindings);
    group.MapPut("/removeFromFindings", RemoveFromFindings);


        return app;
    }

    private static async Task<IResult> Create(
        RiskRequest request,
        IRiskService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToRisk( request );

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
        RiskRequest request,
        IRiskService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToRisk( request );

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
        IRiskService service,
        CancellationToken cancellationToken) {

        var risk = await service.Get(identifier, cancellationToken);
        return risk is null ? Results.NotFound() : Results.Ok( risk );
    }


    private static async Task<IResult> GetAll(
        IRiskService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( RiskResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IRiskService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignOrganization(
        AssociationRequest request,
        IRiskService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignOrganization(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignOrganization(
    AssociationRequest request,
    IRiskService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignOrganization(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToControls(
        MultipleAssociationRequest request,
        IRiskService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToControls(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromControls(
        MultipleAssociationRequest request,
        IRiskService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromControls(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToAssessments(
        MultipleAssociationRequest request,
        IRiskService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToAssessments(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromAssessments(
        MultipleAssociationRequest request,
        IRiskService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromAssessments(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToIssues(
        MultipleAssociationRequest request,
        IRiskService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToIssues(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromIssues(
        MultipleAssociationRequest request,
        IRiskService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromIssues(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToFindings(
        MultipleAssociationRequest request,
        IRiskService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToFindings(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromFindings(
        MultipleAssociationRequest request,
        IRiskService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromFindings(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Risk mapRequestToRisk( RiskRequest request ) {
        var model = new Risk
        {
            Id = request.Id,
            Name = request.Name,
            Description = request.Description,
            InherentRiskScore = request.InherentRiskScore,
            ResidualRiskScore = request.ResidualRiskScore,
            Category = request.Category,
            Impact = request.Impact,
            Likelihood = request.Likelihood,
            Status = request.Status,
        };
        return model;
    }

}
