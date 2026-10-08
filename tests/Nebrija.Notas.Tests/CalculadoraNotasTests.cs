using Nebrija.Notas;

namespace Nebrija.Notas.Tests;

public class CalculadoraNotasTests
{
    [Theory]
    [InlineData(8.0, 6.0, 7.0, 7.1)]
    [InlineData(8.0, 6.0, 7.08, 7.1)]
    public void MediaPonderada_YRedondeadaAUnDecimal(double practicas, double parcial, double examen, double esperado)
    {
        Assert.Equal(
            (decimal)esperado,
            CalculadoraNotas.CalcularNotaFinal((decimal)practicas, (decimal)parcial, (decimal)examen));
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

    [Fact]
    public void Generar_Con26Alumnos_ConcedeDosMatriculasYDesempataPorExamen()
    {
        var alumnos = Enumerable.Range(1, 23)
            .Select(numero => new Alumno($"A{numero}", 8m, 8m, 8m))
            .Concat(new[]
            {
                new Alumno("CandidatoMenor", 10m, 10m, 8m),
                new Alumno("CandidatoEmpateExamenBajo", 10m, 10m, 9m),
                new Alumno("CandidatoEmpateExamenAlto", 10m, 7.5m, 10m)
            });

        var informe = Informe.Generar(alumnos);

        Assert.Equal(2, informe.Count(fila => fila.Calificacion == "Matrícula de Honor"));
        Assert.Equal("Sobresaliente", informe.Single(fila => fila.Id == "CandidatoMenor").Calificacion);
        Assert.Equal("Matrícula de Honor", informe.Single(fila => fila.Id == "CandidatoEmpateExamenBajo").Calificacion);
        Assert.Equal("Matrícula de Honor", informe.Single(fila => fila.Id == "CandidatoEmpateExamenAlto").Calificacion);
    }

    [Theory]
    [InlineData(20, 1)]
    [InlineData(21, 2)]
    public void Generar_ConcedeUnaMatriculaPorCada20AlumnosOTroPorFraccion(int numeroAlumnos, int matriculasEsperadas)
    {
        var alumnos = Enumerable.Range(1, numeroAlumnos)
            .Select(numero => new Alumno($"A{numero}", 10m, 10m, 10m));

        var informe = Informe.Generar(alumnos);

        Assert.Equal(matriculasEsperadas, informe.Count(fila => fila.Calificacion == "Matrícula de Honor"));
    }
}
