# Hook PreToolUse: se ejecuta ANTES de cada herramienta del agente.
# Recibe por la entrada estándar un JSON con tool_name y tool_input.
# Política del equipo:
#   1. El agente nunca hace git push: eso lo decide una persona.
#   2. El agente nunca modifica los propios hooks.
$entrada = [Console]::In.ReadToEnd()

function Denegar([string]$motivo) {
  $salida = @{ hookSpecificOutput = @{ hookEventName = "PreToolUse"; permissionDecision = "deny"; permissionDecisionReason = $motivo } }
  $salida | ConvertTo-Json -Depth 5 -Compress
  exit 0
}

if ($entrada -match 'git\s+push') {
  Denegar "Politica del equipo: el push lo hace una persona, nunca el agente."
}

if (($entrada -match '"tool_name"\s*:\s*"[^"]*(edit|create|write|replace)[^"]*"') -and ($entrada -match '\.github[\\/]+hooks')) {
  Denegar "Los hooks del equipo no se modifican desde el agente."
}

# Sin decisión: sigue el flujo normal de aprobaciones de VS Code.
'{}'
