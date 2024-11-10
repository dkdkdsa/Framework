namespace Framework.Counter
{
    public class IntCounter : ICounter<int>
    {
        public CounterRole Role { get; set; }
        public int Value { get; set; }

        public int ApplyRule()
        {
            if (Role == CounterRole.Plus)
                return ++Value;
            else
                return --Value;
        }

        public object Clone()
        {
            return new IntCounter()
            {

                Value = Value,
                Role = Role

            };
        }

        public void SetValue(int value)
        {
            Value = value;
        }
    }
}