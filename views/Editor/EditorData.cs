using System.ComponentModel;

namespace online_osu_beatmap_editor_client.views.Editor
{
    static class EditorData
    {
        public static EditorTools currentlySelectedEditorTool;

        #region GridType

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
                    GridTypeChanged?.Invoke(null, new PropertyChangedEventArgs(nameof(gridType)));
                }
            }
        }

        #endregion GridType

        #region BackgroundDim

        public static float _backgroundDim;

        public static event PropertyChangedEventHandler BackgroundDimChanged;
        public static float backgroundDim
        {
            get { return _backgroundDim; }
            set
            {
                if (_backgroundDim != value)
                {
                    _backgroundDim = value;
                    BackgroundDimChanged?.Invoke(null, new PropertyChangedEventArgs(nameof(backgroundDim)));
                }
            }
        }

        #endregion BackgroundDim

        #region IsNewComboActive

        public static bool _isNewComboActive = false;

        public static event PropertyChangedEventHandler IsNewComboActiveChanged;
        public static bool isNewComboActive
        {
            get { return _isNewComboActive; }
            set
            {
                if (_isNewComboActive != value)
                {
                    _isNewComboActive = value;
                    IsNewComboActiveChanged?.Invoke(null, new PropertyChangedEventArgs(nameof(isNewComboActive)));
                }
            }
        }

        #endregion IsNewComboActive

        public static bool isWhistleActive = false;
        public static bool isFinishActive = false;
        public static bool isClapActive = false;
        public static bool isDistanceSnapActive = false;
        public static bool isGridSnapActive = false;

        public static float CS;

    }
}
