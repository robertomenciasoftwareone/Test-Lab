# Hook PostToolUse: se ejecuta DESPUÉS de cada herramienta del agente.
# Deja un registro de auditoría en logs/registro-agente.log (fuera de git).
$entrada = [Console]::In.ReadToEnd()
$herramienta = "desconocida"
if ($entrada -match '"tool_name"\s*:\s*"([^"]+)"') { $herramienta = $Matches[1] }
New-Item -ItemType Directory -Force -Path logs | Out-Null
Add-Content -Path logs/registro-agente.log -Value ("{0}  {1}" -f (Get-Date -Format "yyyy-MM-dd HH:mm:ss"), $herramienta)
'{}'
