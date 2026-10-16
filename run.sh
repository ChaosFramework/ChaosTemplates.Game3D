#!/bin/bash

# Build and run ChaosTemplates.Game3D and restore/build all required dependencies.

set -euo pipefail

SolutionDir="$(realpath "$(dirname "${BASH_SOURCE[0]}")")/"
cd "$SolutionDir"

tf="net8.0"

bash ./build.sh
SolutionDir="$SolutionDir" bash -c '       \
  dotnet run "./ChaosTemplates.Game3D.sln" \
  --project "./ChaosTemplates.Game3D/"     \
  --framework "'"$tf"'"                    \
  --configuration Release'
