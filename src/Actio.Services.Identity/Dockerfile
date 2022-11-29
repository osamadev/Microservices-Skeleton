FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /dotnetapp
COPY ./bin/docker .
ENV ASPNETCore_URLS http://*:8083
ENV ASPNETCore_ENVIRONMENT docker
EXPOSE 8083
EXPOSE 7001
EXPOSE 7002
EXPOSE 7007
ENTRYPOINT ["dotnet", "Actio.Services.Identity.dll"]