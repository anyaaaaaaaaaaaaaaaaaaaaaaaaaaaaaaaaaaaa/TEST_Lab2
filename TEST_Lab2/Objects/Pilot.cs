namespace TEST_Lab2
{
    public class Pilot : Human
    {
        public Pilot(string name) : base(name) { }
        public Plane? Plane { get { return _plane; } set { _plane = value; AllWell = true; } }
        private Plane? _plane;
        public List<string> CheckListNameSensors { get; set; } = new List<string>();
        public List<Sensor> BrokenSensors = new List<Sensor>();

        public bool AllWell;

        public void StartFlight()
        {
            if (Plane != null)
            {
                Plane.OnAirborneSystem();
                Plane.InFlight = true;
                EmergencyResponseSystem.NotFixed += CancelCheckSensors;
            }
            else
                throw new Exception("Самолёт не назначен.");
        }

        public void FinishFlight()
        {
            if(Plane != null)
            {
                if (AllWell == true)
                {
                    Plane.OffAirborneSystem();
                    Plane.InFlight = false;
                }
            }
            else
                throw new Exception("Самолёт не назначен или самолёт разбился.");
        }

        public void CancelCheckSensors()
        {
            AllWell = false;
        }

        public void CheckSensors()
        {
            if(Plane != null)
            {
                if (Plane.InFlight == true)
                {
                    foreach (var sensorName in CheckListNameSensors)
                    {
                        var sensor = Plane.AirborneSystem.Sensors.Find(p => p.Name == sensorName);
                        if (sensor != null)
                        {
                            Plane.AirborneSystem.CheckSensor(sensor, BrokenSensors);
                        }

                        if (AllWell == false)
                        {
                            Plane.IsPlaneExist = false;
                            return;
                        }
                    }
                }
                else
                {
                    throw new Exception("Самолёт не находится в полёте.");
                }
            }
            else
                throw new Exception("Самолёт не назначен.");
        }

        public string CreateReport(Commander commander)
        {
            if(Plane != null)
            {
                var report = $"Пилот: {Name}\n";

                report += $"Самолёт: ID {Plane.Id}, Model {Plane.Model}, Exist {Plane.IsPlaneExist}\n";
                report += "Датчики в самолёте:\n";
                foreach (var sensor in Plane.AirborneSystem.Sensors)
                    report += $"\t{sensor.Name}\n";

                if (CheckListNameSensors.Count > 0)
                {
                    report += "Результат проверки:\n";
                    report += "\tДатчики для проверки:\n";
                    foreach (var sensorName in CheckListNameSensors)
                        report += $"\t\t{sensorName}\n";

                    if (BrokenSensors.Count > 0)
                    {
                        report += "\tСломанные датчики\n";
                        foreach (var sensor in BrokenSensors)
                            report += $"\t\t{sensor.Name}, Исправлен {!sensor.HaveSoftwareProblem}\n";
                    }
                }

                commander.ShowReport(report);
                return report;
            }
            else
                throw new Exception("Самолёт не назначен.");
        }
    }
}