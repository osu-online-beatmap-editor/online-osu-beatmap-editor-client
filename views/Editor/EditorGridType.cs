using System.Collections.Generic;

namespace online_osu_beatmap_editor_client.views.Editor
{
    public enum EditorGridType
    {
        Large,
        Big,
        Medium,
        Small

    }

    public static class GridTypeMapper
    {
        private static Dictionary<EditorGridType, int> gridTypeMapping = new Dictionary<EditorGridType, int>()
        {
            { EditorGridType.Large, 16 },
            { EditorGridType.Big, 32 },
            { EditorGridType.Medium, 64 },
            { EditorGridType.Small, 128 }
        };

        public static int GetGridValue(EditorGridType gridType)
        {
            return gridTypeMapping[gridType];
        }
    }
}
