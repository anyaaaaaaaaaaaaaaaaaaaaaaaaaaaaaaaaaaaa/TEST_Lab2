using NUnit.Framework;
using TEST_Lab2;
using Tests;

namespace TestsStates
{
    [TestFixture]
    public class TestStatesAirborneSystem
    {
        [Test]
        public void AirborneSystem_Init_On_Off()
        {
            var airborneSystem = new AirborneSystem(TestData.listSensors);
            Assert.That(airborneSystem.IsActive, Is.False);
            airborneSystem.On();
            Assert.That(airborneSystem.IsActive, Is.True);
            airborneSystem.Off();
            Assert.That(airborneSystem.IsActive, Is.False);
        }
    }

    [TestFixture]
    public class TestStatesPilot
    {
        [Test]
        public void Pilot_Init_AllWell_NotAllWell()
        {
            var plane = TestData.listPlanes[new Random().Next(TestData.listPlanes.Count)];
            var pilot = new Pilot("Михаил");
            pilot.Plane = plane;
            pilot.CheckListNameSensors = TestData.listSensors.Where(p => p.HaveSoftwareProblem == true && 
                                                                         p.IsFixedSoftwareProblem == false)
                                                                .Select(p => p.Name).ToList();
            Assert.That(pilot.AllWell, Is.True);
            pilot.StartFlight();
            pilot.CheckSensors();
            Assert.That(pilot.AllWell, Is.False);
        }
    }

    [TestFixture]
    public class TestStatesPlane
    {
        [Test]
        public void Plane_Init_InFlight_NotInFlight()
        {
            var plane = new Plane(3, "МИГ-25", new AirborneSystem(TestData.listSensors));
            var pilot = new Pilot("Михаил");
            pilot.Plane = plane;
            Assert.That(plane.InFlight, Is.False);
            pilot.StartFlight();
            Assert.That(plane.InFlight, Is.True);
            pilot.FinishFlight();
            Assert.That(plane.InFlight, Is.False);
        }

        [Test]
        public void Plane_Init_PlaneExist_PlaneNotExist()
        {
            var plane = new Plane(3, "МИГ-25", new AirborneSystem(TestData.listSensors));
            var pilot = new Pilot("Михаил");
            pilot.Plane = plane;
            pilot.CheckListNameSensors = TestData.listSensors.Where(p => p.HaveSoftwareProblem == true && 
                                                                         p.IsFixedSoftwareProblem == false)
                                                                .Select(p => p.Name).ToList();
            Assert.That(plane.IsPlaneExist, Is.True);
            pilot.StartFlight();
            pilot.CheckSensors();
            Assert.That(plane.IsPlaneExist, Is.False);
        }
    }

    [TestFixture]
    public class TestStatesSensor
    {
        [Test]
        public void Sensor_Init_On_Off()
        {
            var sensor = new Sensor("Датчик дыма");
            Assert.That(sensor.IsActive, Is.False);
            sensor.On();
            Assert.That(sensor.IsActive, Is.True);
            sensor.Off();
            Assert.That(sensor.IsActive, Is.False);
        }

        [Test]
        public void Sensor_Init_On_Checked()
        {
            var sensor = new Sensor("Датчик дыма");
            Assert.That(sensor.IsActive, Is.False);
            sensor.On();
            Assert.That(sensor.IsActive, Is.True);
            Assert.That(sensor.IsChecked, Is.False);
            sensor.CheckWork();
            Assert.That(sensor.IsChecked, Is.True);
        }
    }
}