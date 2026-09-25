
using governanceonaspdotnet.Service;
using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Api;

public static class GovernanceBodyEndpoints
{
    public static IEndpointRouteBuilder MapGovernanceBodyEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/governanceBody").WithTags("GovernanceBodys");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignOrganization", AssignOrganization);
        group.MapPut("/unassignOrganization", UnassignOrganization);

    group.MapPut("/addToRoleAssignments", AddToRoleAssignments);
    group.MapPut("/removeFromRoleAssignments", RemoveFromRoleAssignments);

    group.MapPut("/addToPolicies", AddToPolicies);
    group.MapPut("/removeFromPolicies", RemoveFromPolicies);


        return app;
    }

    private static async Task<IResult> Create(
        GovernanceBodyRequest request,
        IGovernanceBodyService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToGovernanceBody( request );

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
        GovernanceBodyRequest request,
        IGovernanceBodyService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToGovernanceBody( request );

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
        IGovernanceBodyService service,
        CancellationToken cancellationToken) {

        var governanceBody = await service.Get(identifier, cancellationToken);
        return governanceBody is null ? Results.NotFound() : Results.Ok( governanceBody );
    }


    private static async Task<IResult> GetAll(
        IGovernanceBodyService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( GovernanceBodyResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IGovernanceBodyService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignOrganization(
        AssociationRequest request,
        IGovernanceBodyService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignOrganization(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignOrganization(
    AssociationRequest request,
    IGovernanceBodyService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignOrganization(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToRoleAssignments(
        MultipleAssociationRequest request,
        IGovernanceBodyService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToRoleAssignments(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromRoleAssignments(
        MultipleAssociationRequest request,
        IGovernanceBodyService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromRoleAssignments(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToPolicies(
        MultipleAssociationRequest request,
        IGovernanceBodyService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToPolicies(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromPolicies(
        MultipleAssociationRequest request,
        IGovernanceBodyService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromPolicies(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static GovernanceBody mapRequestToGovernanceBody( GovernanceBodyRequest request ) {
        var model = new GovernanceBody
        {
            Id = request.Id,
            Name = request.Name,
            CharterUrl = request.CharterUrl,
            Chair = request.Chair,
            BodyType = request.BodyType,
        };
        return model;
    }

}
