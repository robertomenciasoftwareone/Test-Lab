using Nebrija.Notas;

namespace Nebrija.Notas.Tests;

public class CalculadoraNotasTests
{
    [Fact]
    public void MediaPonderada()
    {
        Assert.Equal(7.1m, CalculadoraNotas.CalcularNotaFinal(8m, 6m, 7m));
    }

    [Fact]
    public void ExamenPorDebajoDelMinimo_LimitaA4()
    {
        Assert.Equal(4.0m, CalculadoraNotas.CalcularNotaFinal(10m, 10m, 3m));
    }

    [Fact]
    public void ExamenJustoEnElMinimo_NoLimita()
    {
        // Con un 4 en el examen se aprueba por media: 9*0.3 + 8*0.2 + 4*0.5 = 6.3
        Assert.Equal(6.3m, CalculadoraNotas.CalcularNotaFinal(9m, 8m, 4m));
    }

    [Theory]
    [InlineData(4.9, "Suspenso")]
    [InlineData(5.0, "Aprobado")]
    [InlineData(7.0, "Notable")]
    [InlineData(9.0, "Sobresaliente")]
    public void Calificacion_SegunNota(double nota, string esperado)
    {
        Assert.Equal(esperado, CalculadoraNotas.Calificacion((decimal)nota));
    }
}
