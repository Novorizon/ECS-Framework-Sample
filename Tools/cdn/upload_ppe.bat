@echo off
echo input upload bundld ......
set /p BunldID=
echo upload bundld is %BunldID%.

java -jar gy_cdn_uploader-0.0.1-SNAPSHOT.jar --team_name zoids --remote_path_name Android/ppe --local_path_name ..\..\ServerData\Android\ppe\%BunldID%

pause