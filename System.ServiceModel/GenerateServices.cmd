rem
rem You need to run this on a x64 bit windows
rem Just double-click this file to regenerate the service code
rem

set SERVICE=http://localhost:8125/WCF/MonoTests.svc?wsdl


"%PROGRAMFILES(X86)%\Microsoft SDKs\Silverlight\v3.0\Tools\SlSvcUtil.exe" /noConfig /nostdlib /directory:Projects\Silverlight\Services\ %SERVICE% 

if errorlevel 1 pause

"%PROGRAMFILES(X86)%\Microsoft SDKs\Windows\v7.0A\Bin\SvcUtil.exe" /noConfig /async /targetClientVersion:Version35 /directory:Projects\3.5\Services %SERVICE%

if errorlevel 1 pause

"%PROGRAMFILES(X86)%\Microsoft SDKs\Windows\v7.0A\Bin\NETFX 4.0 Tools\SvcUtil.exe" /noConfig /async /targetClientVersion:Version35 /directory:Projects\4.0\Services %SERVICE%

if errorlevel 1 pause

copy Projects\Silverlight\Services\ Projects\MonoTouch\Services\

if errorlevel 1 pause
