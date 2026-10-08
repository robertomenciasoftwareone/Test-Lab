using System.Globalization;

namespace Nebrija.Notas;

/// <summary>Representa a un alumno y sus notas en las tres partes de la asignatura.</summary>
/// <param name="Id">Identificador del alumno.</param>
/// <param name="Practicas">Nota de prácticas.</param>
/// <param name="Parcial">Nota del examen parcial.</param>
/// <param name="Examen">Nota del examen final.</param>
public record Alumno(string Id, decimal Practicas, decimal Parcial, decimal Examen);

/// <summary>Representa el informe individual de un alumno.</summary>
/// <param name="Id">Identificador del alumno.</param>
/// <param name="Practicas">Nota de prácticas.</param>
/// <param name="Parcial">Nota del examen parcial.</param>
/// <param name="Examen">Nota del examen final.</param>
/// <param name="Nota">Nota final calculada.</param>
/// <param name="Calificacion">Texto de calificación, que puede incluir Matrícula de Honor.</param>
public record InformeAlumno(
    string Id,
    decimal Practicas,
    decimal Parcial,
    decimal Examen,
    decimal Nota,
    string Calificacion);

/// <summary>Lee los datos del alumnado y genera el informe de calificaciones de la asignatura.</summary>
public static class Informe
{
    private const int AlumnosPorMatriculaHonor = 20;

    /// <summary>Lee los alumnos de un CSV con columnas id, prácticas, parcial y examen.</summary>
    /// <remarks>
    /// Se omite la primera línea como encabezado y se ignoran las líneas vacías.
    /// Las notas deben usar el punto como separador decimal.
    /// </remarks>
    /// <param name="ruta">Ruta del archivo CSV que contiene los datos.</param>
    /// <returns>Los alumnos leídos, en el mismo orden que aparecen en el archivo.</returns>
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

    /// <summary>Calcula la nota final de cada alumno y crea las filas del informe.</summary>
    /// <remarks>
    /// Asigna como máximo una Matrícula de Honor por cada 20 alumnos o fracción,
    /// entre quienes cumplen la nota mínima. Prioriza la nota final y desempata
    /// por la nota del examen; las filas se devuelven en el orden original.
    /// </remarks>
    /// <param name="alumnos">Alumnos cuyas calificaciones se incluirán en el informe.</param>
    /// <returns>Las filas del informe con la nota final y su calificación correspondiente.</returns>
    public static List<InformeAlumno> Generar(IEnumerable<Alumno> alumnos)
    {
        var resultados = alumnos
            .Select(alumno => new
            {
                Alumno = alumno,
                Nota = CalculadoraNotas.CalcularNotaFinal(alumno.Practicas, alumno.Parcial, alumno.Examen)
            })
            .ToList();

        var limiteMatriculas = (resultados.Count + AlumnosPorMatriculaHonor - 1) / AlumnosPorMatriculaHonor;
        var indicesConMatricula = resultados
            .Select((resultado, indice) => new { resultado, indice })
            .Where(item => CalculadoraNotas.PermiteMatriculaHonor(item.resultado.Nota))
            .OrderByDescending(item => item.resultado.Nota)
            .ThenByDescending(item => item.resultado.Alumno.Examen)
            .Take(limiteMatriculas)
            .Select(item => item.indice)
            .ToHashSet();

        return resultados
            .Select((resultado, indice) => new InformeAlumno(
                resultado.Alumno.Id,
                resultado.Alumno.Practicas,
                resultado.Alumno.Parcial,
                resultado.Alumno.Examen,
                resultado.Nota,
                indicesConMatricula.Contains(indice) ? "Matrícula de Honor" : CalculadoraNotas.Calificacion(resultado.Nota)))
            .ToList();
    }
}
