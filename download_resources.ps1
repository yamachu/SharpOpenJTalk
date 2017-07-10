$latestInfo = Invoke-WebRequest https://api.github.com/repos/yamachu/LibOpenJTalk/releases/latest -Headers @{"Accept"="application/json"}
$json = $latestInfo.Content | ConvertFrom-Json
$latestVersion = $json.tag_name
$baseUrl = "https://github.com/yamachu/LibOpenJTalk/releases/download/$latestVersion"

Invoke-WebRequest "$baseUrl/libopenjtalk.dylib" -OutFile library\resources\osx\libopenjtalk.dylib
Invoke-WebRequest "$baseUrl/libopenjtalk.so" -OutFile library\resources\linux\libopenjtalk.so
#Invoke-WebRequest "$baseUrl/x86_openjtalk.dll" -OutFile library\resources\win-x86\openjtalk.dll
#Invoke-WebRequest "$baseUrl/x64_openjtalk.dll" -OutFile library\resources\win-x64\openjtalk.dll
