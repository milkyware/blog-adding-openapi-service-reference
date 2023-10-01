namespace OpenAPIDemo.Worker
{
    public class Worker : BackgroundService
    {
        private readonly CatFactsClient _catFactsClient;
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger, CatFactsClient catFactsClient)
        {
            _catFactsClient = catFactsClient;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var fact = await _catFactsClient.GetRandomFactAsync(null);
                _logger.LogInformation("Did you know {fact}", fact.Fact.ToLower());
                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}