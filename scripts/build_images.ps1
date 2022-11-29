# $ApiProjectPath = "../src/Actio.Api/Actio.Api.csproj";
# $ActivityServiceProjectPath = "../src/Actio.Services.Activities/Actio.Services.Activities.csproj";
# $IdentityServiceProjectPath = "../src/Actio.Services.Identity/Actio.Services.Identity.csproj";
# $ApiProjOutputPath = "../src/Actio.Api/bin/docker";
# $ActivityServiceProjOutputPath = "../src/Actio.Services.Activities/bin/docker";
# $IdentityServiceProjOutputPath = "../src/Actio.Services.Identity/bin/docker";
# $ApiProjectBasePath = "../src/Actio.Api/";
# $ActivityServiceProjectBasePath = "../src/Actio.Services.Activities/";
# $IdentityServiceProjectBasePath = "../src/Actio.Services.Identity/";

# $ApiProjPath = $(Resolve-Path -Path $ApiProjectBasePath);
# $ActivityServiceProjPath = $(Resolve-Path -Path $ActivityServiceProjectBasePath);
# $IdentityServiceProjPath = $(Resolve-Path -Path $IdentityServiceProjectBasePath);

# $configMode = "release";

# dotnet publish $ApiProjectPath --configuration $configMode --output $ApiProjOutputPath;

# dotnet publish $ActivityServiceProjectPath --configuration $configMode --output $ActivityServiceProjOutputPath;

# dotnet publish $IdentityServiceProjectPath --configuration $configMode --output $IdentityServiceProjOutputPath;

# docker build $ApiProjPath -t actio-api:latest;

# docker build $ActivityServiceProjPath -t actio-activity-svc:latest;

# docker build $IdentityServiceProjPath -t actio-identity-svc:latest;

docker-compose up -d rabbitmq;

docker-compose up -d;