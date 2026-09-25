
using governanceonaspdotnet.Service;
using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Api;

public static class ControlTest_Endpoints
{
    public static IEndpointRouteBuilder MapControlTest_Endpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/controlTest_").WithTags("ControlTest_s");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignControl", AssignControl);
        group.MapPut("/unassignControl", UnassignControl);
        group.MapPut("/assignEngagement", AssignEngagement);
        group.MapPut("/unassignEngagement", UnassignEngagement);

    group.MapPut("/addToEvidence", AddToEvidence);
    group.MapPut("/removeFromEvidence", RemoveFromEvidence);


        return app;
    }

    private static async Task<IResult> Create(
        ControlTest_Request request,
        IControlTest_Service service,
        CancellationToken cancellationToken) {

        var model = mapRequestToControlTest_( request );

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
        ControlTest_Request request,
        IControlTest_Service service,
        CancellationToken cancellationToken) {

        var model = mapRequestToControlTest_( request );

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
        IControlTest_Service service,
        CancellationToken cancellationToken) {

        var controlTest_ = await service.Get(identifier, cancellationToken);
        return controlTest_ is null ? Results.NotFound() : Results.Ok( controlTest_ );
    }


    private static async Task<IResult> GetAll(
        IControlTest_Service service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( ControlTest_Response.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IControlTest_Service service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignControl(
        AssociationRequest request,
        IControlTest_Service service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignControl(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignControl(
    AssociationRequest request,
    IControlTest_Service service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignControl(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignEngagement(
        AssociationRequest request,
        IControlTest_Service service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignEngagement(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignEngagement(
    AssociationRequest request,
    IControlTest_Service service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignEngagement(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToEvidence(
        MultipleAssociationRequest request,
        IControlTest_Service service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToEvidence(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromEvidence(
        MultipleAssociationRequest request,
        IControlTest_Service service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromEvidence(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static ControlTest_ mapRequestToControlTest_( ControlTest_Request request ) {
        var model = new ControlTest_
        {
            Id = request.Id,
            Name = request.Name,
            TestPeriodStart = request.TestPeriodStart,
            TestPeriodEnd = request.TestPeriodEnd,
            SampleSize = request.SampleSize,
            TestType = request.TestType,
            Effectiveness = request.Effectiveness,
            Status = request.Status,
        };
        return model;
    }

}
