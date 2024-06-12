# pack existing release build
cd $PSScriptRoot
$packCmd = 'dotnet pack $Name --no-build --output ./Release --configuration Release'
$projList = 'Fonlow.TypeScriptCodeDomCore/Fonlow.TypeScriptCodeDomCore.csproj'
foreach($name in $projList){
    Invoke-Expression $ExecutionContext.InvokeCommand.ExpandString($packCmd)
}