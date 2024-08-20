using Celestial.Services;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace Celestial.Kernel;

public class Semmy
{
    private readonly CelestialService _celestial;
    private Microsoft.SemanticKernel.Kernel _kernel;
    private ChatHistory _history;
    private IChatCompletionService _chatCompletionService;
    private OpenAIPromptExecutionSettings _openAiPromptExecutionSettings;

    public Semmy(CelestialService celestial)
    {
        _celestial = celestial;
   
        var builder = Microsoft.SemanticKernel.Kernel.CreateBuilder()
            .AddAzureOpenAIChatCompletion(
                _celestial.Settings.OpenAi.Deployment,
                _celestial.Settings.OpenAi.Url,
                _celestial.Settings.OpenAi.Key
            );

        // builder.Services
        //     .AddLogging(services => services.AddConsole().SetMinimumLevel(LogLevel.Debug));

        _kernel = builder.Build();

        _chatCompletionService = _kernel.GetRequiredService<IChatCompletionService>();

        _history = new ChatHistory();

        _openAiPromptExecutionSettings = new()
        {
            ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions,
        };
    }

    public async Task<string> Chat(string message)
    {
        _history.AddUserMessage(message);

        var result = await _chatCompletionService.GetChatMessageContentAsync(
            _history,
            executionSettings: _openAiPromptExecutionSettings,
            kernel: _kernel
        );

        var content = result.Content ?? string.Empty;

        _history.AddMessage(result.Role, content);
        
        return content;
    }
}