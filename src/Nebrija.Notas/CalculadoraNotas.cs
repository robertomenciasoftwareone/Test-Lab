namespace Nebrija.Notas;

/// <summary>
/// Cálculo de la nota final de una asignatura de ejemplo.
/// Reglas ficticias, solo para la formación:
/// - Prácticas 30 %, parcial 20 %, examen final 50 %.
/// - Si el examen final es inferior a 4, la nota final es como máximo 4.
/// - La nota final se redondea a un decimal.
/// </summary>
public static class CalculadoraNotas
{
    public const decimal PesoPracticas = 0.3m;
    public const decimal PesoParcial = 0.2m;
    public const decimal PesoExamen = 0.5m;
    public const decimal NotaMinimaExamen = 4.0m;

    /// <summary>
    /// Calcula la nota final ponderada y la redondea a un decimal.
    /// Si el examen es inferior a 4, la nota final no puede superar el 4.
    /// </summary>
    /// <param name="practicas">Nota de prácticas, que cuenta un 30 %.</param>
    /// <param name="parcial">Nota del examen parcial, que cuenta un 20 %.</param>
    /// <param name="examen">Nota del examen final, que cuenta un 50 %.</param>
    /// <returns>La nota final redondeada a un decimal y, si corresponde, limitada a 4.</returns>
    public static decimal CalcularNotaFinal(decimal practicas, decimal parcial, decimal examen)
    {
        var media = practicas * PesoPracticas + parcial * PesoParcial + examen * PesoExamen;
        var redondeada = Math.Round(media, 1, MidpointRounding.AwayFromZero);

        if (examen < NotaMinimaExamen)
        {
            return Math.Min(redondeada, 4.0m);
        }

        return redondeada;
    }

    public static string Calificacion(decimal nota)
    {
        if (nota < 5) return "Suspenso";
        if (nota < 7) return "Aprobado";
        if (nota < 9) return "Notable";
        return "Sobresaliente";
    }

    /// <summary>
    /// Devuelve true si la nota permite matrícula de honor.
    /// </summary>
    public static bool PermiteMatriculaHonor(decimal nota)
    {
        return nota >= 9.0m;
    }
}
