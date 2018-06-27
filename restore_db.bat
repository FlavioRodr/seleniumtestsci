sqlcmd -U %1 -P %2 -S %3 -Q "exec msdb.dbo.rds_restore_database 
        @restore_db_name='%4', 
        @s3_arn_to_restore_from='%5'"
pause