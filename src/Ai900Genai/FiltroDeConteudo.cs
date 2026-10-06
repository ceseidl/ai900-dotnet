using System.Text.RegularExpressions;
using Microsoft.Extensions.AI;

namespace Ai900Genai;

// IA responsavel: privacidade (mascara CPF), seguranca (bloqueia
// termos proibidos) e transparencia (avisa que e IA).
public partial class FiltroDeConteudo(IChatClient inner)
    : DelegatingChatClient(inner)
{
    static readonly string[] Proibidos = ["senha do banco"];

    [GeneratedRegex(@"\d{3}\.\d{3}\.\d{3}-\d{2}")]
    private static partial Regex Cpf();

    public override async Task<ChatResponse> GetResponseAsync(
        IEnumerable<ChatMessage> messages,
        ChatOptions? options = null,
        CancellationToken ct = default)
    {
        var limpas = new List<ChatMessage>();
        foreach (var m in messages)
        {
            var texto = m.Text ?? "";
            if (Proibidos.Any(p => texto.Contains(p,
                    StringComparison.OrdinalIgnoreCase)))
                return new ChatResponse(new ChatMessage(
                    ChatRole.Assistant, "Pedido bloqueado."));
            limpas.Add(new ChatMessage(
                m.Role, Cpf().Replace(texto, "***.***.***-**")));
        }

        var resposta = await base.GetResponseAsync(
            limpas, options, ct);
        var aviso = new ChatMessage(ChatRole.Assistant,
            resposta.Text + " (gerado por IA)");
        return new ChatResponse(aviso);
    }
}
