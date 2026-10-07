---
name: rubrica-evaluacion
description: Evalúa el código de una práctica de programación con la rúbrica de la asignatura (corrección, legibilidad, tests y buenas prácticas) y devuelve la nota por criterio y comentarios para el alumno. Úsala cuando pidan corregir, evaluar o puntuar una práctica o un ejercicio.
argument-hint: "archivo de la práctica"
---

# Evaluar una práctica con la rúbrica

1. Lee el código indicado y, si existen, sus tests.
2. Aplica los criterios de [rubrica.md](./rubrica.md). Cada criterio tiene su peso.
3. Compara con las reglas oficiales del enunciado, que están en el comentario de src/Nebrija.Notas/CalculadoraNotas.cs.
4. Responde con:
   - Tabla: criterio, puntuación (0-10), peso, evidencia (archivo y línea).
   - Nota ponderada con un decimal.
   - Tres comentarios para el alumno, en segunda persona y en tono constructivo: lo mejor, lo que debe mejorar primero y una sugerencia concreta.
5. La nota es una propuesta: termina siempre con "Propuesta de nota: la decide el profesor."
