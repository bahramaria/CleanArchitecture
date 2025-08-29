using Infrastructure.RequestAudit;
using Microsoft.Extensions.Hosting;

namespace CleanArchitecture.BackgroundServices;

public class RequestAuditService(RequestAuditAgent commandAuditAgent) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        commandAuditAgent.Start(stoppingToken);

        while (!stoppingToken.IsCancellationRequested)
        {
            commandAuditAgent.EnsureStarted();
            await Task.Delay(Settings.EnsureStartedTimeout, stoppingToken);
        }
    }
}
