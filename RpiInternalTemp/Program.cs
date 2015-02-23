using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpiInternalTemp
{
    class Program
    {
        public static MobileServiceClient MobileService = new MobileServiceClient(
            "https://atile.azure-mobile.net/",
            "AHfQsidIDnaIqiFSemWSIrJnHMdUTI49"
        );
        static void Main(string[] args)
        {
            Console.Write("Posting ");
            
            double temp = GetThermalZone0Temp();
            Console.Write(temp);
            
            MobileService.GetTable<SensorValue>().InsertAsync(
                new SensorValue()
                {
                    Sensor = "Thermal Zone0 Temperature",
                    Value = temp
                }).Wait();

            Console.WriteLine(", done!");
        }

        private static double GetThermalZone0Temp()
        {
            string tv;
            using (System.IO.TextReader tr =
                new System.IO.StreamReader("/sys/class/thermal/thermal_zone0/temp"))
            {
                tv = tr.ReadLine();
            }
            return double.Parse(tv) / 1000;
        }
    }

    public class SensorValue
    {
        public string Id { get; set; }
        public string Sensor { get; set; }
        public double Value { get; set; }
    }
}
