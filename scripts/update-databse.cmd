@echo off
pushd %0\..\..
dotnet restore
dotnet ef database update -p ./Gamayun.Infrastucture -s ./Gamayun.UI
popd
set /p DUMMY=Hit ENTER to continue...