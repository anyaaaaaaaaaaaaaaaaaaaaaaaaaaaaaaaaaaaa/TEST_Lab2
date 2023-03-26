namespace TEST_Lab2
{
    public class Sensor : State
    {
        public string Name { get; private set; }

        public bool HaveSoftwareProblem;
        public bool IsFixedSoftwareProblem;
        public bool IsChecked = false;
        public Sensor(string name, bool isBroken = false, bool isFixedSoftwareProblem = true)
        {
            Name = name;
            HaveSoftwareProblem = isBroken;
            IsFixedSoftwareProblem = isFixedSoftwareProblem;
        }

        public bool? CheckWork()
        {
            if (IsActive == true)
            {
                IsChecked = true;
                return HaveSoftwareProblem;
            }
            else
                return null;
        }
    }
}