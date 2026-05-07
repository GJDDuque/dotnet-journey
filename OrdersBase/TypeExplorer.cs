public class TypeExplorer
{
    struct Temperature
    {
        public double Celsius { get; set; }
        public double Fahrenheit => (Celsius * 9.0 / 5.0) + 32.0;
    }

    class WeatherStation
    {
        public Temperature CurrentTemperature { get; set; }
        public string? Location { get; set; }
    }

    public static async Task Main()
    {
        int a = 120;
        int b = a;
        b = b + 20;
        Console.WriteLine($"a: {a}, b: {b}");

        var station1 = new WeatherStation { Location = "Lisbon" };
        var station2 = station1;
        station2.Location = "Porto";
        Console.WriteLine(station1.Location);

        var temp = new Temperature { Celsius = 37 };
        Console.WriteLine($"Struct — {temp.Celsius}°C = {temp.Fahrenheit}°F");

        var station = new WeatherStation
        {
            Location = "Évora",
            CurrentTemperature = new Temperature { Celsius = 28 }
        };
        Console.WriteLine($"Station: {station.Location}, Temp: {station.CurrentTemperature.Fahrenheit}°F");

        var explorer = new TypeExplorer();
        await explorer.TestAsync();
    }

    public async Task TestAsync()
    {
        await Task.Delay(1000);
        Console.WriteLine("Done.");
    }
}
