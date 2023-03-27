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

        public static string CreateReport(Pilot pilot, Plane plane, List<string> checkListNameSensors)
        {
            var report = $"Пилот: {pilot.Name}\n";
            if (plane.AirborneSystem.Sensors.Any(p => p.HaveSoftwareProblem == true && p.IsFixedSoftwareProblem == false))
            {
                report += $"Самолёт: ID {plane.Id}, Model {plane.Model}, Exist False\n";
            }
            else
                report += $"Самолёт: ID {plane.Id}, Model {plane.Model}, Exist True\n";
            report += "Датчики в самолёте:\n";
            foreach (var sensor in plane.AirborneSystem.Sensors)
                report += $"\t{sensor.Name}\n";
            if (checkListNameSensors.Count > 0)
            {
                report += "Результат проверки:\n";
                report += "\tДатчики для проверки:\n";
                foreach (var sensorName in checkListNameSensors)
                    report += $"\t\t{sensorName}\n";

                if (plane.AirborneSystem.Sensors.Any(p => p.HaveSoftwareProblem == true))
                {
                    report += "\tСломанные датчики\n";
                    var sensors = plane.AirborneSystem.Sensors.Where(p => p.HaveSoftwareProblem == true);
                    foreach (var sensor in sensors)
                    {
                        if (sensor.IsFixedSoftwareProblem == true)
                            report += $"\t\t{sensor.Name}, Исправлен True\n";
                        else
                            report += $"\t\t{sensor.Name}, Исправлен False\n";

                        if (sensor.IsFixedSoftwareProblem == false)
                            break;
                    }

                }
            }
            return report;
        }
    }
}
