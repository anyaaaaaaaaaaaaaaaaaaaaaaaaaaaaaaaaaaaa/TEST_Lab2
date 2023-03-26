    namespace TEST_Lab2
{
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

        public string ShowReport(string report)
        {
            Console.WriteLine(report);
            return report;
        }
    }
}