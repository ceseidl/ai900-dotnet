English | [Português](README.pt-BR.md)

# ai900-dotnet

> **Quick start**

```bash
dotnet run --project src/Ai900Ml
dotnet run --project src/Ai900Genai
```

Needs only the .NET 10 SDK. No cloud keys, no network services.

Two small C# programs that show, in code, concepts covered by the Microsoft Azure AI Fundamentals exam (AI-900, replaced by AI-901 in 2026): machine learning techniques and generative AI with responsible AI guardrails.

## What it is

- **Ai900Ml** ([ML.NET](https://learn.microsoft.com/dotnet/machine-learning/) 5.0.0), trained and run locally:
  - classification (review sentiment) with a train/validation split and accuracy;
  - regression (price from area and rooms);
  - clustering (K-Means over customers, no labels).
- **Ai900Genai** ([Microsoft.Extensions.AI](https://www.nuget.org/packages/Microsoft.Extensions.AI) 10.10.0): a fake `IChatClient` (no model, no network) behind a `DelegatingChatClient` that masks CPF numbers, blocks a forbidden term and adds an "AI generated" notice.

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)

## How to run

```bash
dotnet run --project src/Ai900Ml
dotnet run --project src/Ai900Genai
```

Expected output (`Ai900Ml`; the accuracy comes from a tiny 24-row dataset and is illustrative):

```
== Classificacao (sentimento) ==
Acuracia (validacao): 75%
"Produto otimo, recomendo" -> positivo (99%)
"Atendimento pessimo e entrega atrasada" -> negativo (2%)

== Regressao ==
Regressao: 100 m2, 3 quartos -> ~410 mil

== Agrupamento (clustering) ==
Agrupamento: cliente A e B em grupos distintos
```

Expected output (`Ai900Genai`):

```
[temp=0.2] Sistema: Responda como suporte tecnico. | Pergunta: Meu CPF ***.***.***-** esta ok? (gerado por IA)
Pedido bloqueado.
```

## Structure

```
ai900-dotnet.slnx
src/
  Ai900Ml/      Avaliacao.cs, Classificacao.cs, Regressao.cs,
                Agrupamento.cs, Program.cs
  Ai900Genai/   ClienteFalso.cs, FiltroDeConteudo.cs, Program.cs
```

Identifiers and messages are kept in Portuguese on purpose, matching the article.

## License

[MIT](LICENSE). Author: Carlos Eduardo Seidl.
