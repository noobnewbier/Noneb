namespace InGameEditor.Events
{
    public struct InGameMessage
    {
        public InGameMessage(string value)
        {
            Value = value;
        }

        public string Value { get; }
    }
}