using System.Globalization;
using Nebrija.Notas;

var ruta = args.Length > 0 ? args[0] : Path.Combine("data", "alumnos.csv");

foreach (var fila in Informe.Generar(Informe.LeerAlumnos(ruta)))
{
    Console.WriteLine($"{fila.Id}  {fila.Nota.ToString("0.0", CultureInfo.InvariantCulture),4}  {fila.Calificacion}");
}
