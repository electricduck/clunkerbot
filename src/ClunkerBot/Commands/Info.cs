using System;
using ClunkerBot.Data;

namespace ClunkerBot.Commands
{
    class Info : CommandsBase
    {
        public static string Get() {
            try {
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

                string result = $@"<b>ClunkerBot</b> v{AppVersion.FullVersion}
{Separator}
A bot for Telegram providing handy vehicular utlities. See /help for all commands. Code available on <a href='https://github.com/electricduck/ClunkerBot'>Ducky's GitHub</a>; licensed under <a href='https://mit-license.org/'>the MIT license</a>.
{Separator}
<b>Bot</b>
<subitem-icon>📈 Mem.: <code>{memoryUsage}mb</code></subitem-icon>
️<subitem-icon>⏱ Uptime: <code>{uptime}</code></subitem-icon>
{Separator}
<b>System</b>
<subitem-icon>🖥️ Host: <code>{hostname}</code></subitem-icon>
<subitem-icon>💾 OS: <code>{opsys} {opsysVersion}</code></subitem-icon>
<subitem-icon>⚙️ Env.: <code>{runtime}</code></subitem-icon>
<subitem-icon>🕑 Time: <code>{time}</code></subitem-icon>";

                return BuildOutput(result);
            } catch (Exception e) {
                return BuildErrorOutput(e);
            }
        }
    }
}
