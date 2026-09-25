
using governanceonaspdotnet.Service;
using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Api;

public static class OrganizationEndpoints
{
    public static IEndpointRouteBuilder MapOrganizationEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/organization").WithTags("Organizations");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);


    group.MapPut("/addToGovernanceBodies", AddToGovernanceBodies);
    group.MapPut("/removeFromGovernanceBodies", RemoveFromGovernanceBodies);

    group.MapPut("/addToPolicies", AddToPolicies);
    group.MapPut("/removeFromPolicies", RemoveFromPolicies);

    group.MapPut("/addToRisks", AddToRisks);
    group.MapPut("/removeFromRisks", RemoveFromRisks);

    group.MapPut("/addToThirdParties", AddToThirdParties);
    group.MapPut("/removeFromThirdParties", RemoveFromThirdParties);

    group.MapPut("/addToRecordsRepositories", AddToRecordsRepositories);
    group.MapPut("/removeFromRecordsRepositories", RemoveFromRecordsRepositories);

    group.MapPut("/addToDataProcessingActivities", AddToDataProcessingActivities);
    group.MapPut("/removeFromDataProcessingActivities", RemoveFromDataProcessingActivities);

    group.MapPut("/addToCompliancePrograms", AddToCompliancePrograms);
    group.MapPut("/removeFromCompliancePrograms", RemoveFromCompliancePrograms);

    group.MapPut("/addToAuditPrograms", AddToAuditPrograms);
    group.MapPut("/removeFromAuditPrograms", RemoveFromAuditPrograms);

    group.MapPut("/addToBusinessUnits", AddToBusinessUnits);
    group.MapPut("/removeFromBusinessUnits", RemoveFromBusinessUnits);

    group.MapPut("/addToMatters", AddToMatters);
    group.MapPut("/removeFromMatters", RemoveFromMatters);

    group.MapPut("/addToDataBreaches", AddToDataBreaches);
    group.MapPut("/removeFromDataBreaches", RemoveFromDataBreaches);


        return app;
    }

    private static async Task<IResult> Create(
        OrganizationRequest request,
        IOrganizationService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToOrganization( request );

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
        OrganizationRequest request,
        IOrganizationService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToOrganization( request );

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
        IOrganizationService service,
        CancellationToken cancellationToken) {

        var organization = await service.Get(identifier, cancellationToken);
        return organization is null ? Results.NotFound() : Results.Ok( organization );
    }


    private static async Task<IResult> GetAll(
        IOrganizationService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( OrganizationResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IOrganizationService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToGovernanceBodies(
        MultipleAssociationRequest request,
        IOrganizationService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToGovernanceBodies(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromGovernanceBodies(
        MultipleAssociationRequest request,
        IOrganizationService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromGovernanceBodies(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToPolicies(
        MultipleAssociationRequest request,
        IOrganizationService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToPolicies(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromPolicies(
        MultipleAssociationRequest request,
        IOrganizationService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromPolicies(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToRisks(
        MultipleAssociationRequest request,
        IOrganizationService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToRisks(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromRisks(
        MultipleAssociationRequest request,
        IOrganizationService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromRisks(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToThirdParties(
        MultipleAssociationRequest request,
        IOrganizationService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToThirdParties(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromThirdParties(
        MultipleAssociationRequest request,
        IOrganizationService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromThirdParties(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToRecordsRepositories(
        MultipleAssociationRequest request,
        IOrganizationService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToRecordsRepositories(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromRecordsRepositories(
        MultipleAssociationRequest request,
        IOrganizationService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromRecordsRepositories(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToDataProcessingActivities(
        MultipleAssociationRequest request,
        IOrganizationService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToDataProcessingActivities(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromDataProcessingActivities(
        MultipleAssociationRequest request,
        IOrganizationService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromDataProcessingActivities(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToCompliancePrograms(
        MultipleAssociationRequest request,
        IOrganizationService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToCompliancePrograms(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromCompliancePrograms(
        MultipleAssociationRequest request,
        IOrganizationService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromCompliancePrograms(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToAuditPrograms(
        MultipleAssociationRequest request,
        IOrganizationService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToAuditPrograms(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromAuditPrograms(
        MultipleAssociationRequest request,
        IOrganizationService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromAuditPrograms(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToBusinessUnits(
        MultipleAssociationRequest request,
        IOrganizationService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToBusinessUnits(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromBusinessUnits(
        MultipleAssociationRequest request,
        IOrganizationService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromBusinessUnits(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToMatters(
        MultipleAssociationRequest request,
        IOrganizationService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToMatters(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromMatters(
        MultipleAssociationRequest request,
        IOrganizationService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromMatters(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToDataBreaches(
        MultipleAssociationRequest request,
        IOrganizationService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToDataBreaches(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromDataBreaches(
        MultipleAssociationRequest request,
        IOrganizationService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromDataBreaches(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Organization mapRequestToOrganization( OrganizationRequest request ) {
        var model = new Organization
        {
            Id = request.Id,
            Name = request.Name,
            LegalName = request.LegalName,
            Jurisdiction = request.Jurisdiction,
            IndustrySector = request.IndustrySector,
        };
        return model;
    }

}
