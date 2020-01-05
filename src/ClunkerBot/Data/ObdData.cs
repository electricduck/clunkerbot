using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ClunkerBot.Models;

namespace ClunkerBot.Data
{
    public static class ObdData {
        private static string ObdCodeRegex = @"^(([P|C|B|U]{1})([0-9]{4}))$";

        // SEE: http://www.totalcardiagnostics.com/support/Knowledgebase/Article/View/21/0/complete-list-of-obd-codes-generic-obd2-obdii--manufacturer
        private static Dictionary<int, string> PowertrainObdCodesDictionary = new Dictionary<int, string>()
        {
            {0001, "Fuel Volume Regular Control Circuit"},
            {0002, "Fuel Volume Regular Control Circuit Range/Performance"},
            {0003, "Fuel Volume Regular Control Circuit Low"},
            {0004, "Fuel Volume Regular Control Circuit High"},
            {0005, "Fuel Shutoff Valve Control Circuit"},
            {0006, "Fuel Shutoff Valve Control Circuit Low"},
            {0007, "Fuel Shutoff Valve Control Circuit High"},
            {0008, "Engine Position System Performance (Bank 1)"},
            {0009, "Engine Position System Performance (Bank 1)"},
            {0010, "Intake Camshaft Position Actuator Circuit (Bank 1)"},
            {0011, "Intake Camshaft Position Actuator Timing - Over-Advanced (Bank 1)"},
            {0012, "Intake Camshaft Position Actuator Timing - Over-Retarded (Bank 1)"},

            {0261, "Cylinder 1 Injector Circuit Low"},
            {0262, "Cylinder 1 Injector Circuit High"},
            {0263, "Cylinder 1 Contribution/Balance Fault"},
            {0264, "Cylinder 2 Injector Circuit Low"},
            {0265, "Cylinder 2 Injector Circuit High"},
            {0266, "Cylinder 2 Contribution/Balance Fault"},
            {0267, "Cylinder 3 Injector Circuit Low"},
            {0268, "Cylinder 3 Injector Circuit High"},
            {0269, "Cylinder 3 Contribution/Balance Fault"},
            {0270, "Cylinder 4 Injector Circuit Low"},
            {0271, "Cylinder 4 Injector Circuit High"},
            {0272, "Cylinder 4 Contribution/Balance Fault"},

            {0297, "Vehicle Overspeed Condition"},
            {0298, "Engine Oil Over Temperature"},
            {0299, "Turbo/Supercharger Underboost"},

            {1441, "Evaporative Emission Control (EVAP) System Leak"},

            {1918, "Transmission Range Display Circuit Malfunction"}
        };

        public static ObdDetailsModel GetObd(string code)
        {
            code = code.ToUpper();

            ObdDetailsModel returnModel = new ObdDetailsModel();

            Regex regex = new Regex(ObdCodeRegex);
            Match match = regex.Match(code);

            // TODO: Handle code not found

            int obdNumber = Convert.ToInt32(match.Groups[3].Value);
            string obdMessage = "Unknown Fault";
            string obdType = match.Groups[1].Value;

            switch(obdType)
            {
                case "P":
                    PowertrainObdCodesDictionary.TryGetValue(obdNumber, out obdMessage);
                    
                    returnModel.Generic = true;
                    returnModel.Type = "Powertrain";

                    if(obdNumber >= 1 && obdNumber <= 299)
                    { 
                        returnModel.Subtype = "Air/Fuel Mixture Control and Metering";
                    }
                    else if(obdNumber >= 300 && obdNumber <= 399)
                    {
                        returnModel.Subtype = "Auxiliary Emissions Control";
                    }
                    else if(obdNumber >= 500 && obdNumber <= 599)
                    {
                        returnModel.Subtype = "Engine Idling Control, Vehicle Speed, and Auxiliary Inputs";
                    }
                    else if(obdNumber >= 600 && obdNumber <= 699)
                    {
                        returnModel.Subtype = "Onboard Computer and Auxiliary Outputs";
                    }
                    else if(obdNumber >= 700 && obdNumber <= 999)
                    {
                        returnModel.Subtype = "Transmission Control";
                    }
                    else if(obdNumber >= 1000)
                    {
                        if(
                            obdNumber >= 1000 && obdNumber <= 1999 ||
                            obdNumber >= 3000 && obdNumber <= 3300
                        )
                        {
                            returnModel.Generic = false;
                        }

                        returnModel.Subtype = "Other Powertrain Diagnostic Code";
                    }
                    
                    break;
                case "C":
                    // ...

                    returnModel.Generic = true;
                    returnModel.Type = "Chassis";

                    if(
                        obdNumber >= 1000 && obdNumber <= 1999 ||
                        obdNumber >= 2000 && obdNumber <= 2999
                    )
                    {
                        returnModel.Generic = false;
                    }

                    break;
                case "B":
                    // ...

                    returnModel.Generic = true;
                    returnModel.Type = "Body";

                    if(
                        obdNumber >= 1000 && obdNumber <= 1999 ||
                        obdNumber >= 2000 && obdNumber <= 2999
                    )
                    {
                        returnModel.Generic = false;
                    }

                    break;
                case "U":
                    // ...

                    returnModel.Generic = true;
                    returnModel.Type = "Network Communications";

                    if(
                        obdNumber >= 1000 && obdNumber <= 1999 ||
                        obdNumber >= 2000 && obdNumber <= 2999
                    )
                    {
                        returnModel.Generic = false;
                    }

                    break;
            }

            returnModel.Message = obdMessage;

            return returnModel;
        }
    }
}