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

rem ################################################################################
rem #   Zeitstempel-String erzeugen                                                #
rem ################################################################################

for /f "Tokens=1" %%i in ('%ProjectDir%\BuildScripts\unixdate +%%Y') do set year=%%i
for /f "Tokens=1" %%i in ('%ProjectDir%\BuildScripts\unixdate +%%m') do set month=%%i
for /f "Tokens=1" %%i in ('%ProjectDir%\BuildScripts\unixdate +%%d') do set day=%%i
for /f "Tokens=1" %%i in ('%ProjectDir%\BuildScripts\unixdate +%%H') do set hour=%%i
for /f "Tokens=1" %%i in ('%ProjectDir%\BuildScripts\unixdate +%%M') do set minute=%%i
for /f "Tokens=1" %%i in ('%ProjectDir%\BuildScripts\unixdate +%%S') do set second=%%i

set ymd=%year%-%month%-%day%
set hms=%hour%-%minute%-%second%
set ymd_hms=%year%-%month%-%day%_%hour%-%minute%-%second%



set BackupDir=D:\Backup\%USERNAME%\%ConfigurationName%\%ymd_hms%\
md %BackupDir%

set WebDir=C:\inetpub\wwwroot\%ProjectName%\

set LogDir=%BackupDir%log\
set LogFile=%LogDir%Compile.log
if not exist "%LogDir%" MD "%LogDir%"

if "%Program6432%" EQU "" (
    set ProgramFilesPath=%ProgramFiles%
    set AppCmdPath=%windir%\system32\inetsrv
) else (
    set ProgramFilesPath=%ProgramW6432%
    set AppCmdPath=%windir%\syswow64\inetsrv
)





echo ProgramFilesPath =  %ProgramFilesPath% >> "%LogFile%"
echo ConfigurationName = %ConfigurationName% >> "%LogFile%"
echo ProjectDir =        %ProjectDir% >> "%LogFile%"
echo TargetDir =         %TargetDir% >> "%LogFile%"
echo TargetName =        %TargetName% >> "%LogFile%"
echo ProjectName =       %ProjectName% >> "%LogFile%"



call :copy_start "%BackupDir%"


rem	echo "Version: %ymd_hms% / '%ConfigurationName%'"
echo "%year%.%month%.%day% %hour%:%minute%:%second%" > "%BackupDir%web\version.txt"

if '%ConfigurationName%' EQU 'Debug' (
    rem	echo Drain Web > %WebDir%Drain.txt

    call robocopy %BackupDir%web\ %WebDir% /MIR /NP /XD log /W:1 /R:1 /LOG+:"%LogFile%"
) else if '%ConfigurationName%' EQU 'Release' (
    rem	echo Drain Web > %WebDir%Drain.txt

    robocopy %BackupDir%web\ %WebDir% /MIR /XD log /W:1 /R:1 /NP /LOG+:"%LogFile%"
)

rem ################################################################################
rem #   Website starten                                                            #
rem ################################################################################
%AppCmdPath%\appcmd start site "%projectName%"  >> "%LogFile%"

"C:\Program Files (x86)\Internet Explorer\iexplore.exe" "http://localhost:88"
goto :EOF


:copy_start
    set DestinationDir=%~1

    if '%~2' equ '' (
        set ConfName=%ConfigurationName%
    ) else (
        set ConfName=%~2
    )
    
    echo --------------------------------------------------------------------------------
    echo copy_start
    echo     Destination =       %DestinationDir%
    echo     ConfName =          %ConfName%

    echo -------------------------------------------------------------------------------- >> "%LogFile%"
    echo copy_start >> "%LogFile%"
    echo     Destination =       %DestinationDir% >> "%LogFile%"
    echo     ConfName =          %ConfName% >> "%LogFile%"


    call :mirror_AllFiles "%ProjectDir%App_Data\"              "%DestinationDir%web\App_Data\"
    call :mirror_AllFiles "%ProjectDir%App_GlobalResources\"   "%DestinationDir%web\App_GlobalResources\"
    call :mirror_AllFiles "%ProjectDir%App_LocalResources\"    "%DestinationDir%web\App_LocalResources\"
    call :mirror_AllFiles "%ProjectDir%Content\"               "%DestinationDir%web\Content\"
    call :mirror_AllFiles "%ProjectDir%NWebsecConfig\"         "%DestinationDir%web\NWebsecConfig\"
    call :mirror_AllFiles "%ProjectDir%Scripts\"               "%DestinationDir%web\Scripts\"
    call :mirror_AllFiles "%ProjectDir%Views\"                 "%DestinationDir%web\Views\"
    
    call :copy_AllFiles "%ProjectDir%bin\"                     "%DestinationDir%web\bin\"

    xcopy "%ProjectDir%*.as?x" "%DestinationDir%web\" >> "%LogFile%"
    xcopy "%ProjectDir%*.html" "%DestinationDir%web\" >> "%LogFile%"
    xcopy "%ProjectDir%web*.config" "%DestinationDir%web\" >> "%LogFile%"

    rem	pushd "%ProjectDir%obj\Release\CSAutoParameterize\transformed\"
    rem	xcopy "*.config" "%DestinationDir%web\" /s >> "%LogFile%"
    rem	popd

rem    call :copy_web "%ProjectDir%"                              "%DestinationDir%Web\"
rem    call :copy_web "%ProjectDir%Views\"                        "%DestinationDir%Web\Views\"

    
    goto :EOF


:copy_web
    set srcdir=%~1
    set dstdir=%~2

    echo --------------------------------------------------------------------------------
    echo copy_web
    echo     SourceDir =         %srcdir%
    echo     DestinationDir =    %dstdir%

    echo -------------------------------------------------------------------------------- >> "%LogFile%"
    echo copy_web >> "%LogFile%"
    echo     SourceDir =         %srcdir% >> "%LogFile%"
    echo     DestinationDir =    %dstdir% >> "%LogFile%"

    if not exist "%dstdir%" md "%dstdir%"  >> "%LogFile%"
rem    del "%dstdir%*.*" /q  >> "%LogFile%"

    pushd "%srcdir%"

    if exist "web.%ConfName%.%USERNAME%.config" (
        echo copy "web.%ConfName%.%USERNAME%.config" "%dstdir%web.config" >> "%LogFile%"
        copy "web.%ConfName%.%USERNAME%.config" "%dstdir%web.config" >> "%LogFile%"
    ) else if exist "web.%USERNAME%.%ConfName%.config" (
        echo copy "web.%USERNAME%.%ConfName%.config" "%dstdir%web.config" >> "%LogFile%"
        copy "web.%USERNAME%.%ConfName%.config" "%dstdir%web.config" >> "%LogFile%"
rem    ) else if exist "web.%ConfName%.config" (
rem        echo copy "web.%ConfName%.config" "%dstdir%web.config" >> "%LogFile%"
rem        copy "web.%ConfName%.config" "%dstdir%web.config" >> "%LogFile%"
    ) else if exist "web.%USERNAME%.config" (
        echo copy "web.%USERNAME%.config" "%dstdir%web.config" >> "%LogFile%"
        copy "web.%USERNAME%.config" "%dstdir%web.config" >> "%LogFile%"
    ) else (
        echo copy "web.config" "%dstdir%web.config" >> "%LogFile%"
        copy "web.config" "%dstdir%web.config" >> "%LogFile%"
    )

    xcopy "*.html" "%dstdir%" >> "%LogFile%"
rem    xcopy "*.css" "%dstdir%"  >> "%LogFile%"
rem    xcopy "*.resx" "%dstdir%" >> "%LogFile%"
    xcopy "*.as?x" "%dstdir%" >> "%LogFile%"
rem    xcopy "*.asp" "%dstdir%"  >> "%LogFile%"
rem    xcopy "*.eot" "%dstdir%"  >> "%LogFile%"
rem    xcopy "*.svg" "%dstdir%"  >> "%LogFile%"
rem    xcopy "*.ttf" "%dstdir%"  >> "%LogFile%"
rem    xcopy "*.woff" "%dstdir%" >> "%LogFile%"
rem    xcopy "*.otf" "%dstdir%"  >> "%LogFile%"
rem    xcopy "*.png" "%dstdir%"  >> "%LogFile%"
rem    xcopy "*.ico" "%dstdir%"  >> "%LogFile%"
rem    xcopy "*.xml" "%dstdir%"  >> "%LogFile%"
rem    xcopy "*.jpg" "%dstdir%"  >> "%LogFile%"
rem    xcopy "*.xsd" "%dstdir%"  >> "%LogFile%"
rem    xcopy "*.js" "%dstdir%"   >> "%LogFile%"
rem    xcopy "*.map" "%dstdir%"  >> "%LogFile%"

    popd
    goto :EOF

:mirror_AllFiles
    echo -------------------------------------------------------------------------------- >> "%LogFile%"
    echo mirror_AllFiles >> "%LogFile%"
    echo     SourceDir =         %~1 >> "%LogFile%"
    echo     DestinationDir =    %~2 >> "%LogFile%"

    if '%ConfigurationName%' EQU 'Debug' (
        robocopy %~1 %~2 /MIR /W:1 /R:5 /NP /XF *.tmp *.cs *.bat *.config compile.txt /XD Debug Release .svn /LOG+:"%LogFile%"
    ) else (
        robocopy %~1 %~2 /MIR /W:1 /R:5 /NP /XF *.tmp *.cs *.bat *.config *.pdb compile.txt /XD Debug Release .svn /LOG+:"%LogFile%"
    )
    goto :EOF

:copy_AllFiles
    echo -------------------------------------------------------------------------------- >> "%LogFile%"
    echo copy_AllFiles >> "%LogFile%"

    echo     SourceDir =         %~1 >> "%LogFile%"
    echo     DestinationDir =    %~2 >> "%LogFile%"

    if '%ConfigurationName%' EQU 'Debug' (
        robocopy %~1 %~2 /S /E /W:1 /R:5 /NP /XF *.tmp *.cs *.bat *.config compile.txt /XD Debug Release .svn /LOG+:"%LogFile%"
    ) else (
        robocopy %~1 %~2 /S /E /W:1 /R:5 /NP /XF *.tmp *.cs *.bat *.config *.pdb compile.txt /XD Debug Release .svn /LOG+:"%LogFile%"
    )
    goto :EOF

:copy_Complete
    echo -------------------------------------------------------------------------------- >> "%LogFile%"
    echo copy_Complete >> "%LogFile%"

    echo     SourceDir =         %~1 >> "%LogFile%"
    echo     DestinationDir =    %~2 >> "%LogFile%"

    robocopy %~1 %~2 /S /E /W:1 /R:5 /NP /XF *.tmp *.user *.gpState *.suo /XD Debug Release obj bin .svn /LOG+:"%LogFile%"
    goto :EOF
