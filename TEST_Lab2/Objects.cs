using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TEST_Lab2
{
    public  class Airbase
    {
        public string Name { get; private set; }
        public List<Plane> Plains { get; set; }

        public Airbase(string name) 
        { 
            Name = name;
            Plains = new List<Plane>();
        }

        public void ArrivalPlane(Plane plane)
        {
            Plains.Add(plane);
        }

        public void DeparturePlane(Plane plane)
        {
            Plains.Remove(plane);
        }
    }

    public class Plane
    {
        public int Id { get; private set; }
        public string Model { get; private set; }
        public AirborneSystem AirborneSystem { get; private set; }

        public bool InFlight 
        {   
            get 
            { 
                return _inFlight; 
            } 
            set 
            {
                _inFlight = value;
                if (value == true)
                    OnFlight?.Invoke(this);
                else
                    NotFlight?.Invoke(this);
            }
        }
        private bool _inFlight;

        public Plane(int id, string model, AirborneSystem airborneSystem)
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

        public delegate void Flight(Plane plane);
        public event Flight OnFlight;
        public event Flight NotFlight;  
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

        public Sensor CheckSensor(Sensor sensor)
        {
            var isBroken = sensor.CheckWork();
            if (isBroken)
            {
                EmergencyResponseSystem.FixedProblemSensor(sensor);
            }
            return sensor;
        }
    }

    public static class EmergencyResponseSystem
    {
        public delegate void Fixed(Sensor sensor);
        public static event Fixed? OnFixed;
        public static void FixedProblemSensor(Sensor sensor)
        {
            if (sensor.IsFixedSoftwareProblem)
                sensor.HaveSoftwareProblem = false;
            else
                OnFixed?.Invoke(sensor);
        }
    }

    public class Sensor : State
    {
        public string Name { get; private set; }

        public bool HaveSoftwareProblem;
        public bool IsFixedSoftwareProblem;
        public Sensor(string name, bool isBroken = false, bool isFixedSoftwareProblem = true)
        {
            Name = name;
            HaveSoftwareProblem = isBroken;
            IsFixedSoftwareProblem = isFixedSoftwareProblem;
        }

        public bool CheckWork()
        {
            return HaveSoftwareProblem;
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

        public Plane? Plane { get; set; }
        public List<string> CheckListNameSensors { get; set; } = new List<string>();
        public List<Sensor> BrokenSensors = new List<Sensor>();

        private bool _allWell = true;

        public void StartFlight()
        {
            if(Plane != null)
            {
                Plane.OnAirborneSystem();
                Plane.InFlight = true;
                EmergencyResponseSystem.OnFixed += CancelCheckSensors;
            }
        }

        public void FinishFlight()
        {
            if (Plane != null)
            {
                Plane.OffAirbornSystem();
                Plane.InFlight = false;
            }
        }

        private void CancelCheckSensors(Sensor sensor)
        {
            _allWell = false;
        }

        public List<Sensor> CheckSensors()
        {
            var checkedSensors = new List<Sensor>();
            if(Plane.InFlight == true)
            {
                foreach (var sensorName in CheckListNameSensors)
                {
                    var sensor = Plane.AirborneSystem.Sensors.Find(p => p.Name == sensorName);
                    if (sensor != null)
                    {
                        checkedSensors.Add(Plane.AirborneSystem.CheckSensor(sensor));
                        if (sensor.HaveSoftwareProblem)
                            BrokenSensors.Add(sensor);
                    }

                    if(_allWell == false)
                        return checkedSensors;
                }
            }

            return checkedSensors;
        }
    }

    public class Commander : Human
    {
        public Commander(string name) : base(name) { }

        public void SetFlight(Airbase airbase, Pilot pilot, Plane plane, List<string> checkListNameSensors)
        {
            airbase.Plains.Add(plane);
            plane.OnFlight += airbase.DeparturePlane;
            plane.NotFlight += airbase.ArrivalPlane;
            pilot.Plane = plane;
            pilot.CheckListNameSensors = checkListNameSensors;
        }
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
