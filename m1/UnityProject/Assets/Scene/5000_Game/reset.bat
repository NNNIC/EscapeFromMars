cd /d %~dp0

echo OK ?
pause

echo SURE ?
pause

rmdir /s /q doc\base 2>nul
rmdir /s /q sc\base 2>nul
del /f /q sc\*.*
pause