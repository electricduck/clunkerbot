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
            {0011, "Intake Camshaft Position Actuator Timing Over-Advanced (Bank 1)"},
            {0012, "Intake Camshaft Position Actuator Timing Over-Retarded (Bank 1)"},
            {0013, "Exhaust Camshaft Position Actuator Circuit (Bank 1)"},
            {0014, "Exhaust Camshaft Position Timing Over-Advanced (Bank 1)"},
            {0015, "Exhaust Camshaft Position Timing Over-Retarded (Bank 1)"},
            {0016, "Crankshaft Position Camshaft Position Correlation (Bank 1 Sensor A)"},
            {0017, "Crankshaft Position Camshaft Position Correlation (Bank 1 Sensor B)"},
            {0018, "Crankshaft Position Camshaft Position Correlation (Bank 2 Sensor A)"},
            {0019, "Crankshaft Position Camshaft Position Correlation (Bank 2 Sensor B)"},
            {0020, "Intake Camshaft Position Actuator Circuit (Bank 2)"},
            {0021, "Intake Camshaft Position Timing Over-Advanced (Bank 2)"},
            {0022, "Intake Camshaft Position Timing Over-Retarded (Bank 2)"},
            {0023, "Exhaust Camshaft Position Actuator Circuit (Bank 2)"},
            {0024, "Exhaust Camshaft Position Timing Over-Advanced (Bank 2)"},
            {0025, "Exhaust Camshaft Position Timing Over-Retarded (Bank 2)"},
            {0026, "Intake Valve Control Solenoid Circuit Range/Performance (Bank 1)"},
            {0027, "Exhaust Vavle Control Solenoid Circuit Range/Performance (Bank 1)"},
            {0028, "Intake Valve Control Solenoid Circuit Range/Performance (Bank 2)"},
            {0029, "Exhaust Vavle Control Solenoid Circuit Range/Performance (Bank 2)"},
            {0030, "Heated Oxygen Sensor (HO2S) Heater Control Circuit (Bank 1 Sensor 1)"},
            {0031, "Heated Oxygen Sensor (HO2S) Heater Circuit Low Voltage (Bank 1 Sensor 1)"},
            {0032, "Heated Oxygen Sensor (HO2S) Heated Circuit High Voltage (Bank 1 Sensor 1)"},
            {0033, "Turbo/SuperCharger Bypass Valve Control Circuit"},
            {0034, "Turbo/SuperCharger Bypass Valve Control Circuit Low"},
            {0035, "Turbo/SuperCharger Bypass Valve Control Circuit High"},
            {0036, "Heated Oxygen Sensor (HO2S) Heater Control Circuit (Bank 1 Sensor 2)"},
            {0037, "Heated Oxygen Sensor (HO2S) Heater Control Circuit Low Voltage (Bank 1 Sensor 2)"},
            {0038, "Heated Oxygen Snesor (HO2S) Heater Control Circuit High Voltage (Bank 1 Sensor 2)"},
            {0039, "Turbo/SuperCharger Bypass Valve Control Circuit Range/Performance"},
            {0040, "Oxygen Sensor Signals Swapped (Bank 1 Sensor 1 / Bank 2 Sensor 1)"},
            {0041, "Oxygen Sensor Signals Swapped (Bank 1 Sensor 2 / Bank 2 Sensor 2)"},
            {0042, "Heated Oxygen Sensor (HO2S) Heater Control Circuit (Bank 1 Sensor 3)"},
            {0043, "Heated Oxygen Sensor (HO2S) Heater Control Circuit Low Voltage (Bank 1 Sensor 3)"},
            {0044, "Heated Oxygen Snesor (HO2S) Heater Control Circuit High Voltage (Bank 1 Sensor 3)"},
            {0045, "Unknown"},
            {0046, "Turbo/SuperCharger Boost Control 'A' Circuit Range/Performance"},
            {0047, "Turbo/SuperCharger Boost Control 'A' Circuit Low"},
            {0048, "Turbo/SuperCharger Boost Control 'A' Circuit High"},
            {0049, "Turbo/SuperCharger Turbine Overspeed"},
            {0050, "Heated Oxygen Sensor (HO2S) Heater Control Circuit (Bank 2 Sensor 1)"},
            {0051, "Heated Oxygen Sensor (HO2S) Heater Control Circuit Low Voltage (Bank 2 Sensor 1)"},
            {0052, "Heated Oxygen Snesor (HO2S) Heater Control Circuit High Voltage (Bank 2 Sensor 1)"},

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