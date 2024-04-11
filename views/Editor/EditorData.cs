using online_osu_beatmap_editor_client.components;
using System;
using System.ComponentModel;

namespace online_osu_beatmap_editor_client.views.Editor
{
    static class EditorData
    {
        public static EditorTools currentlySelectedEditorTool;

        #region GridType

        private static EditorGridType _gridType;

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

        private static float _backgroundDim;

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

        #region DistanceSnapping

        private static float _distanceSnapping;

        public static event PropertyChangedEventHandler DistanceSnappingChanged;
        public static float distanceSnapping
        {
            get { return _distanceSnapping; }
            set
            {
                if (_distanceSnapping != value)
                {
                    _distanceSnapping = value;
                    DistanceSnappingChanged?.Invoke(null, new PropertyChangedEventArgs(nameof(distanceSnapping)));
                }
            }
        }

        #endregion DistanceSnapping

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

        #region SelectedCircle

        private static HitCircle _selectedCircle;

        public static event PropertyChangedEventHandler SelectedCircleChanged;
        public static HitCircle selectedCircle
        {
            get { return _selectedCircle; }
            set
            {
                if (_selectedCircle != value)
                {
                    _selectedCircle = value;
                    SelectedCircleChanged?.Invoke(null, new PropertyChangedEventArgs(nameof(selectedCircle)));
                }
            }
        }

        #endregion SelectedCircle

        private static int _currentTime;
        public static event PropertyChangedEventHandler CurrentTimeChanged;
        public static int currentTime
        {
            get { return _currentTime; }
            set
            {
                if (_currentTime != value)
                {
                    _currentTime = value <= 15 ? 15 : value;
                    CurrentTimeChanged?.Invoke(null, new PropertyChangedEventArgs(nameof(currentTime)));
                }
            }
        }

        public static int totalTime = 342523;

        public static bool isWhistleActive = false;
        public static bool isFinishActive = false;
        public static bool isClapActive = false;
        public static bool isDistanceSnapActive = false;
        public static bool isGridSnapActive = false;

        public static float CS;

    }
}
