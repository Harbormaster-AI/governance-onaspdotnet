
using governanceonaspdotnet.Service;
using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Api;

public static class IssueEndpoints
{
    public static IEndpointRouteBuilder MapIssueEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/issue").WithTags("Issues");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignRisk", AssignRisk);
        group.MapPut("/unassignRisk", UnassignRisk);
        group.MapPut("/assignFinding", AssignFinding);
        group.MapPut("/unassignFinding", UnassignFinding);
        group.MapPut("/assignControl", AssignControl);
        group.MapPut("/unassignControl", UnassignControl);

    group.MapPut("/addToCorrectiveActions", AddToCorrectiveActions);
    group.MapPut("/removeFromCorrectiveActions", RemoveFromCorrectiveActions);


        return app;
    }

    private static async Task<IResult> Create(
        IssueRequest request,
        IIssueService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToIssue( request );

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
        IssueRequest request,
        IIssueService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToIssue( request );

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
        IIssueService service,
        CancellationToken cancellationToken) {

        var issue = await service.Get(identifier, cancellationToken);
        return issue is null ? Results.NotFound() : Results.Ok( issue );
    }


    private static async Task<IResult> GetAll(
        IIssueService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( IssueResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IIssueService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignRisk(
        AssociationRequest request,
        IIssueService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignRisk(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignRisk(
    AssociationRequest request,
    IIssueService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignRisk(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignFinding(
        AssociationRequest request,
        IIssueService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignFinding(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignFinding(
    AssociationRequest request,
    IIssueService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignFinding(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignControl(
        AssociationRequest request,
        IIssueService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignControl(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignControl(
    AssociationRequest request,
    IIssueService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignControl(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToCorrectiveActions(
        MultipleAssociationRequest request,
        IIssueService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToCorrectiveActions(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromCorrectiveActions(
        MultipleAssociationRequest request,
        IIssueService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromCorrectiveActions(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Issue mapRequestToIssue( IssueRequest request ) {
        var model = new Issue
        {
            Id = request.Id,
            Title = request.Title,
            OpenedDate = request.OpenedDate,
            ClosedDate = request.ClosedDate,
            IssueType = request.IssueType,
            Priority = request.Priority,
            Status = request.Status,
        };
        return model;
    }

}
