using NUnit.Framework;
using TEST_Lab2;
using Tests;

namespace TestsMethods
{
    [TestFixture]
    public class TestMethodsAirbase
    {
        [Test]
        public void Airbase_ArrivalPlane()
        {
            var airbase = new Airbase("Сахалин");
            var plane = TestData.listPlanes[new Random().Next(TestData.listPlanes.Count)];
            airbase.ArrivalPlane(plane);
            Assert.That(airbase.Plains, Has.Member(plane));
        }

        [Test]
        public void Airbase_DeparturePlane()
        {
            var airbase = new Airbase("Сахалин");
            var plane = TestData.listPlanes[new Random().Next(TestData.listPlanes.Count)];
            airbase.ArrivalPlane(plane);
            Assert.That(airbase.Plains, Has.Member(plane));
            airbase.DeparturePlane(plane);
            Assert.That(airbase.Plains, !Has.Member(plane));
        }
    }

    [TestFixture]
    public class TestMethodsAirborneSystem
    {
        [Test]
        public void AirborneSystem_On()
        {
            var airborneSystem = new AirborneSystem(TestData.listSensors);
            airborneSystem.On();
            Assert.That(airborneSystem.IsActive, Is.True);
        }

        [Test]
        public void AirborneSystem_Off()
        {
            var airborneSystem = new AirborneSystem(TestData.listSensors);
            airborneSystem.Off();
            Assert.That(airborneSystem.IsActive, Is.False);
        }

        [Test]
        public void AirborneSystem_OnSensors()
        {
            var airborneSystem = new AirborneSystem(TestData.listSensors);
            airborneSystem.On();
            var result = airborneSystem.OnSensors();
            Assert.That(result, Is.True);
            foreach(var sensor in airborneSystem.Sensors)
            {
                Assert.That(sensor.IsActive, Is.True);
            }
        }

        [Test]
        public void AirborneSystem_OffSensors()
        {
            var airborneSystem = new AirborneSystem(TestData.listSensors);
            airborneSystem.On();
            airborneSystem.OnSensors();
            var result = airborneSystem.OffSensors();
            Assert.That(result, Is.True);
            foreach(var sensor in airborneSystem.Sensors)
            {
                Assert.That(sensor.IsActive, Is.False);
            }
        }

        [TestCaseSource(nameof(listSensors))]
        public void AirborneSystem_CheckSensor(Sensor sensor)
        {
            var airborneSystem = new AirborneSystem(listSensors);
            airborneSystem.On();
            airborneSystem.OnSensors();

            var brokenSensors = new List<Sensor>();
            var isBrokedSensor = sensor.HaveSoftwareProblem;
            var checkedSensor = airborneSystem.CheckSensor(sensor, brokenSensors);
            Assert.That(checkedSensor.IsChecked, Is.True);
            if (isBrokedSensor)
            {
                if (sensor.IsFixedSoftwareProblem == true)
                    Assert.That(sensor.HaveSoftwareProblem, Is.False);
                else
                    Assert.That(sensor.HaveSoftwareProblem, Is.True);
                Assert.That(brokenSensors, Has.Member(sensor));
            }
            else
                Assert.That(brokenSensors, Is.Empty);
        }

        public static List<Sensor> listSensors = new List<Sensor>()
        {
            new Sensor("Датчик высоты"),
            new Sensor("Датчик тангажа", true),
            new Sensor("Датчик погодных условий", true, false),
        };
    }

    [TestFixture]
    public class TestMethodsCommander
    {
        [Test]
        public void Commander_SetFlight()
        {
            var airbase = new Airbase("Сахалин");
            var pilot = new Pilot("Михаил");
            var plane = TestData.listPlanes[new Random().Next(TestData.listPlanes.Count)];
            var checkListSensors = TestData.listNameSensors;
            var commander = new Commander("Николай");
            commander.SetFlight(airbase, pilot, plane, checkListSensors);
            Assert.That(airbase.Plains, Has.Member(plane));
            Assert.That(pilot.Plane, Is.EqualTo(plane));
            Assert.That(pilot.CheckListNameSensors, Is.EqualTo(checkListSensors));
        }

        [Test]
        public void Commander_ShowReport()
        {
            var report = "aaaaa";
            var commander = new Commander("Михаил");
            var result = commander.ShowReport(report);
            Assert.That(result, Is.EqualTo(report));
        }
    }

    [TestFixture]
    public class TestMethodsEmergencyResponseSystem
    {
        [Test]
        public void EmergencyReponseSystem_FixedProblemSensor()
        {
            var sensor = TestData.listSensors.First(p => p.HaveSoftwareProblem == true && p.IsFixedSoftwareProblem == true);
            EmergencyResponseSystem.FixedProblemSensor(sensor);
            Assert.That(sensor.HaveSoftwareProblem, Is.False);
        }
    }

    [TestFixture]
    public class TestMethodsPilot
    {
        [Test]
        public void Pilot_StartFlight_NotSetPlane()
        {
            var pilot = new Pilot("Илья");
            Assert.That(() => pilot.StartFlight(), Throws.Exception);
        }

        [Test]
        public void Pilot_StartFlight_SetPlane()
        {
            var pilot = new Pilot("Илья");
            var plane = TestData.listPlanes[new Random().Next(TestData.listPlanes.Count)];
            pilot.Plane = plane;
            pilot.StartFlight();
            Assert.That(pilot.Plane.InFlight, Is.True);
        }

        [Test]
        public void Pilot_FinishFlight_NotSetPlane()
        {
            var pilot = new Pilot("Илья");
            Assert.That(() => pilot.FinishFlight(), Throws.Exception);
        }

        [Test]
        public void Pilot_FinishFlight_SetPlane()
        {
            var pilot = new Pilot("Илья");
            var plane = TestData.listPlanes[new Random().Next(TestData.listPlanes.Count)];
            pilot.Plane = plane;
            pilot.StartFlight();
            pilot.FinishFlight();
            Assert.That(pilot.Plane.InFlight, Is.False);
        }

        [Test]
        public void Pilot_CancelCheckSensors()
        {
            var pilot = new Pilot("Алексей");
            pilot.CancelCheckSensors();
            Assert.That(pilot.AllWell, Is.False);
        }

        [Test]
        public void Pilot_CheckSensors_NotSetPlane()
        {
            var pilot = new Pilot("Алексей");
            Assert.That(() => pilot.CheckSensors(), Throws.Exception);
        }

        [Test]
        public void Pilot_CheckSensors_SetPlane_NotFly()
        {
            var pilot = new Pilot("Алексей");
            var plane = TestData.listPlanes[new Random().Next(TestData.listPlanes.Count)];
            plane.InFlight = false;
            pilot.Plane = plane;
            Assert.That(() => pilot.CheckSensors(), Throws.Exception);
        }

        [Test]
        public void Pilot_CheckSensors_SetPlain_Fly_WithNormalSensors()
        {
            var pilot = new Pilot("Алексей");
            var plane = TestData.listPlanes[new Random().Next(TestData.listPlanes.Count)];
            var normalSensor = plane.AirborneSystem.Sensors.First(p => p.HaveSoftwareProblem == false);
            pilot.Plane = plane;
            pilot.CheckListNameSensors = new List<string>() { normalSensor.Name };
            pilot.StartFlight();
            pilot.CheckSensors();
            Assert.That(pilot.AllWell, Is.True);
        }

        [Test]
        public void Pilot_CheckSensors_SetPlain_Fly_WithBrokenNotFixedSensors()
        {
            var pilot = new Pilot("Алексей");
            var plane = TestData.listPlanes[new Random().Next(TestData.listPlanes.Count)];
            var brokenSensor = plane.AirborneSystem.Sensors.First(p => p.HaveSoftwareProblem == true && p.IsFixedSoftwareProblem == false);
            pilot.Plane = plane;
            pilot.CheckListNameSensors = new List<string>() { brokenSensor.Name };
            pilot.StartFlight();
            pilot.CheckSensors();
            Assert.That(pilot.AllWell, Is.False);
        }

        [Test]
        public void Pilot_CreateReport_NotSetPlain()
        {
            var commander = new Commander("Максим");
            var pilot = new Pilot("Станислав");
            Assert.That(() => pilot.CreateReport(commander), Throws.Exception);
        }

        [Test]
        public void Pilot_CreateReport_SetPlain()
        {
            var commander = new Commander("Максим");
            var pilot = new Pilot("Станислав");
            var airbase = new Airbase("Нытва");
            var plane = TestData.listPlanes[new Random().Next(TestData.listPlanes.Count)];
            var checkListNameSensors = TestData.listNameSensors;
            commander.SetFlight(airbase, pilot, plane, checkListNameSensors);
            pilot.StartFlight();
            pilot.CheckSensors();
            pilot.FinishFlight();

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
                    var sensors = TestData.listSensors.Where(p => p.HaveSoftwareProblem == true);
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

            var resultReport = pilot.CreateReport(commander);
            Assert.That(resultReport, Is.EqualTo(report));
        }
    }

    [TestFixture]
    public class TestMethodsPlane
    {
        [Test]
        public void Plane_OnAirborneSystem()
        {
            var plane = TestData.listPlanes[new Random().Next(TestData.listPlanes.Count)];
            plane.OnAirborneSystem();
            Assert.IsTrue(plane.AirborneSystem.IsActive);
        }

        [Test]
        public void Plane_OffAirborneSystem()
        {
            var plane = TestData.listPlanes[new Random().Next(TestData.listPlanes.Count)];
            plane.OnAirborneSystem();
            plane.OffAirborneSystem();
            Assert.IsFalse(plane.AirborneSystem.IsActive);
        }
    }

    [TestFixture]
    public class TestMethodsSensor
    {
        [Test]
        public void Sensor_On()
        {
            var sensor = new Sensor("Датчик дыма");
            sensor.On();
            Assert.That(sensor.IsActive, Is.True);
        }

        [Test]
        public void Sensor_Off()
        {
            var sensor = new Sensor("Датчик дыма");
            sensor.Off();
            Assert.That(sensor.IsActive, Is.False);
        }

        [Test]
        public void Sensor_CheckWork_NotActive()
        {
            var sensor = new Sensor("Датчик дыма");
            var result = sensor.CheckWork();
            Assert.IsNull(result);
        }

        [Test]
        public void Sensor_CheckWork_IsActive()
        {
            var sensor = new Sensor("Датчик дыма");
            sensor.On();
            sensor.CheckWork();
            Assert.True(sensor.IsChecked);
        }
    }
}