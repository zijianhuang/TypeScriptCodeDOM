:: pack existing release build
set packCmd=dotnet pack --no-build --output C:\NugetLocalFeeds --configuration Release
%packCmd% Fonlow.TypeScriptCodeDomCore/Fonlow.TypeScriptCodeDomCore.csproj
