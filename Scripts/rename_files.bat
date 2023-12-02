@echo off
setlocal enabledelayedexpansion

:: Rename all Description.md files to README.md

cd /d "C:\git\adventofcode"

:: Loop through all subdirectories
for /r %%i in (Description.md) do (
    set "parent=%%~dpi"
    set "newname=!parent!README.md"
    echo Renaming: "%%i" to "!newname!"
    ren "%%i" "README.md"
)

echo Renaming complete.
pause
