namespace TEST_Lab2
{
    public class Plane
    {
        public int Id { get; private set; }
        public string Model { get; private set; }
        public AirborneSystem AirborneSystem { get; private set; }

        public bool IsPlaneExist = true;
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

        public bool OffAirborneSystem()
        {
            AirborneSystem.Off();
            AirborneSystem.OffSensors();
            return AirborneSystem.GetActiveStatus();
        }

        public delegate void Flight(Plane plane);
        public event Flight? OnFlight;
        public event Flight? NotFlight;
    }
}