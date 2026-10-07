// Práctica 2 - Alumno A017 (entrega ficticia para la formación)
// Enunciado: calcular la nota final con prácticas 30 %, parcial 20 % y examen 50 %.
// Nota: este archivo NO forma parte de la solución (no se compila).

public class Practica
{
    public static double nota(double p, double pa, double e)
    {
        double n = p * 0.3 + pa * 0.2 + e * 0.5;
        if (e < 4)
            n = 4;
        return n;
    }

    public static double nota2(double p, double pa, double e)
    {
        double n = p * 0.3 + pa * 0.2 + e * 0.5;
        if (e < 4)
            n = 4;
        return Math.Round(n);
    }

    public static void Main()
    {
        Console.WriteLine(nota(8, 6, 7));
        Console.WriteLine(nota(10, 10, 2));
    }
}
