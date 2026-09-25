
using governanceonaspdotnet.Service;
using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Api;

public static class RegulationEndpoints
{
    public static IEndpointRouteBuilder MapRegulationEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/regulation").WithTags("Regulations");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);


    group.MapPut("/addToObligations", AddToObligations);
    group.MapPut("/removeFromObligations", RemoveFromObligations);

    group.MapPut("/addToCompliancePrograms", AddToCompliancePrograms);
    group.MapPut("/removeFromCompliancePrograms", RemoveFromCompliancePrograms);


        return app;
    }

    private static async Task<IResult> Create(
        RegulationRequest request,
        IRegulationService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToRegulation( request );

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
        RegulationRequest request,
        IRegulationService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToRegulation( request );

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
        IRegulationService service,
        CancellationToken cancellationToken) {

        var regulation = await service.Get(identifier, cancellationToken);
        return regulation is null ? Results.NotFound() : Results.Ok( regulation );
    }


    private static async Task<IResult> GetAll(
        IRegulationService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( RegulationResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IRegulationService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToObligations(
        MultipleAssociationRequest request,
        IRegulationService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToObligations(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromObligations(
        MultipleAssociationRequest request,
        IRegulationService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromObligations(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToCompliancePrograms(
        MultipleAssociationRequest request,
        IRegulationService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToCompliancePrograms(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromCompliancePrograms(
        MultipleAssociationRequest request,
        IRegulationService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromCompliancePrograms(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Regulation mapRequestToRegulation( RegulationRequest request ) {
        var model = new Regulation
        {
            Id = request.Id,
            Name = request.Name,
            Citation = request.Citation,
            Jurisdiction = request.Jurisdiction,
            PublicationUrl = request.PublicationUrl,
        };
        return model;
    }

}
