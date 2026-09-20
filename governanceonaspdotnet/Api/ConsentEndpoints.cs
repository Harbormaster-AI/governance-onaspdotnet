using governanceonaspdotnet.Service;
using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Api;

public static class ConsentEndpoints
{
    public static IEndpointRouteBuilder MapConsentEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/consent").WithTags("Consents");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignPrivacyNotice", AssignPrivacyNotice);
        group.MapPut("/unassignPrivacyNotice", UnassignPrivacyNotice);

    group.MapPut("/addToProcessingActivities", AddToProcessingActivities);
    group.MapPut("/removeFromProcessingActivities", RemoveFromProcessingActivities);


        return app;
    }

    private static async Task<IResult> Create(
        ConsentRequest request,
        IConsentService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToConsent( request );

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
        ConsentRequest request,
        IConsentService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToConsent( request );

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
        IConsentService service,
        CancellationToken cancellationToken) {

        var consent = await service.Get(identifier, cancellationToken);
        return consent is null ? Results.NotFound() : Results.Ok( consent );
    }


    private static async Task<IResult> GetAll(
        IConsentService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( ConsentResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IConsentService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignPrivacyNotice(
        AssociationRequest request,
        IConsentService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignPrivacyNotice(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignPrivacyNotice(
    AssociationRequest request,
    IConsentService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignPrivacyNotice(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToProcessingActivities(
        MultipleAssociationRequest request,
        IConsentService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToProcessingActivities(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromProcessingActivities(
        MultipleAssociationRequest request,
        IConsentService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromProcessingActivities(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Consent mapRequestToConsent( ConsentRequest request ) {
        var model = new Consent
        {
            Id = request.Id,
            SubjectIdentifier = request.SubjectIdentifier,
            CaptureDate = request.CaptureDate,
            ExpiryDate = request.ExpiryDate,
            ConsentType = request.ConsentType,
            Status = request.Status,
        };
        return model;
    }

}
