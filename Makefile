.PHONY: * #since no targets will produce files, saves us from needing to specify on all https://www.gnu.org/software/make/manual/html_node/Phony-Targets.html

SQL_HOST = host.docker.internal
SQL_PORT = 1433
SQL_USERNAME = sa
SQL_PASSWORD = Localp@55
DB_NAME = DB_NAME

all: deps build run

deps: docker-compose db

docker-compose:
	docker compose -p shared -f shared-compose.yml up -d
	docker compose -p product-deps up -d --force-recreate

db:
	docker build . -f schema.Dockerfile --no-cache --label temp  \
    		--build-arg SQL_HOST=$(SQL_HOST) \
    		--build-arg SQL_PORT=$(SQL_PORT) \
    		--build-arg SQL_USERNAME=$(SQL_USERNAME) \
    		--build-arg SQL_PASSWORD=$(SQL_PASSWORD) \
    		--build-arg DB_NAME=$(DB_NAME)
	docker image prune --filter label=temp --force

build: stop
	-docker image rm product-app 
	@docker build . -f src/Web/Dockerfile -t product-app --no-cache

stop:
	-docker rm -f product-app
 
run: stop
	docker run -d -p 8080:8080 -p 8081:8081 \
		-e ConnectionStrings__DbContext='Data Source=$(SQL_HOST),$(SQL_PORT);Persist Security Info=True;Initial Catalog=$(DB_NAME);User ID=$(SQL_HOST);Password=$(SQL_PORT);TrustServerCertificate=True' \
		--name product-app \
		product-app

clean: stop
	docker compose -p shared down
	docker compose -p product-deps down

test:
	docker build . -f src/Web/Dockerfile --no-cache --label temp --target test \
    		--build-arg SQL_HOST=$(SQL_HOST) \
    		--build-arg SQL_PORT=$(SQL_PORT) \
    		--build-arg SQL_USERNAME=$(SQL_USERNAME) \
    		--build-arg SQL_PASSWORD=$(SQL_PASSWORD) \
    		--build-arg DB_NAME=$(DB_NAME)
	docker image prune --filter label=temp --force

coverage:
	docker build . -f src/Web/Dockerfile --no-cache --label temp --output tests/TestResults --target test-coverage \
    		--build-arg SQL_HOST=$(SQL_HOST) \
    		--build-arg SQL_PORT=$(SQL_PORT) \
    		--build-arg SQL_USERNAME=$(SQL_USERNAME) \
    		--build-arg SQL_PASSWORD=$(SQL_PASSWORD) \
    		--build-arg DB_NAME=$(DB_NAME)
	docker image prune --filter label=temp --force

install:
	-dotnet tool update --global dotnet-ef

migration:
	dotnet ef migrations add $(name) --project .\src\Infrastructure\Infrastructure.csproj --startup-project .\src\Web\Web.csproj

migration-remove:
	dotnet ef migrations remove --project .\src\Infrastructure\Infrastructure.csproj --startup-project .\src\Web\Web.csproj

db-script:
	dotnet ef migrations script --idempotent --project .\src\Infrastructure\Infrastructure.csproj --startup-project .\src\Web\Web.csproj