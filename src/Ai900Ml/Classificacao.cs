using Microsoft.ML;

namespace Ai900Ml;

public static class Classificacao
{
    static readonly (string, bool)[] Dados =
    [
        ("Atendimento excelente, recomendo", true),
        ("Produto otimo, chegou rapido", true),
        ("Adorei, qualidade muito boa", true),
        ("Entrega rapida e embalagem perfeita", true),
        ("Muito bom, superou o esperado", true),
        ("Gostei bastante, vale o preco", true),
        ("Funciona perfeitamente, nota dez", true),
        ("Otimo custo beneficio, recomendo", true),
        ("Suporte atencioso e rapido", true),
        ("Qualidade excelente, adorei", true),
        ("Experiencia perfeita, muito bom", true),
        ("Produto bom e entrega rapida", true),
        ("Pessimo atendimento, nao recomendo", false),
        ("Produto ruim, chegou quebrado", false),
        ("Odiei, qualidade muito ruim", false),
        ("Entrega atrasada e embalagem danificada", false),
        ("Muito ruim, abaixo do esperado", false),
        ("Nao gostei, nao vale o preco", false),
        ("Parou de funcionar, nota zero", false),
        ("Custo alto e qualidade ruim", false),
        ("Suporte demorado e ruim", false),
        ("Atendimento pessimo, odiei", false),
        ("Experiencia ruim, muito atraso", false),
        ("Produto ruim e entrega atrasada", false),
    ];

    public static void Executar()
    {
        var ml = new MLContext(seed: 1);
        var dados = ml.Data.LoadFromEnumerable(
            Dados.Select(d => new Avaliacao
            {
                Texto = d.Item1,
                Positiva = d.Item2,
            }));

        // Treino e validacao: o teste fica fora do treino.
        var divisao = ml.Data.TrainTestSplit(
            dados, testFraction: 0.25, seed: 1);

        var pipeline = ml.Transforms.Text
            .FeaturizeText("Features", nameof(Avaliacao.Texto))
            .Append(ml.BinaryClassification.Trainers
                .SdcaLogisticRegression(
                    nameof(Avaliacao.Positiva)));

        var modelo = pipeline.Fit(divisao.TrainSet);

        var metricas = ml.BinaryClassification.Evaluate(
            modelo.Transform(divisao.TestSet),
            labelColumnName: nameof(Avaliacao.Positiva));
        Console.WriteLine(
            $"Acuracia (validacao): {metricas.Accuracy:P0}");

        var motor = ml.Model
            .CreatePredictionEngine<Avaliacao, Previsao>(modelo);
        foreach (var texto in new[]
        {
            "Produto otimo, recomendo",
            "Atendimento pessimo e entrega atrasada",
        })
        {
            var p = motor.Predict(
                new Avaliacao { Texto = texto });
            var rotulo = p.Positiva ? "positivo" : "negativo";
            Console.WriteLine(
                $"\"{texto}\" -> {rotulo} ({p.Probability:P0})");
        }
    }
}
