
using governanceonaspdotnet.Service;
using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Api;

public static class DispositionReviewEndpoints
{
    public static IEndpointRouteBuilder MapDispositionReviewEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/dispositionReview").WithTags("DispositionReviews");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignRecord", AssignRecord);
        group.MapPut("/unassignRecord", UnassignRecord);
        group.MapPut("/assignRetentionSchedule", AssignRetentionSchedule);
        group.MapPut("/unassignRetentionSchedule", UnassignRetentionSchedule);


        return app;
    }

    private static async Task<IResult> Create(
        DispositionReviewRequest request,
        IDispositionReviewService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToDispositionReview( request );

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
        DispositionReviewRequest request,
        IDispositionReviewService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToDispositionReview( request );

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
        IDispositionReviewService service,
        CancellationToken cancellationToken) {

        var dispositionReview = await service.Get(identifier, cancellationToken);
        return dispositionReview is null ? Results.NotFound() : Results.Ok( dispositionReview );
    }


    private static async Task<IResult> GetAll(
        IDispositionReviewService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( DispositionReviewResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IDispositionReviewService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignRecord(
        AssociationRequest request,
        IDispositionReviewService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignRecord(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignRecord(
    AssociationRequest request,
    IDispositionReviewService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignRecord(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignRetentionSchedule(
        AssociationRequest request,
        IDispositionReviewService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignRetentionSchedule(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignRetentionSchedule(
    AssociationRequest request,
    IDispositionReviewService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignRetentionSchedule(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static DispositionReview mapRequestToDispositionReview( DispositionReviewRequest request ) {
        var model = new DispositionReview
        {
            Id = request.Id,
            ReviewDate = request.ReviewDate,
            Reviewer = request.Reviewer,
            Notes = request.Notes,
            Outcome = request.Outcome,
        };
        return model;
    }

}
