using governanceonaspdotnet.Service;
using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Api;

public static class ControlEndpoints
{
    public static IEndpointRouteBuilder MapControlEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/control").WithTags("Controls");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignPolicy", AssignPolicy);
        group.MapPut("/unassignPolicy", UnassignPolicy);

    group.MapPut("/addToControlTests", AddToControlTests);
    group.MapPut("/removeFromControlTests", RemoveFromControlTests);

    group.MapPut("/addToEvidence", AddToEvidence);
    group.MapPut("/removeFromEvidence", RemoveFromEvidence);

    group.MapPut("/addToRisks", AddToRisks);
    group.MapPut("/removeFromRisks", RemoveFromRisks);

    group.MapPut("/addToObligations", AddToObligations);
    group.MapPut("/removeFromObligations", RemoveFromObligations);

    group.MapPut("/addToProcedures", AddToProcedures);
    group.MapPut("/removeFromProcedures", RemoveFromProcedures);

    group.MapPut("/addToIssues", AddToIssues);
    group.MapPut("/removeFromIssues", RemoveFromIssues);


        return app;
    }

    private static async Task<IResult> Create(
        ControlRequest request,
        IControlService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToControl( request );

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
        ControlRequest request,
        IControlService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToControl( request );

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
        IControlService service,
        CancellationToken cancellationToken) {

        var control = await service.Get(identifier, cancellationToken);
        return control is null ? Results.NotFound() : Results.Ok( control );
    }


    private static async Task<IResult> GetAll(
        IControlService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( ControlResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IControlService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignPolicy(
        AssociationRequest request,
        IControlService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignPolicy(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignPolicy(
    AssociationRequest request,
    IControlService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignPolicy(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToControlTests(
        MultipleAssociationRequest request,
        IControlService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToControlTests(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromControlTests(
        MultipleAssociationRequest request,
        IControlService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromControlTests(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToEvidence(
        MultipleAssociationRequest request,
        IControlService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToEvidence(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromEvidence(
        MultipleAssociationRequest request,
        IControlService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromEvidence(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToRisks(
        MultipleAssociationRequest request,
        IControlService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToRisks(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromRisks(
        MultipleAssociationRequest request,
        IControlService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromRisks(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToObligations(
        MultipleAssociationRequest request,
        IControlService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToObligations(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromObligations(
        MultipleAssociationRequest request,
        IControlService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromObligations(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToProcedures(
        MultipleAssociationRequest request,
        IControlService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToProcedures(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromProcedures(
        MultipleAssociationRequest request,
        IControlService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromProcedures(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToIssues(
        MultipleAssociationRequest request,
        IControlService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToIssues(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromIssues(
        MultipleAssociationRequest request,
        IControlService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromIssues(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Control mapRequestToControl( ControlRequest request ) {
        var model = new Control
        {
            Id = request.Id,
            Name = request.Name,
            Objective = request.Objective,
            OwnerDepartment = request.OwnerDepartment,
            ControlType = request.ControlType,
            Frequency = request.Frequency,
            Status = request.Status,
        };
        return model;
    }

}
