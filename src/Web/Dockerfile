FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release

WORKDIR /repo
COPY . .

RUN dotnet restore "src/Web/Web.csproj"
RUN dotnet build "src/Web/Web.csproj" -c $BUILD_CONFIGURATION -o /app/build --no-restore

FROM build as test
ARG SQL_HOST
ARG SQL_PORT
ARG SQL_USERNAME
ARG SQL_PASSWORD
ARG DB_NAME
ENV ConnectionStrings__DbContext="Initial Catalog=$DB_NAME;Server=$SQL_HOST,$SQL_PORT;Persist Security Info=False;User ID=$SQL_USERNAME;Password=$SQL_PASSWORD;MultipleActiveResultSets=False;TrustServerCertificate=True;Connection Timeout=30;"

RUN dotnet test PRODUCT.sln

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "src/Web/Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false --no-restore

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "COMPANY_NAME.PRODUCT.Web.dll"]
