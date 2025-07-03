using _011Global.JobsService.JobInterfaces;
using _011Global.Shared;

namespace _011Global.JobsService.JobImplementations;

public class RecurringChargeJob : Job, IJob
{
    protected override int IterationWaitTime { get { return 5000; } } //TODO: from config
    public RecurringChargeJob(CancellationTokenBase cancellationTokenBase, ILogger<RecurringChargeJob> logger, IServiceProvider serviceProvider) : base(cancellationTokenBase, logger, serviceProvider)
    {
    }

    

    protected override async Task WorkLoad(CancellationToken cancellationToken)
    { 
        //TODO: sample the customers’ data and submit corresponding transactions
        
        logger.LogInformation($"{Name} my last run time was: {DateTime.Now}");
    }
} 