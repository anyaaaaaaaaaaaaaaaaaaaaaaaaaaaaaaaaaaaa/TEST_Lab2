namespace TEST_Lab2
{
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