using System;
using System.Collections.Generic;
using client.Game.Core.Data;
using osu.Framework.Input.Events;
using osuTK.Input;

namespace client.Game.Core.Shortcuts
{
    public static class EditorShortcuts
    {
        private static Dictionary<Key, Action> editorShortcuts = new()
        {
            { Key.Number1, () => { EditorData.CurrentTool = EditorTools.SELECT; } },
            { Key.Number2, () => { EditorData.CurrentTool = EditorTools.CIRCLE; } },
            { Key.Number3, () => { EditorData.CurrentTool = EditorTools.SLIDER; } },
            { Key.Number4, () => { EditorData.CurrentTool = EditorTools.SPINNER; } },

            { Key.Q, () => { EditorData.SetToggleButtonState(EditorToggleButtons.NEW_COMBO, !EditorData.GetToggleButtonState(EditorToggleButtons.NEW_COMBO)); } },
            { Key.W, () => { EditorData.SetToggleButtonState(EditorToggleButtons.WHISTLE, !EditorData.GetToggleButtonState(EditorToggleButtons.WHISTLE)); } },
            { Key.E, () => { EditorData.SetToggleButtonState(EditorToggleButtons.FINISH, !EditorData.GetToggleButtonState(EditorToggleButtons.FINISH)); } },
            { Key.R, () => { EditorData.SetToggleButtonState(EditorToggleButtons.CLAP, !EditorData.GetToggleButtonState(EditorToggleButtons.CLAP)); } },
            { Key.T, () => { EditorData.SetToggleButtonState(EditorToggleButtons.DISTANCE_SNAP, !EditorData.GetToggleButtonState(EditorToggleButtons.DISTANCE_SNAP)); } },
            { Key.Y, () => { EditorData.SetToggleButtonState(EditorToggleButtons.GRID_SNAP, !EditorData.GetToggleButtonState(EditorToggleButtons.GRID_SNAP)); } },
        };


        public static bool OnKeyDown(KeyDownEvent e)
        {
            if (editorShortcuts.ContainsKey(e.Key))
            {
                editorShortcuts[e.Key].Invoke();
                return true;
            }

            return false;
        }
    }
}
