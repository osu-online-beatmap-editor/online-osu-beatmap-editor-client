using SFML.Graphics;
using SFML.System;

namespace online_osu_beatmap_editor_client.views.Editor
{
    public static class EditorHelper
    {
        public static Vector2i CalculateCirclePosition(Vector2i pos, Vector2i size, Vector2i clickPoint, float scale) {
            Vector2i newHitCirlcePos = new Vector2i(pos.X - size.X / 2 + (int)(clickPoint.X * scale), pos.Y - size.Y / 2 + (int)(clickPoint.Y * scale));
            return newHitCirlcePos;
        }

        public static Vector2i GetRawClickPosOnField(Vector2i mousePosition, Vector2i pos, Vector2i size)
        {
            Vector2i clickPos = new Vector2i(mousePosition.X - pos.X + size.X / 2, mousePosition.Y - pos.Y + size.Y / 2);
            return clickPos;
        }

        public static Vector2i GetUnscaledClickPosOnField(Vector2i rawClickPoint, float scale)
        {
            Vector2i clickPos = new Vector2i((int)(rawClickPoint.X / scale), (int)(rawClickPoint.Y / scale));
            return clickPos;
        }
    }
}
