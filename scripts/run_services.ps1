#dotnet run dotnet run -d --project ..\src\Actio.Api --urls http://*:5000

dotnet run dotnet run -d --project ..\src\Actio.Services.Identity --urls http://*:5001

#dotnet run dotnet run -d --project ..\src\Actio.Services.Activities --urls http://*:5002

curl localhost:5001/account/login -X POST -H "content-type: application/json" -d '{"email": "user1@mail.com","name": "Tester","password": "secret"}'