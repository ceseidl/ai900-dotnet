using Microsoft.ML;
using Microsoft.ML.Data;

namespace Ai900Ml;

public class Cliente
{
    public float Compras { get; set; }
    public float Ticket { get; set; }
}

public class Grupo
{
    [ColumnName("PredictedLabel")]
    public uint Id { get; set; }
}

public static class Agrupamento
{
    public static void Executar()
    {
        var ml = new MLContext(seed: 1);
        // Sem rotulo: o algoritmo descobre os grupos sozinho.
        Cliente[] clientes =
        [
            new() { Compras = 2, Ticket = 30 },
            new() { Compras = 3, Ticket = 35 },
            new() { Compras = 2, Ticket = 28 },
            new() { Compras = 30, Ticket = 400 },
            new() { Compras = 28, Ticket = 380 },
            new() { Compras = 33, Ticket = 420 },
        ];

        var pipeline = ml.Transforms
            .Concatenate("Features", "Compras", "Ticket")
            .Append(ml.Clustering.Trainers.KMeans(
                numberOfClusters: 2));
        var modelo = pipeline.Fit(
            ml.Data.LoadFromEnumerable(clientes));

        var motor = ml.Model
            .CreatePredictionEngine<Cliente, Grupo>(modelo);
        var a = motor.Predict(
            new Cliente { Compras = 3, Ticket = 32 });
        var b = motor.Predict(
            new Cliente { Compras = 31, Ticket = 410 });
        var r = a.Id == b.Id ? "mesmo grupo" : "grupos distintos";
        Console.WriteLine($"Agrupamento: cliente A e B em {r}");
    }
}
