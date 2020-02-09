using Microsoft.Extensions.Logging;

namespace Core
{
    public class ExceptionHandler
    {
        private readonly ILogger<ExceptionHandler> _log;

        public ExceptionHandler(ILogger<ExceptionHandler> log)
        {
            _log = log;
        }

        public void HandleError(string errorText)
        {
            _log.LogError(errorText);
        }
    }
}
