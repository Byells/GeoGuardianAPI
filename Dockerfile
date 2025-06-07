# Use a imagem SDK do .NET para construir a aplicação
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copie o arquivo de projeto e restaure as dependências
COPY GeoGuardianAPI.csproj ./GeoGuardianAPI/

# Navegue para a pasta do seu projeto para restaurar as dependências
WORKDIR /app/GeoGuardianAPI
RUN dotnet restore

# Copie o restante do código-fonte e publique a aplicação
WORKDIR /app
COPY . .
RUN dotnet publish GeoGuardianAPI/GeoGuardianAPI.csproj -c Release -o /app/publish --no-restore

# Use a imagem de tempo de execução do .NET para rodar a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Exponha a porta, embora o Render use a variável de ambiente PORT
EXPOSE 8080 

# Comando para iniciar a aplicação
ENTRYPOINT ["dotnet", "GeoGuardianAPI.dll"]