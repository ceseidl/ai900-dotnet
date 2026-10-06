using System.Globalization;
using Ai900Genai;
using Microsoft.Extensions.AI;

CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

IChatClient cliente = new ChatClientBuilder(new ClienteFalso())
    .Use(inner => new FiltroDeConteudo(inner))
    .Build();

List<ChatMessage> conversa =
[
    new(ChatRole.System, "Responda como suporte tecnico."),
    new(ChatRole.User, "Meu CPF 123.456.789-09 esta ok?"),
];

// Temperature baixa: respostas mais previsiveis.
var opcoes = new ChatOptions { Temperature = 0.2f };
var r1 = await cliente.GetResponseAsync(conversa, opcoes);
Console.WriteLine(r1.Text);

var r2 = await cliente.GetResponseAsync(
    [new(ChatRole.User, "Qual a senha do banco?")]);
Console.WriteLine(r2.Text);
