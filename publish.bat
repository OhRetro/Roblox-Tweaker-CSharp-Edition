@echo off
set arg1=%1

if "%arg1%" == "" (
    dotnet publish --runtime win-x64 --configuration Debug -p:PublishSingleFile=true -p:PublishTrimmed=true --self-contained true
)
if "%arg1%" == "d" (
    dotnet publish --runtime win-x64 --configuration Debug -p:PublishSingleFile=true -p:PublishTrimmed=true --self-contained true
)
if "%arg1%" == "r" (
    dotnet publish --runtime win-x64 --configuration Release -p:PublishSingleFile=true -p:PublishTrimmed=true --self-contained true
)