using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEST_Lab2
{
    public class AirborneSystem : State
    {
        public List<Sensor> Sensors { get; private set; }
        public AirborneSystem(List<Sensor> sensors)
        {
            Sensors = new List<Sensor>(sensors);
        }

        public bool OnSensors()
        {
            foreach (var sensor in Sensors)
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

        public Sensor CheckSensor(Sensor sensor, List<Sensor> BrokenSensors)
        {
            var isBroken = sensor.CheckWork();
            if (isBroken)
            {
                EmergencyResponseSystem.FixedProblemSensor(sensor);
                BrokenSensors.Add(sensor);
            }
            return sensor;
        }
    }
}
