using System.ComponentModel;

namespace online_osu_beatmap_editor_client.views.Editor
{
    static class EditorData
    {
        public static EditorTools currentlySelectedEditorTool;

        public static EditorGridType _gridType;

        public static event PropertyChangedEventHandler GridTypeChanged;
        public static EditorGridType gridType
        {
            get { return _gridType; }
            set
            {
                if (_gridType != value)
                {
                    _gridType = value;
                    OnGridTypeChanged();
                }
            }
        }

        public static bool isNewComboActive = false;
        public static bool isWhistleActive = false;
        public static bool isFinishActive = false;
        public static bool isClapActive = false;
        public static bool isDistanceSnapActive = false;
        public static bool isGridSnapActive = false;

        public static float CS;
        private static void OnGridTypeChanged()
        {
            GridTypeChanged?.Invoke(null, new PropertyChangedEventArgs(nameof(gridType)));
        }
    }
}
