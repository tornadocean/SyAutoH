call "%VS100COMNTOOLS%vsvars32.bat"

sc stop McsLogService
sc delete McsLogService

call _build_Log build Debug
call _build_MCS build Debug

call "../bin/Debug/McsLogService.exe" /service
sc config McsLogService start= auto
sc start McsLogService

pause