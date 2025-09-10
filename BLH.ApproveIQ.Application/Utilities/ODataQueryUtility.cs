using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Query.Validator;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Microsoft.OData.UriParser;
using BLH.ApproveIQ.Domain.Models;

namespace BLH.ApproveIQ.Application.Utilities;

public class ODataQueryUtility<TSourceModel>
{
    private readonly AllowedQueryOptions _allowedQueryOptions =
        AllowedQueryOptions.Top | AllowedQueryOptions.Skip | AllowedQueryOptions.Filter | AllowedQueryOptions.OrderBy | AllowedQueryOptions.Select;
    private readonly HttpRequest _httpRequest;
    private readonly ODataQuerySettings _odataQuerySettings;
    private readonly ODataValidationSettings _odataValidationSettings;
    private readonly ODataQueryOptions _queryOptions;

    public ODataQueryUtility(
        HttpRequest httpRequest)
    {
        _httpRequest = httpRequest;
        _odataQuerySettings = new ODataQuerySettings();
        _odataValidationSettings = new ODataValidationSettings { AllowedQueryOptions = _allowedQueryOptions };
        _queryOptions = GetODataQueryOptions();
    }

    public async Task<PagedResult<TOutput>> GetPagedResult<TOutput>(
        IQueryable<TSourceModel> baseQuery,
        Func<TSourceModel, TOutput> objectProjection,
        CancellationToken cancellationToken = default)
    {
        ValidateODataQuery();

        int count;
        if (_queryOptions.Filter != null)
            count = await (_queryOptions.Filter.ApplyTo(baseQuery, _odataQuerySettings) as IQueryable<TSourceModel>)!.CountAsync(cancellationToken);
        else
            count = await baseQuery.CountAsync(cancellationToken);

        IEnumerable<TSourceModel> result = new List<TSourceModel>();
        if (count > 0)
            result = await ApplyODataQuery(baseQuery).ToArrayAsync(cancellationToken);

        var items = result.Where(o => o != null).Select(o => objectProjection(o));

        return new PagedResult<TOutput>(items, count);
    }

    public IQueryable<TSourceModel> ApplyODataQuery(IQueryable<TSourceModel> baseQuery)
    {
        var query = _queryOptions.ApplyTo(baseQuery, _odataQuerySettings) as IQueryable<TSourceModel>;

        return query!;
    }

    public void ValidateODataQuery()
    {
        IQueryCollection queryParameters = _httpRequest.Query;

        foreach (var kvp in queryParameters)
        {
            if (!_queryOptions.IsSupportedQueryOption(kvp.Key) && kvp.Key.StartsWith("$", StringComparison.Ordinal))
            {
                throw new Exception("Custom query options that start with $ are not supported in OData");
            }
        }

        try
        {
            _queryOptions.Validate(_odataValidationSettings);
        }
        catch (ODataException ex)
        {
            string selectOptionIsNotAllow = "Query option 'Select' is not allowed";

            string errorMessage = ex.Message;
            if (ex.Message.Contains(selectOptionIsNotAllow, StringComparison.OrdinalIgnoreCase))
            {
                errorMessage = selectOptionIsNotAllow;
            }

            throw new Exception(errorMessage);
        }
    }

    private ODataQueryOptions GetODataQueryOptions()
    {
        var sourceModelType = typeof(TSourceModel);
        var context = new ODataQueryContext(ODataQueryUtility<TSourceModel>.GetEdmModel(sourceModelType), sourceModelType, new ODataPath());

        return new ODataQueryOptions<TSourceModel>(context, _httpRequest);
    }

    private static IEdmModel GetEdmModel(Type sourceModelType)
    {
        var builder = new ODataConventionModelBuilder();
        EntityTypeConfiguration entityTypeConfiguration = builder.AddEntityType(sourceModelType);
        entityTypeConfiguration.HasKey(sourceModelType.GetProperty("Id"));
        builder.AddEntitySet(sourceModelType.Name, entityTypeConfiguration);
        var model = builder.GetEdmModel();

        return model;
    }
}
