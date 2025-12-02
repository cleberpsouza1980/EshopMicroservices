using MediatR;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Behavior
{
    public class LoggingBehavior <TRequest,TResponse>
        (ILogger<LoggingBehavior<TRequest,TResponse>> logger)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull, TResponse
        where TResponse : notnull
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            logger.LogInformation("[START] handler request={Request} - Response={Response}", 
                typeof(TRequest).Name, typeof(TResponse).Name,request);


            var timer = new System.Diagnostics.Stopwatch();
            timer.Start();

            var response = await next();

            timer.Stop();

            var timerTaken = timer.Elapsed;
            if(timerTaken.Seconds > 3)
            {
                logger.LogWarning("[PERFORMACE] handler request={Request} - Response={Response} - TimeTaken={TimeTaken}ms", 
                    typeof(TRequest).Name, typeof(TResponse).Name,timerTaken.Seconds);
            }

            logger.LogInformation("[END] handler request={Request} - Response={Response} - TimeTaken={TimeTaken}ms", 
                typeof(TRequest).Name, typeof(TResponse).Name,timerTaken.Seconds);

            return response;
        }
    }
}
