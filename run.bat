@echo off
set arg1=%1

if "%arg1%" == "" (
    .\bin\Debug\net6.0\RTCSharpEdition.exe
)
if "%arg1%" == "d" (
    .\bin\Debug\net6.0\RTCSharpEdition.exe
)
if "%arg1%" == "r" (
    .\bin\Release\net6.0\RTCSharpEdition.exe
)