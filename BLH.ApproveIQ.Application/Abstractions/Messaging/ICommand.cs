using MediatR;
using BLH.ApproveIQ.Domain.Shared;

namespace BLH.ApproveIQ.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}
