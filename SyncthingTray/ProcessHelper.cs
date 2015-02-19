using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncthingTray
{
    public static class ProcessHelper
    {
        public static bool IsProcessOpen(string name)
        {
            var process = Process.GetProcesses().FirstOrDefault(clsProcess => clsProcess.ProcessName.Contains(Path.GetFileNameWithoutExtension(name)));
            return process != null && process.MainModule.FileName == name;
        }

        public static Process StartProcess(string path)
        {
            var startInfo = new ProcessStartInfo
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                FileName = path,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                WindowStyle = ProcessWindowStyle.Hidden
            };

            return Process.Start(startInfo);
        }

        public static void StopProcessByName(string name)
        {
            foreach (var proc in Process.GetProcessesByName(name))
            {
                proc.Kill();
            }
        }
    }
}
