# Use a imagem base do SDK .NET 8
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copie o csproj e restaure as dependências
COPY *.csproj ./
RUN dotnet restore

# Copie o resto dos arquivos e construa o projeto
COPY . ./
RUN dotnet publish -c Release -o out

# Gere a imagem de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Financeiro_backend"]
