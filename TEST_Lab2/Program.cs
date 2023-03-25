namespace TEST_Lab2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var commander = new Commander("Михаил");
            var airbase = new Airbase("Сокольники 1");

            var listSensors = new List<Sensor>()
            {
                new Sensor("Датчик высоты"),
                new Sensor("Датчик тангажа", true, false),
                new Sensor("Датчик погодных условий"),
                new Sensor("Радар")
            };
            var airsystem = new AirborneSystem(listSensors);

            var plane = new Plane(1, "СУ-35", airsystem);

            var pilot = new Pilot("Василий");

            var checkListNameSensors = new List<string>()
            {
                "Датчик тангажа"
            };

            commander.SetFlight(airbase, pilot, plane, checkListNameSensors);
            pilot.StartFlight();
            pilot.CheckSensors();
        }
    }
}