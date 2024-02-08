:: pack existing release build
set packCmd=dotnet pack --no-build --output PublishNuGetPackages --configuration Release
%packCmd% Fonlow.TypeScriptCodeDomCore/Fonlow.TypeScriptCodeDomCore.csproj
