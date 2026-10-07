#!/usr/bin/env bash
# Hook PreToolUse: se ejecuta ANTES de cada herramienta del agente.
# Recibe por la entrada estándar un JSON con tool_name y tool_input.
# Política del equipo:
#   1. El agente nunca hace git push: eso lo decide una persona.
#   2. El agente nunca modifica los propios hooks.
entrada=$(cat)

denegar() {
  printf '{"hookSpecificOutput":{"hookEventName":"PreToolUse","permissionDecision":"deny","permissionDecisionReason":"%s"}}\n' "$1"
  exit 0
}

if printf '%s' "$entrada" | grep -Eq 'git[[:space:]]+push'; then
  denegar "Política del equipo: el push lo hace una persona, nunca el agente."
fi

if printf '%s' "$entrada" | grep -Eq '"tool_name"[[:space:]]*:[[:space:]]*"[^"]*(edit|create|write|replace)[^"]*"' &&
   printf '%s' "$entrada" | grep -Eq '\.github[\\/]+hooks'; then
  denegar "Los hooks del equipo no se modifican desde el agente."
fi

# Sin decisión: sigue el flujo normal de aprobaciones de VS Code.
echo '{}'
