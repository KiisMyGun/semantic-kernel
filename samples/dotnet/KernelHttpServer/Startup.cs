using KernelHttpServer.Config;
using KernelHttpServer.Utils;

using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;

[assembly: FunctionsStartup(typeof(KernelHttpServer.Startup))]

namespace KernelHttpServer
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpClient();

            builder.Services.AddSingleton<IKernel>((s) =>
            {
                var kernel = new KernelBuilder().Build();
                kernel.Config.AddOpenAIChatCompletionService("chat", "gpt-3.5-turbo", Env.Var("OPENAI_API_KEY"));
                return kernel;
            });
        }
    }
}
