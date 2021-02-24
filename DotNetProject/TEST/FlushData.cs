using System;
using System.IO;

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
            startInfo.Arguments = "/c sqllocaldb stop MSSQLLocalDB";
            process.StartInfo = startInfo;
            process.Start();
            startInfo.Arguments = "/c sqllocaldb stop ProjectsV13 ";
            process.StartInfo = startInfo;
            process.Start();
            startInfo.Arguments = "/c sqllocaldb d MSSQLLocalDB";
            process.StartInfo = startInfo;
            process.Start();
            startInfo.Arguments = "/c sqllocaldb d ProjectsV13";
            process.StartInfo = startInfo;
            process.Start();
            File.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "SAMS DB.mdf"));
            File.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "SAMS DB_log.ldf‬"));
        }
    }
}
