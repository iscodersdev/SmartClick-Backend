#!/bin/bash
# entrypoint.sh
# 1. Iniciar la aplicación
echo "Iniciando la aplicación SmartClickCore..."
exec dotnet SmartClickCore.dll
