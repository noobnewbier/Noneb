namespace InGameEditor.Events
{
    public struct InGameEditorUiMessage
    {
        public InGameEditorUiMessage(string value)
        {
            Value = value;
        }

        public string Value { get; }
    }
}