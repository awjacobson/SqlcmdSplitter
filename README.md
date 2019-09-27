# SqlcmdSplitter

SqlcmdSplitter is a .NET Core 3.0 console application for splitting large files (>2GB) created by the SSMS Generate Scripts tool.

## Syntax

```
dotnet SqlcmdSplitter.dll C:\TEST\PATH\FILE.sql
```

The tool will create a series of ~2GB output files in the same folder as the input file.  Each output file will have an incrementing number appended to the file name.  For example

```
C:\TEST\PATH\FILE_1.sql
C:\TEST\PATH\FILE_2.sql
C:\TEST\PATH\FILE_3.sql
```

This tool will maintain the "USE" and "GO" statements across all the output files if they are present in the input.


Documentation on SQLCMD https://docs.microsoft.com/en-us/sql/tools/sqlcmd-utility?view=sql-server-2017
