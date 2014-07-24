using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncthingTray
{
    public static class ProcessHelper
    {
        public static bool IsProcessOpen(string name)
        {
            return Process.GetProcesses().Any(clsProcess => clsProcess.ProcessName.Contains(name));
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

        public static void StopProcess(string name)
        {
            foreach (var proc in Process.GetProcessesByName(name))
            {
                proc.Kill();
            }
        }
    }
}
