using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using appsin.Common;

public class TimedTaskService : BackgroundService
{
    private readonly ILogger<TimedTaskService> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly PeriodicTimer _timer = new(TimeSpan.FromMinutes(10));//You can define the timespan here

    public TimedTaskService(ILogger<TimedTaskService> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        //Timer start
        while (await _timer.WaitForNextTickAsync(stoppingToken) && !stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var myDependency = scope.ServiceProvider.GetRequiredService<IMyDependency>();
                await myDependency.DoWorkAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "the timer excute fail");
            }
        }
        //Timer end
    }
}

public interface IMyDependency
{
    Task DoWorkAsync();
}

public class MyDependency : IMyDependency
{
    public async Task DoWorkAsync()
    {
        // Here is the specific logic of the scheduled tasks, for example:
        Console.WriteLine("Please go to Common/TimeHelper.cs to code the scheduled task. This prompt is just for reminding." + DateTime.Now);
    }
}

