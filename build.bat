@echo off

set PROJECT_PATH=%~dp0
set KEEPASS_PATH=%PROJECT_PATH%Libs\KeePass.exe
set SOURCE_PATH=%PROJECT_PATH%AdvancedConnectPlugin

IF NOT EXIST build (
	mkdir build
)


echo Building PLGX file...
%KEEPASS_PATH% --plgx-prereq-net:2.0 --plgx-prereq-kp:2.28 --plgx-create "%SOURCE_PATH%"

echo Moving PLGX file to build directory...
move /Y AdvancedConnectPlugin.plgx .\build\

echo Done.
pause