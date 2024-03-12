pac auth create --name LETSEXERCISE --environment https://pg-dataverse-dev.crm4.dynamics.com/
pac auth list
pac auth select --name LETSEXERCISE

$solutionName = "LetsExercise"
$SolutionFileName = "LetsExercise.zip"
$exportLocation = "..\Solutions"
$managedSolutionFolder = "LetsExercise_managed"
$unmanagedSolutionFolder = "LetsExercise"
$customPageDashboardName = "pg_exercisetabdashboard_c4be7_DocumentUri";
$canvasAppManagedFolder = "$exportLocation\$managedSolutionFolder\CanvasApps\"
$canvasAppUnmanagedFolder = "$exportLocation\$unmanagedSolutionFolder\CanvasApps\"

Write-Output "Creating solution folder if not exists..."
If(!(test-path $exportLocation))
{
		New-Item -ItemType Directory -Force -Path $exportLocation
}

cls;

Write-Output "exporting managed customization file..."

pac solution export --path $exportLocation\$SolutionFileName --name $solutionName --managed true --include general

Write-Output "Extracting customization file and removing downloaded zip archive..."
pac solution unpack `
--zipfile $exportLocation\$SolutionFileName `
--folder $exportLocation\$managedSolutionFolder `
--packagetype Managed `
--allowDelete true `
--map ..\infrastructure\configs\letsexercise-mappings.xml

Write-output "extraction process completed."

Write-Output "Extracting Canvas App file and removing MsApp archive..."
pac canvas unpack `
--msapp "$canvasAppManagedFolder\$customPageDashboardName.msapp"  `
--sources $canvasAppManagedFolder\$customPageDashboardName

Write-Output "Deleting canvas app file..."
Remove-Item "$canvasAppManagedFolder\$customPageDashboardName.msapp"
Write-Output "Operation completed."

Write-Output "Deleting custom control build files..."
Remove-Item "$exportLocation\$managedSolutionFolder\Controls\pg_Pg.LetsExercise.CircleProgressControl\bundle.js"
Remove-Item "$exportLocation\$managedSolutionFolder\Controls\pg_Pg.LetsExercise.CircleProgressControl\styles.css"
Write-Output "Operation completed."

Write-Output "Deleting managed solution's file..."
Remove-Item $exportLocation\$solutionFileName
Write-Output "Operation completed."

Write-Output "exporting unmanaged customization file..."
pac solution export --path $exportLocation\$SolutionFileName --name $solutionName --managed false --include general

Write-Output "Extracting customization file and removing downloaded zip archive..."
pac solution unpack `
--zipfile $exportLocation\$SolutionFileName `
--folder $exportLocation\$unmanagedSolutionFolder `
--packagetype Unmanaged `
--allowDelete true `
--map ..\infrastructure\configs\letsexercise-mappings.xml

Write-output "extraction process completed."

Write-Output "Extracting Canvas App file and removing MsApp archive..."
pac canvas unpack `
--msapp "$canvasAppUnmanagedFolder\$customPageDashboardName.msapp"  `
--sources $canvasAppUnmanagedFolder\$customPageDashboardName

Write-Output "Deleting canvas app file..."
Remove-Item "$canvasAppUnmanagedFolder\$customPageDashboardName.msapp"
Write-Output "Operation completed."

Write-Output "Deleting custom control build files..."
Remove-Item "$exportLocation\$unmanagedSolutionFolder\Controls\pg_Pg.LetsExercise.CircleProgressControl\bundle.js"
Remove-Item "$exportLocation\$unmanagedSolutionFolder\Controls\pg_Pg.LetsExercise.CircleProgressControl\styles.css"
Write-Output "Operation completed."

Write-Output "Deleting managed solution's file..."
Remove-Item $exportLocation\$solutionFileName
Write-Output "Operation completed."



