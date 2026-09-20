using governanceonaspdotnet.Service;
using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Api;

public static class ThirdPartyAssessmentEndpoints
{
    public static IEndpointRouteBuilder MapThirdPartyAssessmentEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/thirdPartyAssessment").WithTags("ThirdPartyAssessments");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignThirdParty", AssignThirdParty);
        group.MapPut("/unassignThirdParty", UnassignThirdParty);

    group.MapPut("/addToIssues", AddToIssues);
    group.MapPut("/removeFromIssues", RemoveFromIssues);


        return app;
    }

    private static async Task<IResult> Create(
        ThirdPartyAssessmentRequest request,
        IThirdPartyAssessmentService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToThirdPartyAssessment( request );

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
        ThirdPartyAssessmentRequest request,
        IThirdPartyAssessmentService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToThirdPartyAssessment( request );

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
        IThirdPartyAssessmentService service,
        CancellationToken cancellationToken) {

        var thirdPartyAssessment = await service.Get(identifier, cancellationToken);
        return thirdPartyAssessment is null ? Results.NotFound() : Results.Ok( thirdPartyAssessment );
    }


    private static async Task<IResult> GetAll(
        IThirdPartyAssessmentService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( ThirdPartyAssessmentResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IThirdPartyAssessmentService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignThirdParty(
        AssociationRequest request,
        IThirdPartyAssessmentService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignThirdParty(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignThirdParty(
    AssociationRequest request,
    IThirdPartyAssessmentService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignThirdParty(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToIssues(
        MultipleAssociationRequest request,
        IThirdPartyAssessmentService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToIssues(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromIssues(
        MultipleAssociationRequest request,
        IThirdPartyAssessmentService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromIssues(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static ThirdPartyAssessment mapRequestToThirdPartyAssessment( ThirdPartyAssessmentRequest request ) {
        var model = new ThirdPartyAssessment
        {
            Id = request.Id,
            AssessmentDate = request.AssessmentDate,
            Assessor = request.Assessor,
            AssessmentType = request.AssessmentType,
            Result = request.Result,
        };
        return model;
    }

}
