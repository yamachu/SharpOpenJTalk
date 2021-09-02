$latestInfo = [System.Net.HttpWebRequest]::Create('https://github.com/yamachu/LibOpenJTalk/releases/latest');
$Location = $latestInfo.GetResponse().ResponseUri.AbsoluteUri
$Location -match ".*/tag/(?<tagName>.*?)$"
$latestVersion = $Matches.tagName
"$latestVersion"
$baseUrl = "https://github.com/yamachu/LibOpenJTalk/releases/download/$latestVersion"

# Full
Invoke-WebRequest "$baseUrl/libopenjtalk.dylib" -OutFile library\full\resources\osx\libopenjtalk.dylib
Invoke-WebRequest "$baseUrl/libopenjtalk.so" -OutFile library\full\resources\linux\libopenjtalk.so
Invoke-WebRequest "$baseUrl/x86_open_jtalk.dll" -OutFile library\full\resources\win-x86\openjtalk.dll
Invoke-WebRequest "$baseUrl/x64_open_jtalk.dll" -OutFile library\full\resources\win-x64\openjtalk.dll

# Lang
Invoke-WebRequest "$baseUrl/libopenjtalk-lang.dylib" -OutFile library\lang\resources\osx\libopenjtalk-lang.dylib
Invoke-WebRequest "$baseUrl/libopenjtalk-lang.so" -OutFile library\lang\resources\linux\libopenjtalk-lang.so
Invoke-WebRequest "$baseUrl/x86_open_jtalk_lang.dll" -OutFile library\lang\resources\win-x86\openjtalk-lang.dll
Invoke-WebRequest "$baseUrl/x64_open_jtalk_lang.dll" -OutFile library\lang\resources\win-x64\openjtalk-lang.dll
