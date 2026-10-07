#!/usr/bin/env bash
# Hook PostToolUse: se ejecuta DESPUÉS de cada herramienta del agente.
# Deja un registro de auditoría en logs/registro-agente.log (fuera de git).
entrada=$(cat | tr -d '\n')
herramienta=$(printf '%s' "$entrada" | sed -n 's/.*"tool_name"[[:space:]]*:[[:space:]]*"\([^"]*\)".*/\1/p')
mkdir -p logs
printf '%s  %s\n' "$(date '+%Y-%m-%d %H:%M:%S')" "${herramienta:-desconocida}" >> logs/registro-agente.log
echo '{}'
