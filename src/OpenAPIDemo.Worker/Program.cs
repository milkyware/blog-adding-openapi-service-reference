using Microsoft.Extensions.Options;
using OpenAPIDemo.Worker;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddOptions<MyOptions>()
            .Configure<IConfiguration>((options, configuration) => configuration.GetSection("Myoptions").Bind(options));
        services.AddHttpClient<ICatFactsClient, CatFactsClient>((sp, client) =>
        {
            var options = sp.GetRequiredService<IOptions<MyOptions>>();

            client.BaseAddress = options.Value.CatFactsBaseUrl;
        });
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
