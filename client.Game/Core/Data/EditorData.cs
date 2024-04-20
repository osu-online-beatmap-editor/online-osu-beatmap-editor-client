using System.Collections.Generic;
using System.ComponentModel;

namespace client.Game.Core.Data
{
    public static class EditorData
    {
        #region Tool

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

        #endregion Tool

        #region Toggle

        private static Dictionary<EditorToggleButtons, bool> _ToggleButtonStates = new()
        {
            { EditorToggleButtons.NEW_COMBO, false },
            { EditorToggleButtons.WHISTLE, false },
            { EditorToggleButtons.FINISH, false },
            { EditorToggleButtons.CLAP, false },
            { EditorToggleButtons.DISTANCE_SNAP, false },
            { EditorToggleButtons.GRID_SNAP, false },
        };

        public static event PropertyChangedEventHandler ToggleButtonStatesChanged;

        public static bool GetToggleButtonState(EditorToggleButtons button)
        {
            if (_ToggleButtonStates.ContainsKey(button))
            {
                return _ToggleButtonStates[button];
            }
            return false;
        }

        public static void SetToggleButtonState(EditorToggleButtons button, bool state)
        {
            if (_ToggleButtonStates.ContainsKey(button))
            {
                _ToggleButtonStates[button] = state;
                ToggleButtonStatesChanged?.Invoke(null, new PropertyChangedEventArgs(nameof(_ToggleButtonStates)));
            }
        }

        public static bool IsToggleActive(EditorToggleButtons toggle)
        {
            _ToggleButtonStates.TryGetValue(toggle, out bool active);
            return active;
        }

        #endregion Toggle
    }
}
