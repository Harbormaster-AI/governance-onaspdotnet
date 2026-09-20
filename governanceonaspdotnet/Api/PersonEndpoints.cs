using governanceonaspdotnet.Service;
using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Api;

public static class PersonEndpoints
{
    public static IEndpointRouteBuilder MapPersonEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/person").WithTags("Persons");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);


    group.MapPut("/addToRoleAssignments", AddToRoleAssignments);
    group.MapPut("/removeFromRoleAssignments", RemoveFromRoleAssignments);

    group.MapPut("/addToOwnedPolicies", AddToOwnedPolicies);
    group.MapPut("/removeFromOwnedPolicies", RemoveFromOwnedPolicies);

    group.MapPut("/addToCorrectiveActions", AddToCorrectiveActions);
    group.MapPut("/removeFromCorrectiveActions", RemoveFromCorrectiveActions);


        return app;
    }

    private static async Task<IResult> Create(
        PersonRequest request,
        IPersonService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToPerson( request );

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
        PersonRequest request,
        IPersonService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToPerson( request );

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
        IPersonService service,
        CancellationToken cancellationToken) {

        var person = await service.Get(identifier, cancellationToken);
        return person is null ? Results.NotFound() : Results.Ok( person );
    }


    private static async Task<IResult> GetAll(
        IPersonService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( PersonResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IPersonService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToRoleAssignments(
        MultipleAssociationRequest request,
        IPersonService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToRoleAssignments(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromRoleAssignments(
        MultipleAssociationRequest request,
        IPersonService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromRoleAssignments(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToOwnedPolicies(
        MultipleAssociationRequest request,
        IPersonService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToOwnedPolicies(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromOwnedPolicies(
        MultipleAssociationRequest request,
        IPersonService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromOwnedPolicies(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToCorrectiveActions(
        MultipleAssociationRequest request,
        IPersonService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToCorrectiveActions(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromCorrectiveActions(
        MultipleAssociationRequest request,
        IPersonService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromCorrectiveActions(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Person mapRequestToPerson( PersonRequest request ) {
        var model = new Person
        {
            Id = request.Id,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Department = request.Department,
        };
        return model;
    }

}
