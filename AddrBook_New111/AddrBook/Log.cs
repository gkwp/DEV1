using System;
using System.IO;

namespace AddrBook
{
    class Log
    {
        public static void WriteLine(string name, string e)
        {
            string filename = @"c:\dotnet\" + name + "_log";
            string logtime = DateTime.Now.ToString();

            FileStream aFile = new FileStream(filename, FileMode.Append, FileAccess.Write);

            StreamWriter aWriter = new StreamWriter(aFile, System.Text.Encoding.Default);

            aWriter.WriteLine("[" + logtime + "] ");
            aWriter.WriteLine(e);
            aWriter.Flush();
            aWriter.Close();
        }
    }
}
