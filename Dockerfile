FROM mcr.microsoft.com/dotnet/sdk:8.0 AS builder 

WORKDIR /Application
EXPOSE 8080
COPY ./*.sln ./
COPY ./*/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p ./${file%.*}/ && mv $file ./${file%.*}/; done
 
RUN dotnet restore 

COPY .  .  

RUN dotnet test ./Messages.Bll.Tests/Messages.Bll.Tests.csproj

WORKDIR /Application/Messages.Web

RUN dotnet publish -c Release -o output

FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /Application

COPY --from=builder /Application/Messages.Web/output ./

ENTRYPOINT ["dotnet","Messages.Web.dll"]