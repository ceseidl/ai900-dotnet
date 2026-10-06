using Microsoft.ML.Data;

namespace Ai900Ml;

// Uma linha do dataset: Texto e a feature, Positiva e o rotulo.
public class Avaliacao
{
    public string Texto { get; set; } = "";
    public bool Positiva { get; set; }
}

public class Previsao
{
    [ColumnName("PredictedLabel")]
    public bool Positiva { get; set; }
    public float Probability { get; set; }
}
