using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEST
{
    class FlushData
    {
        public static void FlushAll()
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/c MSSQLLocalDB stop";
            process.StartInfo = startInfo;
            process.Start();
            startInfo.Arguments = "/c ProjectsV13 stop";
            process.StartInfo = startInfo;
            process.Start();
            startInfo.Arguments = "/c MSSQLLocalDB d";
            process.StartInfo = startInfo;
            process.Start();
            startInfo.Arguments = "/c ProjectsV13 d";
            process.StartInfo = startInfo;
            process.Start();
            File.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "SAMS DB.mdf"));
            File.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "SAMS DB_log.mdf"));
        }
    }
}
