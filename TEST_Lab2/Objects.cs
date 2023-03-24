using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEST_Lab2
{
    public  class Airbase
    {
        public string Name { get; private set; }
        public List<FighterJet> fighterJets { get; set; }
        public Commander? Commander { get; set; }

        public Airbase(string name) 
        { 
            Name = name;
            fighterJets = new List<FighterJet>();
        }

    }

    public class FighterJet
    {
        public int Id { get; private set; }
        public string Model { get; private set; }
        public AirborneSystem AirborneSystem { get; private set; }


        public bool InFlight { get; set; }
        public FighterJet(int id, string model, AirborneSystem airborneSystem)
        {
            Id = id;
            Model = model;
            AirborneSystem = airborneSystem;
        }

        public bool OnAirborneSystem()
        {
            AirborneSystem.On();
            AirborneSystem.OnSensors();
            return AirborneSystem.GetActiveStatus();
        }

        public bool OffAirbornSystem()
        {
            AirborneSystem.Off();
            AirborneSystem.OffSensors();
            return AirborneSystem.GetActiveStatus();
        }
    }

    public class AirborneSystem : State
    {
        public List<Sensor> Sensors { get; set; }
        public AirborneSystem(List<Sensor> sensors)
        {
            Sensors = sensors;
        }

        public bool OnSensors()
        {
            foreach(var sensor in Sensors)
            {
                sensor.On();
            }
            return true;
        }

        public bool OffSensors()
        {
            foreach (var sensor in Sensors)
            {
                sensor.Off();
            }
            return true;
        }

        public bool CheckSensors()
        {
            foreach(var sensor in Sensors)
            {
                var status = sensor.CheckWork();
                if (status == false)
                    return false;
            }
            return true;
        }
    }

    public class Sensor : State
    {
        public string Name { get; private set; }

        private bool _isBroken;
        public Sensor(string name, bool isBroken = false)
        {
            Name = name;
            _isBroken = isBroken;
        }

        public bool CheckWork()
        {
            return _isBroken;
        }
    }











    public class Human
    {
        public string Name { get; private set; }
        public Human(string name)
        {
            Name = name;
        }
    }

    public class Pilot : Human
    {
        public Pilot(string name) : base(name) { }

        public FighterJet Plane { get; set; }
        public List<Sensor> CheckListSensors { get; set; }

        public bool StartFlight()
        {
            Plane.OnAirborneSystem();
            Plane.InFlight = true;
            return Plane.InFlight;
        }

        public bool FinishFlight()
        {
            Plane.OffAirbornSystem();
            Plane.InFlight = false;
            return Plane.InFlight;
        }
    }

    public class Commander : Human
    {
        public Commander(string name) : base(name) { }
    }



    public abstract class State
    {
        private bool _isActive;

        public void On()
        {
            _isActive = true;
        }

        public void Off()
        {
            _isActive = false;
        }

        public bool GetActiveStatus()
        {
            return _isActive;
        }
    }
}
