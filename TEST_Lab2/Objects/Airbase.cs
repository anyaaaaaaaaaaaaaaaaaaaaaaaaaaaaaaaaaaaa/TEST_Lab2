namespace TEST_Lab2
{
    public class Airbase
    {
        public string Name { get; private set; }
        public List<Plane> Plains { get; private set; }

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
}
