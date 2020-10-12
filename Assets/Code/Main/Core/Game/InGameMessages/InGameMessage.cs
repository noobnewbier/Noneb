namespace Main.Core.Game.InGameMessages
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