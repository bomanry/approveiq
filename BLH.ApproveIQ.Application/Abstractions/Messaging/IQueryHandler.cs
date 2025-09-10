using MediatR;
using BLH.ApproveIQ.Domain.Shared;

namespace BLH.ApproveIQ.Application.Abstractions.Messaging;

public interface IQueryHandler<TQuery, TResponse>
    : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}