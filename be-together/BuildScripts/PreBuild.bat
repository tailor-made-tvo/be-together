echo #
echo #
echo #
echo ################################################################################
echo # %~0
echo ################################################################################
echo # $(ConfigurationName) = %1
echo # $(ProjectDir)        = %2
echo # $(TargetDir)         = %3
echo # $(TargetName)        = %4
echo # $(ProjectName)       = %5
echo ################################################################################

set ConfigurationName=%~1
set ProjectDir=%~2
set TargetDir=%~3
set TargetName=%~4
set ProjectName=%~5




set WebDir=C:\inetpub\wwwroot\%ProjectName%\

if "%Program6432%" EQU "" (
    set ProgramFilesPath=%ProgramFiles%
    set AppCmdPath=%windir%\system32\inetsrv
) else (
    set ProgramFilesPath=%ProgramW6432%
    set AppCmdPath=%windir%\syswow64\inetsrv
)





echo ProgramFilesPath =  %ProgramFilesPath%
echo AppCmdPath =        %AppCmdPath%




rem ################################################################################
rem #   Website beenden                                                            #
rem ################################################################################
echo EMPTY > %WebDir%web.config
%AppCmdPath%\appcmd.exe stop site "%ProjectName%"


rem ################################################################################
rem #   Website komplett abräumen                                                  #
rem ################################################################################
:delWebPath
pushd %WebDir%
FOR /D %%i IN (*.*) DO RD %%i /S /Q
if %ERRORLEVEL% GTR 0 echo %ERRORLEVEL%

del *.* /Q
if %ERRORLEVEL% GTR 0 echo %ERRORLEVEL%

popd
