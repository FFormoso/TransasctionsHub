using System.Runtime.CompilerServices;
using _011Global.Shared;
using _011Global.Shared.JobsServiceDBContext.Interfaces;

namespace _011Global.JobsService.JobInterfaces;
public abstract class Job
{
    protected abstract int IterationWaitTime { get; }
    private CancellationToken _globalCancellationToken { get; set;  }
    protected CancellationToken cancellationToken;
    protected CancellationTokenSource cts { get; set; }

    protected ILogger logger { get; private set;  }

    private readonly IServiceProvider _serviceProvider;

    public string Name { get; private set; }

    private Task RunningTask { get; set; }
    
    public Job(CancellationTokenBase globalCancellationToken, ILogger logger, IServiceProvider serviceProvider)
    {
    this.
        _globalCancellationToken = globalCancellationToken;
        this.logger = logger;
        Name = this.GetType().FullName;
        _serviceProvider = serviceProvider;
    }

    protected abstract Task WorkLoad(CancellationToken cancellationToken);

    public async Task Start()
    {
        try
        {
            if (RunningTask != null && !RunningTask.IsCompleted)
                return;
            cts = CancellationTokenSource.CreateLinkedTokenSource(_globalCancellationToken);
            cancellationToken = cts.Token;

            using (var scope = _serviceProvider.CreateAsyncScope())
            {
                var jobRepo = scope.ServiceProvider.GetRequiredService<IJobsServiceRepository>();

                await jobRepo.UpdateStartedJob(Name, isRunning: true, lastStartDate: DateTime.Now);
            }

            logger.LogInformation($"{Name} started {DateTime.Now}");
            RunningTask = await Task.Factory.StartNew(async () =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        await WorkLoad(cancellationToken);
                        await Task.Delay(IterationWaitTime, cancellationToken);
                    }
                    catch (TaskCanceledException ex)
                    {
                    }
                    catch (Exception ex)
                    {
                        logger.LogCritical($"{Name} threw an exception: {ex.Message}");
                        //log major exception here
                    }
                }
            });
        }
        catch (Exception ex)
        {
            logger.LogError($"Error starting job {Name}. Exception: {ex.Message}");
        }
    }

    public virtual async Task Stop()
    {
        try
        {
            using (var scope = _serviceProvider.CreateAsyncScope())
            {
                var jobRepo = scope.ServiceProvider.GetRequiredService<IJobsServiceRepository>();

                await jobRepo.UpdateStopedJob(Name, isRunning: true, lastStopDate: DateTime.Now);
            }
            
            if (RunningTask != null && !RunningTask.IsCompleted)
            {
                cts.Cancel();
                await RunningTask;
                logger.LogInformation($"{Name} stopped {DateTime.Now}");
            }
        }
        catch (Exception ex)
        {
            logger.LogError($"Error stoping job {Name}. Exception: {ex.Message}");
        }
    }

}

