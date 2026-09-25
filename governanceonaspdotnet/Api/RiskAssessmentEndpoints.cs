
using governanceonaspdotnet.Service;
using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Api;

public static class RiskAssessmentEndpoints
{
    public static IEndpointRouteBuilder MapRiskAssessmentEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/riskAssessment").WithTags("RiskAssessments");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignRisk", AssignRisk);
        group.MapPut("/unassignRisk", UnassignRisk);


        return app;
    }

    private static async Task<IResult> Create(
        RiskAssessmentRequest request,
        IRiskAssessmentService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToRiskAssessment( request );

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
        RiskAssessmentRequest request,
        IRiskAssessmentService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToRiskAssessment( request );

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
        IRiskAssessmentService service,
        CancellationToken cancellationToken) {

        var riskAssessment = await service.Get(identifier, cancellationToken);
        return riskAssessment is null ? Results.NotFound() : Results.Ok( riskAssessment );
    }


    private static async Task<IResult> GetAll(
        IRiskAssessmentService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( RiskAssessmentResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IRiskAssessmentService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignRisk(
        AssociationRequest request,
        IRiskAssessmentService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignRisk(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignRisk(
    AssociationRequest request,
    IRiskAssessmentService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignRisk(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static RiskAssessment mapRequestToRiskAssessment( RiskAssessmentRequest request ) {
        var model = new RiskAssessment
        {
            Id = request.Id,
            AssessmentDate = request.AssessmentDate,
            Assessor = request.Assessor,
            Summary = request.Summary,
            AssessmentType = request.AssessmentType,
        };
        return model;
    }

}
