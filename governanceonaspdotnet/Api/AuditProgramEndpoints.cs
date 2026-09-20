using governanceonaspdotnet.Service;
using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Api;

public static class AuditProgramEndpoints
{
    public static IEndpointRouteBuilder MapAuditProgramEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/auditProgram").WithTags("AuditPrograms");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignOrganization", AssignOrganization);
        group.MapPut("/unassignOrganization", UnassignOrganization);

    group.MapPut("/addToEngagements", AddToEngagements);
    group.MapPut("/removeFromEngagements", RemoveFromEngagements);


        return app;
    }

    private static async Task<IResult> Create(
        AuditProgramRequest request,
        IAuditProgramService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToAuditProgram( request );

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
        AuditProgramRequest request,
        IAuditProgramService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToAuditProgram( request );

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
        IAuditProgramService service,
        CancellationToken cancellationToken) {

        var auditProgram = await service.Get(identifier, cancellationToken);
        return auditProgram is null ? Results.NotFound() : Results.Ok( auditProgram );
    }


    private static async Task<IResult> GetAll(
        IAuditProgramService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( AuditProgramResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IAuditProgramService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignOrganization(
        AssociationRequest request,
        IAuditProgramService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignOrganization(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignOrganization(
    AssociationRequest request,
    IAuditProgramService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignOrganization(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToEngagements(
        MultipleAssociationRequest request,
        IAuditProgramService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToEngagements(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromEngagements(
        MultipleAssociationRequest request,
        IAuditProgramService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromEngagements(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static AuditProgram mapRequestToAuditProgram( AuditProgramRequest request ) {
        var model = new AuditProgram
        {
            Id = request.Id,
            Name = request.Name,
            Scope = request.Scope,
            Cycle = request.Cycle,
            Status = request.Status,
        };
        return model;
    }

}
