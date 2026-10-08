using System.Globalization;
using Nebrija.Notas;

var ruta = args.Length > 0 ? args[0] : Path.Combine("data", "alumnos.csv");

Console.WriteLine("Alumno  Prácticas  Parcial  Examen  Nota  Calificación");
foreach (var fila in Informe.Generar(Informe.LeerAlumnos(ruta)))
{
    Console.WriteLine(
        $"{fila.Id,-7}  {fila.Practicas.ToString("0.0", CultureInfo.InvariantCulture),9}" +
        $"{fila.Parcial.ToString("0.0", CultureInfo.InvariantCulture),8}" +
        $"{fila.Examen.ToString("0.0", CultureInfo.InvariantCulture),8}" +
        $"{fila.Nota.ToString("0.0", CultureInfo.InvariantCulture),6}  {fila.Calificacion}");
}
