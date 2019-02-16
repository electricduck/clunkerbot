using System.Collections.Generic;

namespace ClunkerBot.Data
{
    // TODO: Some way of generating this for `list-of-commands.txt`
    class HelpData
    {
        public static Dictionary<string, string> HelpDictionary = new Dictionary<string, string>();

        public static string Help = @"❓ <i>Help</i>
—
To get additional help about the modules below (such as what arguments they accept), type <code>/help &lt;module&gt;</code> (for example, <code>/help calculate0to60</code>). Any modules which require arguments will output their help when no arguments (or the wrong ones) are supplied.

<b>Car Utilities</b>
<code>/calculate0to60</code> - Calculate 0-60mph/0-100kph times
<code>/calculatejourneyprice</code> - Calculate price of a fuel costs for a journey
<code>/findavailableplate</code> - Find custom numberplate from your local office
<code>/getfuelly</code> - Get a summary from a Fuelly profile
<code>/getobdcode</code> - Get symptoms & causes of an OBDII code
<code>/guessmileage</code> - Guess mileage of a vehicle from previous known records
<code>/parseplate</code> - Parse number/license plate and &quot;decode&quot; it

<b>Misc. Utilities</b>
<code>/getweather</code> - Get weather for a location

<b>Meta</b>
<code>/help</code> - This page. Derp
<code>/info</code> - Information about the bot, and its instance";

        public static string AvailablePlate_Find = @"<code>/findavailableplate &lt;1&gt; &lt;[2]&gt;</code>
—
<code>&lt;1&gt;</code> <b>Plate</b> - The plate you wish to request, void of spaces <i>(e.g. AA15BEN, DU68CKY, 93FAR)</i>
<code>&lt;2&gt;</code> <b>Country</b> <i>(Optional)</i> - The <a href='https://en.wikipedia.org/wiki/ISO_3166-1#Current'>ISO 3166-1</a> country code -- defaults to <b>gb</b> (Great Britain) <i>(e.g. gb, fr, nl)</i>*

<b>*</b> <i>Supported: gb.</i>";

        public static string Fuelly_Get = @"<code>/getfuelly &lt;1&gt; &lt;[2]&gt;</code>
—
<code>&lt;1&gt;</code> <b>Fuelly ID</b> - ID from the profile URL of a car. So, for `http://www.fuelly.com/car/peugeot/106/2002/electricduck/713804`, the ID would be `713804` -- the integer at the end
<code>&lt;1&gt;</code> <b>Unit</b> <i>(Optional)</i> - Unit to use (<b>us</b> (US MPG), <b>uk</b> (UK/Imperial MPG) -- defaults to <b>us</b>)";

        public static string JourneyPrice_Calculate = @"<code>/calculatejourneyprice &lt;1&gt; &lt;2&gt; &lt;3&gt;</code>
—
<code>&lt;1&gt;</code> <b>Distance</b> - Distance to cover, with optional unit (<b>mi</b> or <b>km</b> -- defaults to <b>mi</b> with no unit) <i>(e.g. 100mi, 741km, 31)</i>
<code>&lt;2&gt;</code> <b>MPG</b> - MPG you are able to achieve, with optional unit (<b>mpg</b>*; to force US: <b>usmpg</b>; to force UK/imperial: <b>ukmpg</b> or <b>impmpg</b> -- defaults to <b>mpg</b> with no unit) <i>(e.g. 34.5mpg, 60ukmpg, 45)</i>
<code>&lt;3&gt;</code> <b>Fuel price</b> - Price of fuel per unit, with optional unit (<b>/L</b> or <b>/G</b>* -- defaults to <b>/L</b> with no unit), and currency** (<b>£</b>, <b>$*</b>, <b>€</b>, etc.) <i>(e.g. £1.15/L, $2.60/G, 1.30)</i>

<b>*</b> <i>If you use '/G' and/or '$' in the fuel price, this will use US MPG, not imperial MPG. You can force it by using the units described above.</i>
<b>**</b> <i>This makes no difference, but presents your currency in the output message for niceness.</i>";

        public static string Mileage_Guess = @"<code>/guessmileage &lt;1&gt; &lt;2&gt; &lt;3&gt; &lt;[4]&gt;</code>
—
<code>&lt;1&gt;</code> <b>Date Registered</b> - Date of car registration <i>(e.g. 1-Aug-1995)</i>
<code>&lt;2&gt;</code> <b>Last MOT Mileage</b> - The latest MOT mileage <i>(e.g. 89165)</i>
<code>&lt;3&gt;</code> <b>Last MOT Date</b> - The date the latest MOT occured <i>(e.g. 15-Aug-2017)</i>
<code>&lt;4&gt;</code> <b>Date To Calculate To</b> <i>(Optional)</i> - Date to calculate to; reverts to current date by default <i>(e.g. 11-Aug-2020)</i>";

        public static string OBDCode_Get = @"<code>/getobdcode &lt;1&gt;</code>
—
<code>&lt;1&gt;</code> <b>OBDII Code</b> - The OBDII code. <i>(e.g. p0204, P0803, P0420)</i>";
        
        public static string Plate_Parse = @"<code>/parseplate &lt;1&gt; &lt;[2]&gt;</code>
—
<code>&lt;1&gt;</code> <b>Plate</b> - The vehicle's numberplate, void of spaces. Tags/seals/stickers must be separated by a '-' <i>(e.g. N161CWW, 15210, AN-US69)</i>
<code>&lt;2&gt;</code> <b>Country</b> <i>(Optional)</i> - The <a href='https://en.wikipedia.org/wiki/ISO_3166-1#Current'>ISO 3166-1</a> country code. Not supplying a CC will cause the parser to try and guess (you will be prompted to specify the CC when mulitple matches are found). <i>(e.g. gb, fr, nl)</i>*

<b>*</b> <i>Supported: at**, de, fr**, gb/uk, gb-nir**, gg, jp, lt, nl**, ru.</i>
<b>**</b> <i>Some formats not supported yet.</i>";

        public static string Weather_Get = @"<code>/getweather &lt;∞&gt; &lt;[2]&gt;</code>
—
<code>&lt;∞&gt;</code> <b>Location</b> - Location of requested weather, expressed by city name, long/lat, postcode/zipcode, etc. <i>(e.g. new york; london, uk; paris, france)</i>
<code>&lt;2&gt;</code> <b>Full Details?</b> <i>(Optional)</i> - If you want more details, use <b>full</b>.";

        public static string ZeroToSixty_Calculate = @"<code>/calculate0to60 &lt;1&gt; &lt;2&gt; &lt;3&gt; &lt;4&gt; &lt;[5]&gt; &lt;[6]&gt;</code>
—
<code>&lt;1&gt;</code> <b>Power</b> - Vehicle power at the flywheel, with optional unit (<b>hp</b>, <b>ps</b>, or <b>kw</b> -- defaults to <b>hp</b> with no unit) <i>(e.g. 64hp, 123ps, 250kw, 329)</i>
<code>&lt;2&gt;</code> <b>Weight</b> - Curb weight of vehicle, with optional unit (<b>lbs</b>, or <b>kg</b> -- defaults to <b>kg</b> with no unit) <i>(e.g. 850kg, 2100lbs, 1024)</i>
<code>&lt;3&gt;</code> <b>Drive Type</b> - Drive type of vehicle: either <b>FWD</b>, <b>RWD</b>, or <b>AWD</b>
<code>&lt;4&gt;</code> <b>Transmission</b> - Transmission of vehicle: either <b>manual/man</b>, <b>automatic/auto</b>, or <b>dct</b> (Dual-Clutch Semi-Automatic)
<code>&lt;4&gt;</code> <b>Passengers</b> <i>(Optional)</i> - How many passengers are in the vehicle (weighing approx. 85kg each) -- defaults to <b>0</b>, giving dry vehicle weight in the calculation
<code>&lt;5&gt;</code> <b>Fuel Volume</b> <i>(Optional)</i> - How much fuel is in the vehicle, with optional unit (<b>l</b>, or <b>gal</b> -- defaults to <b>l</b> with no unit) -- defaults to <b>0L</b>, giving dry vehicle weight in the calculation <i>(e.g. 50L, 10gal, 75)</i>
<code>&lt;6&gt;</code> <b>Fuel Type</b> <i>(Optional)</i> - The type of fuel in the vehicle: either <b>petrol</b>, or <b>diesel</b>";

        public static void CompileHelpDictionary()
        {
            HelpDictionary.Add("help", Help);

            HelpDictionary.Add("calculate0to60", ZeroToSixty_Calculate);
            HelpDictionary.Add("0to60", ZeroToSixty_Calculate);

            HelpDictionary.Add("calculatejourneyprice", JourneyPrice_Calculate);
            HelpDictionary.Add("journeyprice", JourneyPrice_Calculate);
            HelpDictionary.Add("tripprice", JourneyPrice_Calculate);
            
            HelpDictionary.Add("findavailableplate", AvailablePlate_Find);
            HelpDictionary.Add("findplate", AvailablePlate_Find);

            HelpDictionary.Add("getfuelly", Fuelly_Get);
            HelpDictionary.Add("fuelly", Fuelly_Get);

            HelpDictionary.Add("getobdcode", OBDCode_Get);
            HelpDictionary.Add("getobd", OBDCode_Get);
            HelpDictionary.Add("obd", OBDCode_Get);
            HelpDictionary.Add("getobd2code", OBDCode_Get);
            HelpDictionary.Add("getobd2", OBDCode_Get);
            HelpDictionary.Add("obd2", OBDCode_Get);

            HelpDictionary.Add("getweather", Weather_Get);
            HelpDictionary.Add("weather", Weather_Get);

            HelpDictionary.Add("guessmileage", Mileage_Guess);
            HelpDictionary.Add("mileage", Mileage_Guess);

            HelpDictionary.Add("parseplate", Plate_Parse);
            HelpDictionary.Add("plate", Plate_Parse);
        }

        public static string GetHelp(string command, bool incorrectFormatWarning = false)
        {
            if(HelpData.HelpDictionary.ContainsKey(command)) {
                string helpText;
                string output;

                HelpData.HelpDictionary.TryGetValue(command, out helpText);
                output = helpText;

                return output;
            } else {
                command = command.Replace("/", "");

                return $@"❓ <i>Help</i>
—
<i>No help available for </i><code>/{command}</code><i>.</i>";
            }
        }
    }
}
