using System.Collections.Generic;
using client.Game.Core.Data;

namespace client.Game.Resources
{
    public static class IconsMapper
    {
        private static Dictionary<EditorTools, string> editorTools = new()
        {
            { EditorTools.SELECT, "select" },
            { EditorTools.CIRCLE, "circles" },
            { EditorTools.SLIDER, "sliders" },
            { EditorTools.SPINNER, "spinners" },
        };

        private static Dictionary<EditorToggleButtons, string> toggleTools = new()
        {
            { EditorToggleButtons.NEW_COMBO, "newCombo" },
            { EditorToggleButtons.WHISTLE, "whistle" },
            { EditorToggleButtons.FINISH, "finish" },
            { EditorToggleButtons.CLAP, "finish" }, //@TODO missing icon
            { EditorToggleButtons.DISTANCE_SNAP, "distanceSnap" },
            { EditorToggleButtons.GRID_SNAP, "gridSnap" },
        };

        public static string GetEditorToolIcon(EditorTools tool)
        {
            return editorTools[tool];
        }

        public static string GetEditorToggleIcon(EditorToggleButtons toggle)
        {
            return toggleTools[toggle];
        }
    }
}
