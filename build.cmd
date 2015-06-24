@echo off
cd %~dp0

echo %PATH%

set PATH=%USERPROFILE%/.dnx/bin;%PATH%

SETLOCAL
SET CACHED_NUGET=%LocalAppData%\NuGet\NuGet.exe

IF EXIST %CACHED_NUGET% goto copynuget
echo Downloading latest version of NuGet.exe...
IF NOT EXIST %LocalAppData%\NuGet md %LocalAppData%\NuGet
@powershell -NoProfile -ExecutionPolicy unrestricted -Command "$ProgressPreference = 'SilentlyContinue'; Invoke-WebRequest 'https://www.nuget.org/nuget.exe' -OutFile '%CACHED_NUGET%'"

:copynuget
IF EXIST .nuget\nuget.exe goto restore
md .nuget
copy %CACHED_NUGET% .nuget\nuget.exe > nul

:restore
.nuget\NuGet.exe install Sake -version 0.2 -o packages -ExcludeVersion

IF "%SKIP_DNX_INSTALL%"=="1" goto run
CALL dnvm upgrade -runtime CLR -arch x86
CALL nvm install default -runtime CoreCLR -arch x86

:run
CALL dnvm use default -runtime CLR -arch x86
packages\Sake\tools\Sake.exe -I build -f makefile.shade %*