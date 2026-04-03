@echo off
set "PATH=%PATH%;C:\Program Files (x86)\Microsoft Visual Studio\Installer"
dotnet publish -c:Release -f:net10.0-windows10.0.19041.0 && bin\Release\net10.0\win-x64\publish\NativeAotQuirks.exe
