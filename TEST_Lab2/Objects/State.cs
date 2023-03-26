namespace TEST_Lab2
{
    public abstract class State
    {
        public bool IsActive { get; private set; }

        public void On()
        {
            IsActive = true;
        }

        public void Off()
        {
            IsActive = false;
        }

        public bool GetActiveStatus()
        {
            return IsActive;
        }
    }
}