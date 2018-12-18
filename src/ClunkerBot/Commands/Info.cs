using System;
using ClunkerBot.Data;

namespace ClunkerBot.Commands
{
    class Info
    {
        public static string Get() {
            var thisProcess = System.Diagnostics.Process.GetCurrentProcess();

            DateTime startTime = thisProcess.StartTime;
            TimeSpan timeSinceStart = DateTime.Now.ToUniversalTime().Subtract(startTime.ToUniversalTime());

            string hostname = System.Net.Dns.GetHostName();
            string memoryUsage = Convert.ToDecimal(thisProcess.WorkingSet64 / 1000000).ToString();
            string opsys = "(unknown)";
            string opsysVersion = System.Environment.OSVersion.Version.ToString();
            string runtime = System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription;
            string time = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss zzz");
            string uptime = timeSinceStart.ToString("d'd 'h'h 'm'm 's's'");

            if(System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows)) {
                opsys = "Windows";

                if(System.Environment.OSVersion.Version.Build > 9600) {
                    opsysVersion = "10.0." + System.Environment.OSVersion.Version.Build;
                }
            } else if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.OSX)) {
                opsys = "macOS";
            } else if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Linux)) {
                opsys = "Linux";
            }

            string allInfo = $@"<b>ClunkerBot</b> v{AppVersion.FullVersion}
—
A bot for Telegram providing handy vehicular utlities. See <code>/help</code> for all commands. Code available on <a href='https://github.com/electricduck/ClunkerBot'>Ducky's GitHub</a>; licensed under <a href='https://mit-license.org/'>the MIT license</a>.
—
<b>Bot</b>
📈 Mem.: <code>{memoryUsage}mb</code>
️⏱ Uptime: <code>{uptime}</code>
—
<b>System</b>
🖥️ Host: <code>{hostname}</code>
💾 OS: <code>{opsys} {opsysVersion}</code>
⚙️ Env.: <code>{runtime}</code>
🕑 Time: <code>{time}</code>";

            return allInfo;
        }
    }
}
