using Microsoft.Extensions.Options;
using OpenAPIDemo.Worker;
using Polly;
using Polly.Extensions.Http;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddOptions<MyOptions>()
            .Configure<IConfiguration>((options, configuration) => configuration.GetSection("MyOptions").Bind(options));
        services.AddHttpClient<ICatFactsClient, CatFactsClient>((sp, client) =>
        {
            var options = sp.GetRequiredService<IOptions<MyOptions>>();

            client.BaseAddress = options.Value.CatFactsBaseUrl;
        })
        .AddPolicyHandler((sp, request) => HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))));
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
