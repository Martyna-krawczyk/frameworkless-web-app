FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS base
WORKDIR /app
# copy csproj and restore as distinct layers
COPY *.sln .
COPY FrameworklessWebApp/*.csproj ./FrameworklessWebApp/
RUN dotnet restore FrameworklessWebApp/FrameworklessWebApp.csproj
# copy everything else
COPY FrameworklessWebApp/. ./FrameworklessWebApp/
WORKDIR /app/FrameworklessWebApp

FROM base as build
#build application as release and save to /app/FrameworklessWebApp/out
RUN dotnet publish /p:DebugType=None /p:DebugSymbols=false -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app
EXPOSE 8080
#configure the new directory containing .dll and runtime image
COPY --from=build /app/FrameworklessWebApp/out ./ 
ENTRYPOINT ["dotnet", "FrameworklessWebApp.dll"]

