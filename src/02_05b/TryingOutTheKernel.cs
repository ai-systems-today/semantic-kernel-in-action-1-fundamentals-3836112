using Microsoft.SemanticKernel;
using Microsoft.Extensions.Configuration;

namespace _02_05b;

public class TryingOutTheKernel
{

    public static async Task Execute()
    {
        var config = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .AddUserSecrets<Program>() // requires Microsoft.Extensions.Configuration.UserSecrets
            .Build();

        var modelDeploymentName = "gpt-4o";
        var azureOpenAiEndpoint = Environment.GetEnvironmentVariable("AZUREOPENAI_ENDPOINT") 
                                   ?? throw new InvalidOperationException("AZUREOPENAI_ENDPOINT environment variable is not set.");
        var azureOpenAiKey = Environment.GetEnvironmentVariable("AZUREOPENAI_APIKEY") 
                             ?? throw new InvalidOperationException("AZUREOPENAI_APIKEY environment variable is not set.");

        var builder = Kernel.CreateBuilder();
        builder.Services.AddAzureOpenAIChatCompletion(
            modelDeploymentName,
            azureOpenAiEndpoint,
            azureOpenAiKey
        );
        var kernel = builder.Build();

        var topic = "Explain the theory of relativity in simple terms.";
        var prompt = @$"Generate a funny poem about the following topic:{topic}";

        var poemResult = await kernel.InvokePromptAsync(prompt);
        Console.WriteLine(poemResult);


    }
}