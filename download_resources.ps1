$latestInfo = Invoke-WebRequest -Uri https://github.com/yamachu/LibOpenJTalk/releases/latest -MaximumRedirection 0  -ErrorAction SilentlyContinue
$Location = $latestInfo.Headers.Location
$Location -match ".*/tag/(?<tagName>.*?)$"
$latestVersion = $Matches.tagName
"$latestVersion"
$baseUrl = "https://github.com/yamachu/LibOpenJTalk/releases/download/$latestVersion"

Invoke-WebRequest "$baseUrl/libopenjtalk.dylib" -OutFile library\resources\osx\libopenjtalk.dylib
Invoke-WebRequest "$baseUrl/libopenjtalk.so" -OutFile library\resources\linux\libopenjtalk.so
Invoke-WebRequest "$baseUrl/x86_open_jtalk.dll" -OutFile library\resources\win-x86\open_jtalk.dll
Invoke-WebRequest "$baseUrl/x64_open_jtalk.dll" -OutFile library\resources\win-x64\open_jtalk.dll