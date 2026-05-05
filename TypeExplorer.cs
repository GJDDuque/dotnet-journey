public class TypeExplorer
{
    struct Temperature
    {
        public int Celsius { get; set; }
        public int Fahrenheit => (Celsius * 9 / 5) + 32;
    }

    class WeatherStation
    {
        public Temperature CurrentTemperature { get; set; }
        public string Location { get; set; }
    }

    public static void Main()
    {
        int a = 120;
        int b = a;
        b = b + 20;
        Console.WriteLine($"a: {a}, b: {b}");

        var explorer = new TypeExplorer();
        explorer.TestAsync().Wait();
    }

    public async Task TestAsync()
    {
        await Task.Delay(1000);
        Console.WriteLine("Done.");
    }
}
