using System.ComponentModel;

namespace client.Game.Core.Data
{
    public static class EditorData
    {
        private static EditorTools _CurrentTool = EditorTools.SELECT;

        public static event PropertyChangedEventHandler CurrentToolChanged;
        public static EditorTools CurrentTool
        {
            get { return _CurrentTool; }
            set
            {
                if (_CurrentTool != value)
                {
                    _CurrentTool = value;
                    CurrentToolChanged?.Invoke(null, new PropertyChangedEventArgs(nameof(CurrentTool)));
                }
            }
        }

        public static bool IsToolActive(EditorTools tool)
        {
            return CurrentTool == tool;
        }
    }
}
