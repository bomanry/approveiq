using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Quartz;
using BLH.ApproveIQ.Domain.Primitives;
using BLH.ApproveIQ.Persistence;
using BLH.ApproveIQ.Persistence.Outbox;

namespace BLH.ApproveIQ.Infrastructure.BackgroundJobs;

[DisallowConcurrentExecution]
public class ProcessOutboxMessagesJob : IJob
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IPublisher _publisher;

    public ProcessOutboxMessagesJob(ApplicationDbContext dbContext, IPublisher publisher)
    {
        _dbContext = dbContext;
        _publisher = publisher;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        List<OutboxMessage> messages = await _dbContext
            .Set<OutboxMessage>()
            .Where(m => m.ProcessedOnUtc == null)
            .Take(20)
            .ToListAsync(context.CancellationToken);

        foreach (OutboxMessage outboxMessage in messages)
        {
            IDomainEvent? domainEvent = JsonConvert
               .DeserializeObject<IDomainEvent>(
                   outboxMessage.Content,
                   new JsonSerializerSettings
                   {
                       TypeNameHandling = TypeNameHandling.All
                   });

            if (domainEvent is null)
            {
                continue;
            }

            try
            {
                await _publisher.Publish(domainEvent, context.CancellationToken);
            }
            catch (Exception ex)
            {
                outboxMessage.Error = ex.ToString();
            }
            finally
            {
                outboxMessage.ProcessedOnUtc = DateTime.UtcNow;
            }
        }

        await _dbContext.SaveChangesAsync();
    }
}
