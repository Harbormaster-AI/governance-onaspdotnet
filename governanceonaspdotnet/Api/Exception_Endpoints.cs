using governanceonaspdotnet.Service;
using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Api;

public static class Exception_Endpoints
{
    public static IEndpointRouteBuilder MapException_Endpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/exception_").WithTags("Exception_s");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignRetentionSchedule", AssignRetentionSchedule);
        group.MapPut("/unassignRetentionSchedule", UnassignRetentionSchedule);
        group.MapPut("/assignPolicy", AssignPolicy);
        group.MapPut("/unassignPolicy", UnassignPolicy);
        group.MapPut("/assignControl", AssignControl);
        group.MapPut("/unassignControl", UnassignControl);
        group.MapPut("/assignRisk", AssignRisk);
        group.MapPut("/unassignRisk", UnassignRisk);


        return app;
    }

    private static async Task<IResult> Create(
        Exception_Request request,
        IException_Service service,
        CancellationToken cancellationToken) {

        var model = mapRequestToException_( request );

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
        Exception_Request request,
        IException_Service service,
        CancellationToken cancellationToken) {

        var model = mapRequestToException_( request );

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
        IException_Service service,
        CancellationToken cancellationToken) {

        var exception_ = await service.Get(identifier, cancellationToken);
        return exception_ is null ? Results.NotFound() : Results.Ok( exception_ );
    }


    private static async Task<IResult> GetAll(
        IException_Service service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( Exception_Response.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IException_Service service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignRetentionSchedule(
        AssociationRequest request,
        IException_Service service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignRetentionSchedule(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignRetentionSchedule(
    AssociationRequest request,
    IException_Service service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignRetentionSchedule(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignPolicy(
        AssociationRequest request,
        IException_Service service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignPolicy(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignPolicy(
    AssociationRequest request,
    IException_Service service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignPolicy(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignControl(
        AssociationRequest request,
        IException_Service service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignControl(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignControl(
    AssociationRequest request,
    IException_Service service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignControl(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignRisk(
        AssociationRequest request,
        IException_Service service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignRisk(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignRisk(
    AssociationRequest request,
    IException_Service service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignRisk(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static Exception_ mapRequestToException_( Exception_Request request ) {
        var model = new Exception_
        {
            Id = request.Id,
            Title = request.Title,
            Justification = request.Justification,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            ExceptionType = request.ExceptionType,
            Status = request.Status,
        };
        return model;
    }

}
