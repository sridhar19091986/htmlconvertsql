@echo on 

@echo cd G:\htmlconvertsql\SqlCompact\Soccer Score Forecast\Soccer Score Forecast\bin\Release

@echo G:

call "F:\Program Files\Microsoft Visual Studio 10.0\VC\vcvarsall.bat"

@echo A

pause


@echo on 

cd G:\htmlconvertsql\SqlCompact\Soccer Score Forecast\Soccer Score Forecast\bin\Release

G:

call "F:\Program Files\Microsoft Visual Studio 10.0\VC\vcvarsall.bat"

pause 

@echo ping -n 10 127.0.0.1 > nul

@echo G:

sqlmetal.exe  /dbml:SoccerScoreCompact.dbml  SyncSoccerScore.sdf  /namespace:SoccerScore.Compact.Linq

sqlmetal.exe  /dbml:SoccerScoreCompact.dbml  SoccerScoreCompact.sdf   /password:adminadmin123 /namespace:SoccerScore.Compact.Linq

pause


sqlmetal /server:localhost /database:G:\htmlconvertsql\match_analysis_pdm.mdf  /dbml:SoccerScoreServer.dbml  /namespace:Soccer_Score_Forecast.LinqSql


