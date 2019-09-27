# SqlcmdSplitter

SqlcmdSplitter is a .NET Core 3.0 console application for splitting large files (>2GB) created by the SSMS Generate Scripts tool.

This tool will maintain the "USE" and "GO" statements across all the output files if they are present in the input.

## Syntax

```
dotnet SqlcmdSplitter.dll C:\TEST\PATH\FILE.sql
```

Documentation on SQLCMD https://docs.microsoft.com/en-us/sql/tools/sqlcmd-utility?view=sql-server-2017
