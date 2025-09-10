using MediatR;
using BLH.ApproveIQ.Domain.Shared;

namespace BLH.ApproveIQ.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}