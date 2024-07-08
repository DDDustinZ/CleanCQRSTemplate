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

RUN dotnet test tests/UnitTests/UnitTests.csproj --results-directory "tests/TestResults" --collect:"XPlat Code Coverage" --settings coverlet.runsettings
RUN dotnet test tests/IntegrationTests/IntegrationTests.csproj --results-directory "tests/TestResults" --collect:"XPlat Code Coverage" --settings coverlet.runsettings
RUN dotnet test tests/FunctionalTests/FunctionalTests.csproj --results-directory "tests/TestResults" --collect:"XPlat Code Coverage" --settings coverlet.runsettings

FROM test as report-generator

RUN dotnet tool install dotnet-reportgenerator-globaltool --tool-path /bin
RUN reportgenerator -reports:"/repo/tests/TestResults/**/coverage.opencover.xml" -targetdir:"tests/TestResults" -reporttypes:"Cobertura;TextSummary"

FROM scratch as test-coverage
COPY --from=report-generator /repo/tests/TestResults/Cobertura.xml .
COPY --from=report-generator /repo/tests/TestResults/Summary.txt .

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "src/Web/Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false --no-restore

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "COMPANY_NAME.PRODUCT.Web.dll"]
