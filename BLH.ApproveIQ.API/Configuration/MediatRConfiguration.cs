using MediatR;
using BLH.ApproveIQ.Application.Behaviors;
using BLH.ApproveIQ.Infrastructure.Idempotence;

namespace BLH.ApproveIQ.API.Configuration;

public static class MediatRConfiguration
{
    public static void ConfigureMediatR(this IServiceCollection services)
    {
        services.AddMediatR(Application.AssemblyReference.Assembly);

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));

        //services.Decorate(typeof(INotificationHandler<>), typeof(IdempotentDomainEventHandler<>));
    }
}
