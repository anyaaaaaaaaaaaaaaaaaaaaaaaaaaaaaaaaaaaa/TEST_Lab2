using NUnit.Framework;
using TEST_Lab2;
using Tests;

namespace TestSystem
{
    [TestFixture]
    public class TestSystem
    {
        [Test]
        public void TestSystem_Scenario1()
        {
            var commander = new Commander("Михаил");
            Assert.That(commander.Name, Is.EqualTo("Михаил"));
            var airbase = new Airbase("Сокольники 1");
            Assert.That(airbase.Name, Is.EqualTo("Сокольники 1"));
            var listSensors = TestData.listSensors.Where(p => p.HaveSoftwareProblem == false).ToList();
            var airborneSystem = new AirborneSystem(listSensors);
            Assert.That(airborneSystem.Sensors, Is.EqualTo(listSensors));
            var plane = new Plane(1, "СУ-35", airborneSystem);
            Assert.That(plane.Id, Is.EqualTo(1));
            Assert.That(plane.Model, Is.EqualTo("СУ-35"));
            var pilot = new Pilot("Василий");
            Assert.That(pilot.Name, Is.EqualTo("Василий"));
            var checkListNameSensors = listSensors.Select(p => p.Name).ToList();
            var expectedReport = TestData.CreateReport(pilot, plane, checkListNameSensors);

            commander.SetFlight(airbase, pilot, plane, checkListNameSensors);
            Assert.That(airbase.Plains, Has.Member(plane));
            Assert.That(pilot.Plane, Is.EqualTo(plane));
            Assert.That(pilot.CheckListNameSensors, Is.EqualTo(checkListNameSensors));
            pilot.StartFlight();
            Assert.That(plane.InFlight, Is.True);
            Assert.That(plane.AirborneSystem.IsActive, Is.True);
            foreach (var sensor in plane.AirborneSystem.Sensors)
                Assert.That(sensor.IsActive, Is.True);
            pilot.CheckSensors();
            foreach (var sensor in plane.AirborneSystem.Sensors)
                Assert.That(sensor.IsChecked, Is.True);

            var resultReport = pilot.CreateReport(commander);
            Assert.That(resultReport, Is.EqualTo(expectedReport));
        }

        [Test]
        public void TestSystem_Scenario2()
        {
            var commander = new Commander("Михаил");
            Assert.That(commander.Name, Is.EqualTo("Михаил"));
            var airbase = new Airbase("Сокольники 1");
            Assert.That(airbase.Name, Is.EqualTo("Сокольники 1"));
            var listSensors = TestData.listSensors.Where(p => p.HaveSoftwareProblem == true && p.IsFixedSoftwareProblem == true).ToList();
            var airborneSystem = new AirborneSystem(listSensors);
            Assert.That(airborneSystem.Sensors, Is.EqualTo(listSensors));
            var plane = new Plane(1, "СУ-35", airborneSystem);
            Assert.That(plane.Id, Is.EqualTo(1));
            Assert.That(plane.Model, Is.EqualTo("СУ-35"));
            var pilot = new Pilot("Василий");
            Assert.That(pilot.Name, Is.EqualTo("Василий"));
            var checkListNameSensors = listSensors.Select(p => p.Name).ToList();
            var expectedReport = TestData.CreateReport(pilot, plane, checkListNameSensors);

            commander.SetFlight(airbase, pilot, plane, checkListNameSensors);
            Assert.That(airbase.Plains, Has.Member(plane));
            Assert.That(pilot.Plane, Is.EqualTo(plane));
            Assert.That(pilot.CheckListNameSensors, Is.EqualTo(checkListNameSensors));
            pilot.StartFlight();
            Assert.That(plane.InFlight, Is.True);
            Assert.That(plane.AirborneSystem.IsActive, Is.True);
            foreach (var sensor in plane.AirborneSystem.Sensors)
                Assert.That(sensor.IsActive, Is.True);
            pilot.CheckSensors();
            foreach (var sensor in plane.AirborneSystem.Sensors)
                Assert.That(sensor.IsChecked, Is.True);

            var resultReport = pilot.CreateReport(commander);
            Assert.That(resultReport, Is.EqualTo(expectedReport));
        }

        [Test]
        public void TestSystem_Scenario3()
        {
            var commander = new Commander("Михаил");
            Assert.That(commander.Name, Is.EqualTo("Михаил"));
            var airbase = new Airbase("Сокольники 1");
            Assert.That(airbase.Name, Is.EqualTo("Сокольники 1"));
            var listSensors = TestData.listSensors.Where(p => p.HaveSoftwareProblem == true && p.IsFixedSoftwareProblem == false).ToList();
            var airborneSystem = new AirborneSystem(listSensors);
            Assert.That(airborneSystem.Sensors, Is.EqualTo(listSensors));
            var plane = new Plane(1, "СУ-35", airborneSystem);
            Assert.That(plane.Id, Is.EqualTo(1));
            Assert.That(plane.Model, Is.EqualTo("СУ-35"));
            var pilot = new Pilot("Василий");
            Assert.That(pilot.Name, Is.EqualTo("Василий"));
            var checkListNameSensors = listSensors.Select(p => p.Name).ToList();
            var expectedReport = TestData.CreateReport(pilot, plane, checkListNameSensors);

            commander.SetFlight(airbase, pilot, plane, checkListNameSensors);
            Assert.That(airbase.Plains, Has.Member(plane));
            Assert.That(pilot.Plane, Is.EqualTo(plane));
            Assert.That(pilot.CheckListNameSensors, Is.EqualTo(checkListNameSensors));
            pilot.StartFlight();
            Assert.That(plane.InFlight, Is.True);
            Assert.That(plane.AirborneSystem.IsActive, Is.True);
            foreach (var sensor in plane.AirborneSystem.Sensors)
                Assert.That(sensor.IsActive, Is.True);
            pilot.CheckSensors();
            foreach (var sensor in plane.AirborneSystem.Sensors)
                Assert.That(sensor.IsChecked, Is.True);

            var resultReport = pilot.CreateReport(commander);
            Assert.That(resultReport, Is.EqualTo(expectedReport));
        }
    }
}