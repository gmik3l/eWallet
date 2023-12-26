using test.DbModels;
using test.QueryModels;
using test.Services;

namespace test.Services;

public class BackGroundWorkingService : BackgroundService
{
    private Timer _timer;
    
    readonly ILogger<BackGroundWorkingService> _logger;
    
    private readonly IServiceProvider _service;
    
    public BackGroundWorkingService(ILogger<BackGroundWorkingService> logger, IServiceProvider service)
    {
        _logger = logger;
        _service = service;
    }
    
    protected  override async Task ExecuteAsync(CancellationToken stoppingToken)
    {

        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Background job is starting...");

            using var scope = _service.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var accountsForLog= dbContext.Accounts.Where(o => o.AccountBalance > 0).ToList();

            foreach (var account in accountsForLog)
            {
                Console.WriteLine($"Account number: {account.AccountNumber} has balance: {account.AccountBalance}{account.AccountCurrency}");
            }
        
            await Task.Delay(5000, stoppingToken).ConfigureAwait(false);
        }
        
    }
}