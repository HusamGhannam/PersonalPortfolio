#!/bin/bash
set -e

# Use PORT env var if provided by Vercel, otherwise default to 8080
export ASPNETCORE_URLS="http://+:${PORT:-8080}"

exec dotnet PersonalPortfolio.dll
