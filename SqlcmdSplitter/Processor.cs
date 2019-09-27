using System;
using System.Collections.Generic;
using System.IO;

namespace SqlcmdSplitter
{
    public static class Processor
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path">The file to be opened for reading</param>
        /// <returns></returns>
        public static int Process(string path)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));

            StreamWriter outfile = null;

            try
            {
                using (var inFile = File.OpenText(path))
                {
                    string lineContent;
                    long size = 0;
                    var outputfilesformat = GetOutputFilesFormat(path);
                    var outFileCount = 0;
                    IList<string> outHeader = null;
                    bool insertFound = false;
                    while ((lineContent = inFile.ReadLine()) != null)
                    {
                        // read until first insert. header might be USE, GO, SET IDENTITY_INSERT, etc.
                        if (!insertFound)
                        {
                            if (lineContent.StartsWith("INSERT"))
                            {
                                insertFound = true;
                            }
                            else
                            {
                                (outHeader ??= new List<string>()).Add(lineContent);
                                continue;
                            }
                        }

                        size += lineContent.Length + 2;

                        if (size > 943572753)
                        {
                            // close output file. start a new output file
                            outfile.Dispose();
                            outfile = null;
                            size = 0;
                        }

                        if (outfile == null)
                        {
                            var outPath = string.Format(outputfilesformat, outFileCount++);
                            outfile = new StreamWriter(outPath, false, inFile.CurrentEncoding);
                            if (outHeader != null)
                            {
                                foreach(var headerLine in outHeader)
                                {
                                    size += headerLine.Length;
                                    outfile.WriteLine(headerLine);
                                }
                            }
                        }

                        outfile.WriteLine(lineContent);
                    }
                }
            }
            finally
            {
                if (outfile != null)
                {
                    outfile.Dispose();
                }
            }

            return 0;
        }

        public static string GetOutputFilesFormat(string path)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));

            var fileName = Path.GetFileNameWithoutExtension(path);
            var extension = Path.GetExtension(path);
            var directory = Path.GetDirectoryName(path);
            return $"{directory}/{fileName}_{{0}}{extension}";
        }
    }
}
