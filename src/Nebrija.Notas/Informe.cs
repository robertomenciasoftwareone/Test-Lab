using System.Globalization;

namespace Nebrija.Notas;

public record Alumno(string Id, decimal Practicas, decimal Parcial, decimal Examen);

public record FilaInforme(string Id, decimal Nota, string Calificacion);

/// <summary>Genera el informe de la asignatura a partir de un CSV (id,practicas,parcial,examen).</summary>
public static class Informe
{
    public static List<Alumno> LeerAlumnos(string ruta)
    {
        return File.ReadLines(ruta)
            .Skip(1)
            .Where(linea => !string.IsNullOrWhiteSpace(linea))
            .Select(linea => linea.Split(','))
            .Select(c => new Alumno(
                c[0],
                decimal.Parse(c[1], CultureInfo.InvariantCulture),
                decimal.Parse(c[2], CultureInfo.InvariantCulture),
                decimal.Parse(c[3], CultureInfo.InvariantCulture)))
            .ToList();
    }

    public static List<FilaInforme> Generar(IEnumerable<Alumno> alumnos)
    {
        var filas = new List<FilaInforme>();
        foreach (var a in alumnos)
        {
            var nota = CalculadoraNotas.CalcularNotaFinal(a.Practicas, a.Parcial, a.Examen);
            filas.Add(new FilaInforme(a.Id, nota, CalculadoraNotas.Calificacion(nota)));
        }
        return filas;
    }
}
