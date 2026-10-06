[English](README.md) | Português

# ai900-dotnet

> **Início rápido**

```bash
dotnet run --project src/Ai900Ml
dotnet run --project src/Ai900Genai
```

Precisa apenas do SDK do .NET 10. Sem chaves de nuvem, sem serviços de rede.

Dois programas C# pequenos que mostram, em código, conceitos cobrados na prova Microsoft Azure AI Fundamentals (AI-900, substituída pela AI-901 em 2026): técnicas de machine learning e IA generativa com proteções de IA responsável.

## O que é

- **Ai900Ml** ([ML.NET](https://learn.microsoft.com/dotnet/machine-learning/) 5.0.0), treinado e executado localmente:
  - classificação (sentimento de avaliações) com divisão treino/validação e acurácia;
  - regressão (preço a partir de área e quartos);
  - agrupamento (K-Means sobre clientes, sem rótulos).
- **Ai900Genai** ([Microsoft.Extensions.AI](https://www.nuget.org/packages/Microsoft.Extensions.AI) 10.10.0): um `IChatClient` falso (sem modelo, sem rede) atrás de um `DelegatingChatClient` que mascara CPF, bloqueia um termo proibido e acrescenta o aviso "gerado por IA".

## Requisitos

- [SDK do .NET 10](https://dotnet.microsoft.com/download)

## Como rodar

```bash
dotnet run --project src/Ai900Ml
dotnet run --project src/Ai900Genai
```

Saída esperada (`Ai900Ml`; a acurácia vem de um dataset minúsculo de 24 linhas e é ilustrativa):

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

Saída esperada (`Ai900Genai`):

```
[temp=0.2] Sistema: Responda como suporte tecnico. | Pergunta: Meu CPF ***.***.***-** esta ok? (gerado por IA)
Pedido bloqueado.
```

## Estrutura

```
ai900-dotnet.slnx
src/
  Ai900Ml/      Avaliacao.cs, Classificacao.cs, Regressao.cs,
                Agrupamento.cs, Program.cs
  Ai900Genai/   ClienteFalso.cs, FiltroDeConteudo.cs, Program.cs
```

Identificadores e mensagens ficam em português de propósito, como no artigo.

## Licença

[MIT](LICENSE). Autor: Carlos Eduardo Seidl.
