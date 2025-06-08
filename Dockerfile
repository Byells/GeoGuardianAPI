
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app


COPY GeoGuardian.csproj .  


RUN dotnet restore


COPY . .
RUN dotnet publish GeoGuardian.csproj -c Release -o /app/publish --no-restore 


FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080 


ENTRYPOINT ["dotnet", "GeoGuardian.dll"] 