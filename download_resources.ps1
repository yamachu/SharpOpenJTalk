$latestInfo = [System.Net.HttpWebRequest]::Create('https://github.com/yamachu/LibOpenJTalk/releases/latest');
$Location = $latestInfo.GetResponse().ResponseUri.AbsoluteUri
$Location -match ".*/tag/(?<tagName>.*?)$"
$latestVersion = $Matches.tagName
"$latestVersion"
$baseUrl = "https://github.com/yamachu/LibOpenJTalk/releases/download/$latestVersion"

# Full
Invoke-WebRequest "$baseUrl/x64-libopenjtalk.dylib" -OutFile library\full\resources\osx\x64-libopenjtalk.dylib
Invoke-WebRequest "$baseUrl/arm64-libopenjtalk.dylib" -OutFile library\full\resources\osx\arm64-libopenjtalk.dylib
Invoke-WebRequest "$baseUrl/x64-libopenjtalk.so" -OutFile library\full\resources\linux\x64-libopenjtalk.so
Invoke-WebRequest "$baseUrl/arm64-libopenjtalk.so" -OutFile library\full\resources\linux\arm64-libopenjtalk.so
# Invoke-WebRequest "$baseUrl/aarch64-linux-android-libopenjtalk.so" -OutFile library\full\resources\android\aarch64-linux-android-libopenjtalk.so
# Invoke-WebRequest "$baseUrl/armv7a-linux-androideabi-libopenjtalk.so" -OutFile library\full\resources\android\armv7a-linux-androideabi-libopenjtalk.so
# Invoke-WebRequest "$baseUrl/i686-linux-android-libopenjtalk.so" -OutFile library\full\resources\android\i686-linux-android-libopenjtalk.so
# Invoke-WebRequest "$baseUrl/x86_64-linux-android-libopenjtalk.so" -OutFile library\full\resources\android\x86_64-linux-android-libopenjtalk.so
Invoke-WebRequest "$baseUrl/x86_open_jtalk.dll" -OutFile library\full\resources\win-x86\openjtalk.dll
Invoke-WebRequest "$baseUrl/x64_open_jtalk.dll" -OutFile library\full\resources\win-x64\openjtalk.dll
Invoke-WebRequest "$baseUrl/wasm-libopenjtalk.a.2.0.23" -OutFile library\full\resources\browser-wasm\2.0.23\openjtalk.a
Invoke-WebRequest "$baseUrl/wasm-libopenjtalk.a.3.1.12" -OutFile library\full\resources\browser-wasm\3.1.12\openjtalk.a
Invoke-WebRequest "$baseUrl/wasm-libopenjtalk.a.3.1.34" -OutFile library\full\resources\browser-wasm\3.1.34\openjtalk.a
Invoke-WebRequest "$baseUrl/wasm-libopenjtalk.a.3.1.56" -OutFile library\full\resources\browser-wasm\3.1.56\openjtalk.a

# Lang
Invoke-WebRequest "$baseUrl/x64-libopenjtalk-lang.dylib" -OutFile library\lang\resources\osx\x64-libopenjtalk-lang.dylib
Invoke-WebRequest "$baseUrl/arm64-libopenjtalk-lang.dylib" -OutFile library\lang\resources\osx\arm64-libopenjtalk-lang.dylib
Invoke-WebRequest "$baseUrl/x64-libopenjtalk-lang.so" -OutFile library\lang\resources\linux\x64-libopenjtalk-lang.so
Invoke-WebRequest "$baseUrl/arm64-libopenjtalk-lang.so" -OutFile library\lang\resources\linux\arm64-libopenjtalk-lang.so
# Invoke-WebRequest "$baseUrl/aarch64-linux-android-libopenjtalk-lang.so" -OutFile library\lang\resources\android\aarch64-linux-android-libopenjtalk-lang.so
# Invoke-WebRequest "$baseUrl/armv7a-linux-androideabi-libopenjtalk-lang.so" -OutFile library\lang\resources\android\armv7a-linux-androideabi-libopenjtalk-lang.so
# Invoke-WebRequest "$baseUrl/i686-linux-android-libopenjtalk-lang.so" -OutFile library\lang\resources\android\i686-linux-android-libopenjtalk-lang.so
# Invoke-WebRequest "$baseUrl/x86_64-linux-android-libopenjtalk-lang.so" -OutFile library\lang\resources\android\x86_64-linux-android-libopenjtalk-lang.so
Invoke-WebRequest "$baseUrl/x86_open_jtalk_lang.dll" -OutFile library\lang\resources\win-x86\openjtalk-lang.dll
Invoke-WebRequest "$baseUrl/x64_open_jtalk_lang.dll" -OutFile library\lang\resources\win-x64\openjtalk-lang.dll
Invoke-WebRequest "$baseUrl/wasm-libopenjtalk-lang.a.2.0.23" -OutFile library\lang\resources\browser-wasm\2.0.23\openjtalk-lang.a
Invoke-WebRequest "$baseUrl/wasm-libopenjtalk-lang.a.3.1.12" -OutFile library\lang\resources\browser-wasm\3.1.12\openjtalk-lang.a
Invoke-WebRequest "$baseUrl/wasm-libopenjtalk-lang.a.3.1.34" -OutFile library\lang\resources\browser-wasm\3.1.34\openjtalk-lang.a
Invoke-WebRequest "$baseUrl/wasm-libopenjtalk-lang.a.3.1.56" -OutFile library\lang\resources\browser-wasm\3.1.56\openjtalk-lang.a
