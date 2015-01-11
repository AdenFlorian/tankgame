<?php

// This script needs to be in a folder under the unity project root
// e.g. unityprojectfolder\buildscripts\thisscript.php

// **Need unity editor in PATH**

$projectPath = dirname(getcwd()); // Gets parent directory full path
$projectName = basename($projectPath);

$buildNumFile = "nextbuildnumber";
$buildNum = file_get_contents($buildNumFile);

echo "Build Number " . $buildNum . "\n";

file_put_contents($buildNumFile, $buildNum + 1);

$buildNumPath = 'Builds/' . $buildNum;
$buildWebPath = $buildNumPath . '/WebPlayer';
$buildWinx86Path = $buildNumPath . '/Windows_x86/' . $projectName . '.exe';

exec("taskkill /im Unity.exe");
exec("sleep 2"); // Give Unity a chance to close
exec('Unity -batchmode -nographics -quit -buildWebPlayer "' . $buildWebPath . '" -buildWindowsPlayer "' . $buildWinx86Path . '" -projectPath "' . $projectPath . '"');

echo "Built to " . $buildNumPath . PHP_EOL;

//exec("copy " . $buildWebPath .  " " . $buildWebPath);
exec("toast \"Build " . $buildNum . " has finished!\"");

echo "Build finished!\n";
