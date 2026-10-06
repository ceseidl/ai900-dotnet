using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;

namespace Ai900Ml;

public class Imovel
{
    public float Area { get; set; }
    public float Quartos { get; set; }
    public float Preco { get; set; }
}

public class PrecoPrevisto
{
    [ColumnName("Score")]
    public float Valor { get; set; }
}

public static class Regressao
{
    public static void Executar()
    {
        var ml = new MLContext(seed: 1);
        // Preco = 3 * area + 20 * quartos + 50 (em mil reais)
        var dados = Enumerable.Range(0, 40).Select(i => new Imovel
        {
            Area = 40 + i * 3,
            Quartos = 1 + i % 4,
            Preco = 3 * (40 + i * 3) + 20 * (1 + i % 4) + 50,
        });

        var pipeline = ml.Transforms
            .Concatenate("Features", "Area", "Quartos")
            .Append(ml.Transforms.NormalizeMinMax("Features"))
            .Append(ml.Regression.Trainers.Sdca(
                new SdcaRegressionTrainer.Options
                {
                    LabelColumnName = "Preco",
                    L2Regularization = 0,
                    L1Regularization = 0,
                    MaximumNumberOfIterations = 2000,
                }));
        var modelo = pipeline.Fit(
            ml.Data.LoadFromEnumerable(dados));

        var motor = ml.Model
            .CreatePredictionEngine<Imovel, PrecoPrevisto>(
                modelo);
        var p = motor.Predict(
            new Imovel { Area = 100, Quartos = 3 });
        Console.WriteLine(
            $"Regressao: 100 m2, 3 quartos -> ~{p.Valor:F0} mil");
    }
}
