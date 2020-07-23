namespace InGameEditor.Events
{
    public struct UiMessageEvent
    {
        public UiMessageEvent(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }
}