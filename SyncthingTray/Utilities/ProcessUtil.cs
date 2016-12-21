using System.Diagnostics;
using System.IO;
using System.Linq;

namespace SyncthingTray.Utilities
{
    public static class ProcessUtil
    {
        public static bool IsProcessOpen(string name)
        {
            var fileName = Path.GetFileNameWithoutExtension(name);
            if (string.IsNullOrEmpty(fileName)) return false;

            var process = Process.GetProcesses().FirstOrDefault(clsProcess => clsProcess.ProcessName.Contains(fileName));
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
