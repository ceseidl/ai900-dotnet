using Microsoft.Extensions.AI;

namespace Ai900Genai;

// Substitui o modelo real: mesma interface, resposta fixa.
public class ClienteFalso : IChatClient
{
    public Task<ChatResponse> GetResponseAsync(
        IEnumerable<ChatMessage> messages,
        ChatOptions? options = null,
        CancellationToken ct = default)
    {
        var lista = messages.ToList();
        var sistema = lista
            .FirstOrDefault(m => m.Role == ChatRole.System)?.Text;
        var usuario = lista
            .Last(m => m.Role == ChatRole.User).Text;
        var temp = options?.Temperature ?? 1.0f;

        var texto = $"[temp={temp:F1}] "
            + $"Sistema: {sistema ?? "(nenhum)"} | "
            + $"Pergunta: {usuario}";
        return Task.FromResult(new ChatResponse(
            new ChatMessage(ChatRole.Assistant, texto)));
    }

    public IAsyncEnumerable<ChatResponseUpdate>
        GetStreamingResponseAsync(
            IEnumerable<ChatMessage> messages,
            ChatOptions? options = null,
            CancellationToken ct = default)
        => throw new NotSupportedException();

    public object? GetService(Type t, object? key = null) => null;

    public void Dispose() { }
}
