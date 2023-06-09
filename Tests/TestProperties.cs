﻿using NUnit.Framework;
using TEST_Lab2;
using Tests;

namespace TestsProperties
{
    [TestFixture]
    public class TestPropertiesAirbase
    {
        [Test]
        public void Airbase_Name_Create()
        {
            var airbase = new Airbase("Сибирский");
            Assert.That(airbase.Name, Is.EqualTo("Сибирский"));
        }

        [Test]
        public void Airbase_List_Plains_NotNullAfterCreate() 
        {
            var airbase = new Airbase("Сибирский");
            Assert.NotNull(airbase.Plains);
        }
    }

    [TestFixture]
    public class TestPropertiesAirborneSystem
    {
        [Test]
        public void AirborneSystem_List_Sensors_Added()
        {
            var airborneSystem = new AirborneSystem(TestData.listSensors);
            Assert.That(airborneSystem.Sensors, Is.EqualTo(TestData.listSensors));
        }
    }

    [TestFixture]
    public class TestPropertiesCommander
    {
        [Test]
        public void Commander_Name_Create()
        {
            var commander = new Commander("Михаил");
            Assert.That(commander.Name, Is.EqualTo("Михаил"));
        }
    }

    [TestFixture]
    public class TestPropertiesHuman
    {
        [Test]
        public void Human_Name_Create()
        {
            var human = new Human("Олег");
            Assert.That(human.Name, Is.EqualTo("Олег"));
        }
    }

    [TestFixture]
    public class TestPropertiesPilot
    {
        [Test]
        public void Pilot_Name_Create()
        {
            var pilot = new Pilot("Василий");
            Assert.That(pilot.Name, Is.EqualTo("Василий"));
        }

        [Test]
        public void Pilot_Plane_AfterCreateNotAdded()
        {
            var pilot = new Pilot("Василий");
            Assert.IsNull(pilot.Plane);
        }

        [Test]
        public void Pilot_Plane_Added()
        {
            var pilot = new Pilot("Василий");
            var plane = TestData.listPlanes[new Random().Next(TestData.listPlanes.Count)];
            pilot.Plane = plane;
            Assert.That(pilot.Plane, Is.EqualTo(plane));
        }

        [Test]
        public void Pilot_CheckListNameSensors_IsNotNullAfterCreatePilot()
        {
            var pilot = new Pilot("Василий");
            Assert.IsNotNull(pilot.CheckListNameSensors);
        }

        [Test]
        public void Pilot_CheckListNameSensors_EmptyAfterCreatePilot()
        {
            var pilot = new Pilot("Василий");
            Assert.IsEmpty(pilot.CheckListNameSensors);
        }

        [Test]
        public void Pilot_CheckListNameSensors_SetItems()
        {
            var pilot = new Pilot("Василий");
            pilot.CheckListNameSensors = TestData.listNameSensors;
            Assert.That(pilot.CheckListNameSensors, Is.EqualTo(TestData.listNameSensors));
        }

        [Test]
        public void Pilot_CheckListNameSensors_AddItem()
        {
            var pilot = new Pilot("Василий");
            var nameSensor = "Датчик тангажа";
            pilot.CheckListNameSensors.Add(nameSensor);
            Assert.That(pilot.CheckListNameSensors, Has.Member(nameSensor));
        }

        [Test]
        public void Pilot_CheckListNameSensors_RemoveItem()
        {
            var pilot = new Pilot("Василий");
            pilot.CheckListNameSensors = TestData.listNameSensors;
            var removeNameSensor = TestData.listNameSensors[new Random().Next(TestData.listNameSensors.Count)];
            pilot.CheckListNameSensors.Remove(removeNameSensor);
            Assert.That(pilot.CheckListNameSensors, !Has.Member(removeNameSensor));
        }
    }

    [TestFixture]
    public class TestPropertiesPlane
    {
        [Test]
        public void Plane_ChackInitParametrs()
        {
            var id = 1;
            var model = "СУ-35";
            var airborneSystem = new AirborneSystem(TestData.listSensors);
            var plane = new Plane(id, model, airborneSystem);
            Assert.That(plane.Id, Is.EqualTo(id));
            Assert.That(plane.Model, Is.EqualTo(model));
            Assert.That(plane.AirborneSystem, Is.EqualTo(airborneSystem));
        }
    }

    [TestFixture]
    public class TestPropertiesSensor
    {
        [Test]
        public void Sensor_Name_Create()
        {
            var name = "Датчик дыма";
            var sensor = new Sensor(name);
            Assert.That(sensor.Name, Is.EqualTo(name));
        }
    }
}