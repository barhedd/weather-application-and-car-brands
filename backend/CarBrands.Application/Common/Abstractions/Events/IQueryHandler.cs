using CarBrands.Application.Common.Results;

namespace CarBrands.Application.Common.Abstractions.Events;

public interface IQueryHandler<TQuery, TResult>
{
    Task<Result<TResult>> Handle(
        TQuery query,
        CancellationToken cancellationToken);
}
