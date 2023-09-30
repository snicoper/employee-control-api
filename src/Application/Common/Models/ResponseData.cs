using AutoMapper;
using EmployeeControl.Application.Common.EntityFramework.Filter;
using EmployeeControl.Application.Common.EntityFramework.OrderBy;
using Microsoft.EntityFrameworkCore;

namespace EmployeeControl.Application.Common.Models;

public class ResponseData<TDto> : RequestData
{
    public IEnumerable<TDto> Items { get; private init; } = new HashSet<TDto>();

    public bool HasPreviousPage => PageNumber > 1;

    public bool HasNextPage => PageNumber < TotalPages;

    public static async Task<ResponseData<TDto>> CreateAsync<TEntity>(
        IQueryable<TEntity> source,
        RequestData request,
        IMapper mapper,
        CancellationToken cancellationToken)
        where TEntity : class
    {
        var totalItems = await source
            .Filter(request)
            .Ordering(request)
            .CountAsync(cancellationToken);

        var items = await source
            .Filter(request)
            .Ordering(request)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var totalPages = (int)Math.Ceiling(totalItems / (double)request.PageSize);

        return new ResponseData<TDto>
        {
            TotalItems = totalItems,
            PageNumber = request.PageNumber,
            TotalPages = totalPages,
            Ratio = request.Ratio,
            PageSize = request.PageSize,
            Items = mapper.Map<List<TDto>>(items),
            Orders = request.Orders,
            Filters = request.Filters
        };
    }
}
