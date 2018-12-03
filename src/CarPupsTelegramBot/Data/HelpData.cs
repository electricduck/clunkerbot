using System.Collections.Generic;

namespace CarPupsTelegramBot.Data
{
    class HelpData
    {
        public static Dictionary<string, string> HelpDictionary = new Dictionary<string, string>();

        public static string IncorrectFormat = "🚫 <b>Incorrect format. Try again!</b>\r\n";

        public static string Fuelly_Get = @"<code>/getfuelly &lt;1&gt; &lt;[2]&gt;</code>

<code>&lt;1&gt;</code> <b>Fuelly ID</b> - ID from the profile URL of a car. So, for `http://www.fuelly.com/car/peugeot/106/2002/electricduck/713804`, the ID would be `713804` -- the integer at the end
<code>&lt;1&gt;</code> <b>Unit</b> <i>(Optional)</i> - Unit to use (<b>us</b> (US MPG), <b>uk</b> (UK/Imperial MPG) -- defaults to <b>us</b>)";

        public static string JourneyPrice_Calculate = @"<code>/calculatejourneyprice &lt;1&gt; &lt;2&gt; &lt;3&gt;</code>
—
<code>&lt;1&gt;</code> <b>Distance</b> - Distance to cover, with optional unit (<b>mi</b> or <b>km</b> -- defaults to <b>mi</b> with no unit) <i>(e.g. 100mi, 741km, 31)</i>
<code>&lt;2&gt;</code> <b>MPG</b> - MPG you are able to achieve, with optional unit (<b>mpg</b>*; to force US: <b>usmpg</b>; to force UK/imperial: <b>ukmpg</b> or <b>impmpg</b> -- defaults to <b>mpg</b> with no unit) <i>(e.g. 34.5mpg, 60ukmpg, 45)</i>
<code>&lt;3&gt;</code> <b>Fuel price</b> - Price of fuel per unit, with optional unit (<b>/L</b> or <b>/G</b>* -- defaults to <b>/L</b> with no unit), and currency** (<b>£</b>, <b>$**</b>, <b>€</b>, etc.) <i>(e.g. £1.15/L, $2.60/G, 1.30)</i>

<b>*</b> <i>If you use '/G' and/or '$' in the fuel price, this will use US MPG, not imperial MPG. You can force it by using the units described above.</i>
<b>**</b> <i>This makes no difference, but presents your currency in the output message for niceness.</i>";

        public static string Mileage_Guess = @"<code>/guessmileage &lt;1&gt; &lt;2&gt; &lt;3&gt; &lt;[4]&gt;</code>
—
<code>&lt;1&gt;</code> <b>Date Registered</b> - Date of car registration <i>(e.g. 1-Aug-1995)</i>
<code>&lt;2&gt;</code> <b>Last MOT Mileage</b> - The latest MOT mileage <i>(e.g. 89165)</i>
<code>&lt;3&gt;</code> <b>Last MOT Date</b> - The date the latest MOT occured <i>(e.g. 15-Aug-2017)</i>
<code>&lt;4&gt;</code> <b>Date To Calculate To</b> <i>(Optional)</i> - Date to calculate to; reverts to current date by default <i>(e.g. 11-Aug-2020)</i>";

        public static string Plate_Parse = @"<code>/parseplate</code>";

        public static string ZeroToSixty_Calculate = @"<code>/calculate0to60 &lt;1&gt; &lt;2&gt; &lt;3&gt; &lt;4&gt;</code>
—
<code>&lt;1&gt;</code> <b>Power</b> - Vehicle power at the flywheel, with optional unit (<b>hp</b>, <b>ps</b>, or <b>kw</b> -- defaults to <b>hp</b> with no unit) <i>(e.g. 64hp, 123ps, 250kw, 329)</i>
<code>&lt;2&gt;</code> <b>Weight</b> - Curb weight of vehicle, with optional unit (<b>lbs</b>, or <b>kg</b> -- defaults to <b>kg</b> with no unit) <i>(e.g. 850kg, 2100lbs, 1024)</i>
<code>&lt;3&gt;</code> <b>Drive Type</b> - Drive type of vehicle: either <b>FWD</b>, <b>RWD</b>, or <b>AWD</b>
<code>&lt;4&gt;</code> <b>Transmission</b> - Transmission of vehicle: either <b>manual/man</b>, <b>automatic/auto</b>, or <b>dct</b> (Dual-Clutch Semi-Automatic)";

        public static void CompileHelpDictionary()
        {
            HelpDictionary.Add("calculate0to60", ZeroToSixty_Calculate);
            HelpDictionary.Add("calculatejourneyprice", JourneyPrice_Calculate);
            HelpDictionary.Add("getfuelly", Fuelly_Get);
            HelpDictionary.Add("guessmileage", Mileage_Guess);
            HelpDictionary.Add("parseplate", Plate_Parse);
        }

        public static string GetHelp(string command, bool incorrectFormatWarning)
        {
            if(HelpData.HelpDictionary.ContainsKey(command)) {
                string helpText;
                string output;

                HelpData.HelpDictionary.TryGetValue(command, out helpText);

                if(incorrectFormatWarning) {
                    output = IncorrectFormat + helpText;
                } else {
                    output = helpText;
                }

                return output;
            }

            return "";
        }
    }
}
