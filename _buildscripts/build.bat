@ECHO OFF
echo Building players...
php build.php
cd ..
echo Starting editor...
start Unity -buildTarget Standalone -projectPath "%CD%"
pause
