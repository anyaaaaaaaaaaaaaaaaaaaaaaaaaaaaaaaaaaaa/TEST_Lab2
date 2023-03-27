using TEST_Lab2;

namespace Tests
{
    public static class TestData
    {
        public static List<Sensor> listSensors = new List<Sensor>()
        {
            new Sensor("Датчик высоты"),
            new Sensor("Датчик тангажа", true, false),
            new Sensor("Датчик погодных условий", true),
            new Sensor("Радар")
        };

        public static List<string> listNameSensors = new List<string>()
        {
            "Датчик высоты",
            "Датчик тангажа",
            "Датчик погодных условий",
        };

        public static List<Plane> listPlanes = new List<Plane>()
        {
            new Plane(1, "СУ-35", new AirborneSystem(listSensors)),
            new Plane(2, "Миг-25", new AirborneSystem(listSensors))
        };
    }
}
