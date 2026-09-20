using governanceonaspdotnet.Service;
using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Api;

public static class RoleEndpoints
{
    public static IEndpointRouteBuilder MapRoleEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/role").WithTags("Roles");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);


    group.MapPut("/addToAssignments", AddToAssignments);
    group.MapPut("/removeFromAssignments", RemoveFromAssignments);


        return app;
    }

    private static async Task<IResult> Create(
        RoleRequest request,
        IRoleService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToRole( request );

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
        RoleRequest request,
        IRoleService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToRole( request );

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
        IRoleService service,
        CancellationToken cancellationToken) {

        var role = await service.Get(identifier, cancellationToken);
        return role is null ? Results.NotFound() : Results.Ok( role );
    }


    private static async Task<IResult> GetAll(
        IRoleService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( RoleResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IRoleService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToAssignments(
        MultipleAssociationRequest request,
        IRoleService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToAssignments(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromAssignments(
        MultipleAssociationRequest request,
        IRoleService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromAssignments(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Role mapRequestToRole( RoleRequest request ) {
        var model = new Role
        {
            Id = request.Id,
            Name = request.Name,
            Responsibility = request.Responsibility,
        };
        return model;
    }

}
