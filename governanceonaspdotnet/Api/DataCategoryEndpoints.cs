
using governanceonaspdotnet.Service;
using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Api;

public static class DataCategoryEndpoints
{
    public static IEndpointRouteBuilder MapDataCategoryEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/dataCategory").WithTags("DataCategorys");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);


    group.MapPut("/addToProcessingActivities", AddToProcessingActivities);
    group.MapPut("/removeFromProcessingActivities", RemoveFromProcessingActivities);

    group.MapPut("/addToRecords", AddToRecords);
    group.MapPut("/removeFromRecords", RemoveFromRecords);

    group.MapPut("/addToDataBreaches", AddToDataBreaches);
    group.MapPut("/removeFromDataBreaches", RemoveFromDataBreaches);


        return app;
    }

    private static async Task<IResult> Create(
        DataCategoryRequest request,
        IDataCategoryService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToDataCategory( request );

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
        DataCategoryRequest request,
        IDataCategoryService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToDataCategory( request );

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
        IDataCategoryService service,
        CancellationToken cancellationToken) {

        var dataCategory = await service.Get(identifier, cancellationToken);
        return dataCategory is null ? Results.NotFound() : Results.Ok( dataCategory );
    }


    private static async Task<IResult> GetAll(
        IDataCategoryService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( DataCategoryResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IDataCategoryService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToProcessingActivities(
        MultipleAssociationRequest request,
        IDataCategoryService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToProcessingActivities(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromProcessingActivities(
        MultipleAssociationRequest request,
        IDataCategoryService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromProcessingActivities(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToRecords(
        MultipleAssociationRequest request,
        IDataCategoryService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToRecords(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromRecords(
        MultipleAssociationRequest request,
        IDataCategoryService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromRecords(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToDataBreaches(
        MultipleAssociationRequest request,
        IDataCategoryService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToDataBreaches(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromDataBreaches(
        MultipleAssociationRequest request,
        IDataCategoryService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromDataBreaches(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static DataCategory mapRequestToDataCategory( DataCategoryRequest request ) {
        var model = new DataCategory
        {
            Id = request.Id,
            Name = request.Name,
            Description = request.Description,
            Classification = request.Classification,
        };
        return model;
    }

}
