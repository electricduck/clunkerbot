using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ClunkerBot.Commands
{
    class Shell : CommandsBase
    {
        public static string Run(string command) {
            try {
                var escapedArgs = command
                    .Replace("\"", "\\\"");

                var process = new Process()
                {
                    StartInfo = new ProcessStartInfo{
                        FileName = "/bin/bash",
                        Arguments = $"-c \"{escapedArgs}\"",
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                    }
                };

                process.Start();
                string result = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                return $@"<code>{result}</code>";
            } catch(Exception e) {
                return BuildErrorOutput(e);
            }
        }
    }
}