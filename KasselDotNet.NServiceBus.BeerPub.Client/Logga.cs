using System;

namespace KasselDotNet.NServiceBus.BeerPub.Client {
    public class Logga {
        public static void Log(Exception ex) {
            Log(ex.Message);
        }

        public static void Log(string message) {
            Console.WriteLine($"{DateTime.Now}: {message}");
        }
    }
}
