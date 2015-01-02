@ECHO OFF
echo Building web player...
php build_web.php
cd ..
echo Starting editor...
start Unity -buildTarget Standalone -projectPath "%CD%"
pause
