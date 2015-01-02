<?php

// This script needs to be in a folder under the unity project root
// e.g. unityprojectfolder\buildscripts\thisscript.php

// **Need unity editor in PATH**

$projectPath = dirname(getcwd()); // Gets parent directory full path

$buildNumFile = "nextbuildnumber";
$buildNum = file_get_contents($buildNumFile);

echo "Build Number " . $buildNum . "\n";

file_put_contents($buildNumFile, $buildNum + 1);

$buildPath = '"Builds\\' . $buildNum . '\\WebPlayer"';

exec("taskkill /im Unity.exe");
exec("sleep 1");
exec("Unity -batchmode -nographics -quit -buildWebPlayer " . $buildPath . ' -projectPath "' . $projectPath . '"');

echo "Built to " . $buildPath;

exec("copy " . $buildPath .  " " . $buildPath);
exec("toast \"Build " . $buildNum . " has finished!\"");

echo "Build finished!\n";
