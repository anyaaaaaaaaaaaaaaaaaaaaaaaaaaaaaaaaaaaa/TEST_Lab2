namespace TEST_Lab2
{
    public static class EmergencyResponseSystem
    {
        public delegate void Fixed();
        public static event Fixed? NotFixed;
        public static void FixedProblemSensor(Sensor sensor)
        {
            if (sensor.IsFixedSoftwareProblem)
                sensor.HaveSoftwareProblem = false;
            else
                NotFixed?.Invoke();
        }
    }
}