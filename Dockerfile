# Stage 1: Build du frontend React
FROM node:18-alpine AS frontend-build
WORKDIR /app
# Copier les fichiers package.json et package-lock.json (ou yarn.lock) puisinstaller
COPY gorapulv-frontend/package*.json ./
RUN npm install
# Copier le reste des sources frontend et lancer le build
COPY gorapulv-frontend/ ./
RUN npm run build
# Stage 2: Build du backend .NET
FROM mcr.microsoft.com/dotnet/sdk:9.0-preview AS backend-build
WORKDIR /src
# Copier les fichiers .csproj et solution, puis restaurer les dépendances
COPY Gorapulv.sln ./
COPY Gorapulv.Api/Gorapulv.Api.csproj ./Gorapulv.Api/
COPY Gorapulv.Tests/Gorapulv.Tests.csproj ./Gorapulv.Tests/
RUN dotnet restore
# Copier tous les fichiers source et compiler/publier en Release
COPY . .
RUN dotnet publish -c Release -o /app
# Copier les fichiers statiques du frontend buildé dans le dossier wwwroot du publish
RUN mkdir -p /app/wwwroot && rm -rf /app/wwwroot/*
COPY --from=frontend-build /app/dist /app/wwwroot
# Stage 3: Image finale d'exécution
FROM mcr.microsoft.com/dotnet/aspnet:9.0-preview AS runtime
WORKDIR /app
# Copier les fichiers publiés du stage build
COPY --from=backend-build /app ./
# Indiquer l'environnement et le port
ENV ASPNETCORE_ENVIRONMENT=Production \
ASPNETCORE_URLS=http://+:80
EXPOSE 80
ENTRYPOINT ["dotnet", "Gorapulv.Api.dll"]