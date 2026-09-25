
using governanceonaspdotnet.Service;
using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Api;

public static class RoleAssignmentEndpoints
{
    public static IEndpointRouteBuilder MapRoleAssignmentEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/roleAssignment").WithTags("RoleAssignments");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignPerson", AssignPerson);
        group.MapPut("/unassignPerson", UnassignPerson);
        group.MapPut("/assignRole", AssignRole);
        group.MapPut("/unassignRole", UnassignRole);
        group.MapPut("/assignGovernanceBody", AssignGovernanceBody);
        group.MapPut("/unassignGovernanceBody", UnassignGovernanceBody);
        group.MapPut("/assignOrganization", AssignOrganization);
        group.MapPut("/unassignOrganization", UnassignOrganization);


        return app;
    }

    private static async Task<IResult> Create(
        RoleAssignmentRequest request,
        IRoleAssignmentService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToRoleAssignment( request );

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
        RoleAssignmentRequest request,
        IRoleAssignmentService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToRoleAssignment( request );

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
        IRoleAssignmentService service,
        CancellationToken cancellationToken) {

        var roleAssignment = await service.Get(identifier, cancellationToken);
        return roleAssignment is null ? Results.NotFound() : Results.Ok( roleAssignment );
    }


    private static async Task<IResult> GetAll(
        IRoleAssignmentService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( RoleAssignmentResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IRoleAssignmentService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignPerson(
        AssociationRequest request,
        IRoleAssignmentService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignPerson(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignPerson(
    AssociationRequest request,
    IRoleAssignmentService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignPerson(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignRole(
        AssociationRequest request,
        IRoleAssignmentService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignRole(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignRole(
    AssociationRequest request,
    IRoleAssignmentService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignRole(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignGovernanceBody(
        AssociationRequest request,
        IRoleAssignmentService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignGovernanceBody(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignGovernanceBody(
    AssociationRequest request,
    IRoleAssignmentService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignGovernanceBody(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignOrganization(
        AssociationRequest request,
        IRoleAssignmentService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignOrganization(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignOrganization(
    AssociationRequest request,
    IRoleAssignmentService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignOrganization(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static RoleAssignment mapRequestToRoleAssignment( RoleAssignmentRequest request ) {
        var model = new RoleAssignment
        {
            Id = request.Id,
            EffectiveFrom = request.EffectiveFrom,
            EffectiveTo = request.EffectiveTo,
        };
        return model;
    }

}
