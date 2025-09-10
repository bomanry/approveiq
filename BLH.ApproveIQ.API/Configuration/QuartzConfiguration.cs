using Quartz;
using BLH.ApproveIQ.Infrastructure.BackgroundJobs;

namespace BLH.ApproveIQ.API.Configuration;

public static class QuartzConfiguration
{
    public static void ConfigureQuartz(this IServiceCollection services)
    {
        services.AddQuartz(configure =>
        {
            var processOutboxMessagesJobKey = new JobKey(nameof(ProcessOutboxMessagesJob));
            var syncUsersWithEntraJobKey = new JobKey(nameof(SyncUsersWithEntraJob));
            
         
            configure
                .AddJob<ProcessOutboxMessagesJob>(processOutboxMessagesJobKey)
                .AddTrigger(
                    trigger =>
                        trigger.ForJob(processOutboxMessagesJobKey)
                            .WithSimpleSchedule(
                                schedule =>
                                    schedule.WithIntervalInSeconds(10)
                                        .RepeatForever()));
            
            configure
                .AddJob<SyncUsersWithEntraJob>(syncUsersWithEntraJobKey)
                .AddTrigger(
                    trigger =>
                        trigger.ForJob(syncUsersWithEntraJobKey)
                            .WithSimpleSchedule(
                                schedule =>
                                    schedule.WithIntervalInMinutes(1)
                                        .RepeatForever()));
            
            configure.UseMicrosoftDependencyInjectionJobFactory();
        });

        services.AddQuartzHostedService();
    }
}
