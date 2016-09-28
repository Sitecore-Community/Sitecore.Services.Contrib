@echo Off
cd %~dp0

SETLOCAL

set target=%1
if "%target%" == "" (
   set target=Full
)
set config=%2
if "%config%" == "" (
   set config=Debug
)

REM Set SHA from latest GIT commit if not already defined by build process
set ShaFile=sha.txt

if "%SHA%" == "" (
   git rev-parse HEAD > %ShaFile%
   for /f "delims=" %%a in (%ShaFile%) do set SHA=%%a
)

nuget restore src\Build\packages.config -PackagesDirectory packages -ConfigFile src\build\nuget\NuGet.Config

msbuild Build\Build.proj /t:"%target%" /p:Configuration="%config%" /fl /flp:LogFile=msbuild.log;Verbosity=Detailed /nr:false

REM Remove SHA file
if EXIST %ShaFile% del /F %ShaFile%
if EXIST %ShaFile% exit 1

REM Abandon versioning changes made by the command line build process
git checkout src\Common\CommonVersionInfo.cs