FROM mcr.microsoft.com/dotnet/sdk:8.0.419-alpine3.23 AS builder
WORKDIR /source

COPY *.csproj .
RUN dotnet restore

COPY . .
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0.25-alpine3.23 AS runner
WORKDIR /app

COPY --from=builder /app/publish .

EXPOSE 8080

USER app

HEALTHCHECK --interval=30s --timeout=5s --start-period=5s --retries=3 \
    CMD wget --no-verbose --tries=1 --spider http://localhost:8080/healthz || exit 1

ENTRYPOINT ["dotnet", "evoNaplo.dll"]

