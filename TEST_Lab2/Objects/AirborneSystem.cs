namespace TEST_Lab2
{
    public class AirborneSystem : State
    {
        public List<Sensor> Sensors { get; private set; }
        public AirborneSystem(List<Sensor> sensors)
        {
            Sensors = new List<Sensor>(sensors);
        }

        public bool? OnSensors()
        {
            if (IsActive == true)
            {
                foreach (var sensor in Sensors)
                {
                    sensor.On();
                }
                return true;
            }
            else
                return null;
        }

        public bool? OffSensors()
        {
            if (IsActive == true)
            {
                foreach (var sensor in Sensors)
                {
                    sensor.Off();
                }
                return true;
            }
            else
                return null;
        }

        public Sensor CheckSensor(Sensor sensor, List<Sensor> BrokenSensors)
        {
            if (IsActive == true)
            {
                var isBroken = sensor.CheckWork();
                if (isBroken == true)
                {
                    EmergencyResponseSystem.FixedProblemSensor(sensor);
                    BrokenSensors.Add(sensor);
                }
            }
            
            return sensor;
        }
    }
}
