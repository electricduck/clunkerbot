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

                string ownerLink = $"<a href=\"https://t.me/{AppSettings.Config_Owner_Username}\">{AppSettings.Config_Owner_Name}</a>";

                string result = $@"<b>ClunkerBot</b> | {AppVersion.FullVersion} <i>{AppVersion.Release}</i>
{Separator}
A bot for Telegram providing handy vehicular utlities. See /help for all commands. Code available on <a href='https://github.com/electricduck/ClunkerBot'>Ducky's GitHub</a>; licensed under <a href='https://mit-license.org/'>the MIT license</a>. This bot is ran by {ownerLink}.
{Separator}
<h2>Bot</h2>
📈 Memory: <code>{memoryUsage}mb</code>
️⏱ Uptime: <code>{uptime}</code>
{Separator}
<h2>System</h2>
🖥️ Host: <code>{hostname}</code>
💾 OS: <code>{opsys} {opsysVersion}</code>
⚙️ Env.: <code>{runtime}</code>
🕑 Time: <code>{time}</code>";

                return BuildOutput(result);
            } catch (Exception e) {
                return BuildErrorOutput(e);
            }
        }
    }
}
