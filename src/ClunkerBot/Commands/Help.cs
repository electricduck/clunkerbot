using ClunkerBot.Data;

namespace ClunkerBot.Commands
{
    class Help {
        public static string Get()
        {
            return HelpData.GetHelp("help");
        }

        public static string Get(string module)
        {
            module = module.Replace("/", "");
            return HelpData.GetHelp(module);
        }
    }
}