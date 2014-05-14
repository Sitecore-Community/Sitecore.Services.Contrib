@echo Off

if "%MajorVersion%" == "" (
   set MajorVersion=1
)

if "%MinorVersion%" == "" (
   set MinorVersion=0
)

if "%PatchVersion%" == "" (
   set PatchVersion=0
)

if "%Revision%" == "" (
   set Revision=0
)

set target=%1
if "%target%" == "" (
   set target=Go
)
set config=%2
if "%config%" == "" (
   set config=Debug
)
%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild Build\Build.proj /t:"%target%" /p:Configuration="%config%" /fl /flp:LogFile=msbuild.log;Verbosity=Detailed /nr:false