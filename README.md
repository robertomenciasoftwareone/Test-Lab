# Nebrija.Notas

Proyecto de ejemplo para la formación de GitHub Copilot. Las reglas de cálculo y los datos son ficticios.

- `src/Nebrija.Notas`: cálculo de la nota final (`CalculadoraNotas.cs`) e informe a partir de `data/alumnos.csv`.
- `tests/Nebrija.Notas.Tests`: tests con xUnit (`dotnet test`).
- `docs/requisito-matricula-honor.md`: requisito para la demo multiagente.
- `practicas/EntregaA017.cs`: entrega ficticia de un alumno (no se compila).
- `.github/`: agentes, archivo de prompt y skill de la formación.

Ejecutar el informe (desde esta carpeta): `dotnet run --project src/Nebrija.Notas`
