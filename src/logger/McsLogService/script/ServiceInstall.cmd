sc stop McsLogService
sc delete McsLogService
McsLogService.exe /service
sc config McsLogService start= auto
sc start McsLogService
pause