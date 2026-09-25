
using governanceonaspdotnet.Service;
using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Api;

public static class PrivacyNoticeEndpoints
{
    public static IEndpointRouteBuilder MapPrivacyNoticeEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/privacyNotice").WithTags("PrivacyNotices");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignOrganization", AssignOrganization);
        group.MapPut("/unassignOrganization", UnassignOrganization);

    group.MapPut("/addToProcessingActivities", AddToProcessingActivities);
    group.MapPut("/removeFromProcessingActivities", RemoveFromProcessingActivities);

    group.MapPut("/addToConsents", AddToConsents);
    group.MapPut("/removeFromConsents", RemoveFromConsents);


        return app;
    }

    private static async Task<IResult> Create(
        PrivacyNoticeRequest request,
        IPrivacyNoticeService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToPrivacyNotice( request );

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
        PrivacyNoticeRequest request,
        IPrivacyNoticeService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToPrivacyNotice( request );

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
        IPrivacyNoticeService service,
        CancellationToken cancellationToken) {

        var privacyNotice = await service.Get(identifier, cancellationToken);
        return privacyNotice is null ? Results.NotFound() : Results.Ok( privacyNotice );
    }


    private static async Task<IResult> GetAll(
        IPrivacyNoticeService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( PrivacyNoticeResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IPrivacyNoticeService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignOrganization(
        AssociationRequest request,
        IPrivacyNoticeService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignOrganization(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignOrganization(
    AssociationRequest request,
    IPrivacyNoticeService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignOrganization(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToProcessingActivities(
        MultipleAssociationRequest request,
        IPrivacyNoticeService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToProcessingActivities(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromProcessingActivities(
        MultipleAssociationRequest request,
        IPrivacyNoticeService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromProcessingActivities(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToConsents(
        MultipleAssociationRequest request,
        IPrivacyNoticeService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToConsents(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromConsents(
        MultipleAssociationRequest request,
        IPrivacyNoticeService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromConsents(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static PrivacyNotice mapRequestToPrivacyNotice( PrivacyNoticeRequest request ) {
        var model = new PrivacyNotice
        {
            Id = request.Id,
            Title = request.Title,
            Audience = request.Audience,
            VersionLabel = request.VersionLabel,
            PublicationDate = request.PublicationDate,
            PublicationUrl = request.PublicationUrl,
            Status = request.Status,
        };
        return model;
    }

}
