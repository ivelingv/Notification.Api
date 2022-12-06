FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["Giftshop.Notifications/Giftshop.Notifications.csproj", "Giftshop.Notifications/"]
COPY ["Giftshop.Core/Giftshop.Core.csproj", "Giftshop.Core/"]
RUN dotnet restore "Giftshop.Notifications/Giftshop.Notifications.csproj"
COPY . .
WORKDIR "/src/Giftshop.Notifications"
RUN dotnet build "Giftshop.Notifications.csproj" -c Debug -o /app

FROM build AS publish
RUN dotnet publish "Giftshop.Notifications.csproj" -c Debug -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Giftshop.Notifications.dll"]