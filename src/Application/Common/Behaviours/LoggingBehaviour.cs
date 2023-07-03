using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Baires.Application.Common.Behaviours;

public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly ILogger _logger;

    public LoggingBehaviour(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;

        await Task.Run(() => _logger.LogInformation($"Request: {requestName}. Object: {request}"));
    }
}
