@echo off

:: -----------------------------------------------------------------------------------------------
::
:: Purpose:
::    Builds the .NET C# Project in the parent folder and deploys it to the Batronix NuGet Feed
:: 
:: Author: 
::    Dipl.-Ing.(FH) Jan Ruhländer <jr@batronix.com> - 2016/10/13 11:36
::
:: Requirements:
::     both tools must be added to the Path environment variable
::     nuget.exe (version 3.4.4.1321 or newer)        https://dist.nuget.org/index.html
::     regex                                          x:\Produkte und Projekte\Software\CLI\
:: -----------------------------------------------------------------------------------------------

:: The SETLOCAL command ensures that I don’t clobber any existing variables after my script exits. 
:: ENABLEEXTENSIONS argument turns on a very helpful feature called command processor extensions
SETLOCAL ENABLEEXTENSIONS

:: set me to the name of this script. useful for printed messages like echo %me%: some message
SET me=%~n0
:: set parent to the directory of this batch script. useful to construct fully quanified paths.
SET parent=%~dp0
IF %parent:~-1%==\ SET parent=%parent:~0,-1%

SET compress="C:\Program Files\7-Zip\7z.exe"

:: set working directory to the directory of this batch file
pushd "%~dp0"

build "%parent%\CoreCli.sln"

mkdir "%parent%\WinRelease"

copy /Y "%parent%\build\bin\Release\build.exe" "%parent%\WinRelease"
copy /Y "%parent%\datetime\bin\Release\datetime.exe" "%parent%\WinRelease"
copy /Y "%parent%\ipinfo\bin\Release\ipinfo.exe" "%parent%\WinRelease"
copy /Y "%parent%\regex\bin\Release\regex.exe" "%parent%\WinRelease"
copy /Y "%parent%\upinfo\bin\Release\upinfo.exe" "%parent%\WinRelease"

"%PROGRAMFILES%\7-Zip\7z.exe" a "%parent%\CoreCli-1.2.0.Windows.7z" "%parent%\WinRelease\*"
:: restore previous working directory
popd