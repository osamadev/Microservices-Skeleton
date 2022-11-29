FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /dotnetapp
COPY ./bin/docker .
ENV ASPNETCore_URLS http://*:8081
ENV ASPNETCore_ENVIRONMENT docker
EXPOSE 8081
EXPOSE 7001
EXPOSE 7002
EXPOSE 7007
ENTRYPOINT ["dotnet", "Actio.Api.dll"]