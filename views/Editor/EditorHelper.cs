using SFML.System;

namespace online_osu_beatmap_editor_client.views.Editor
{
    public static class EditorHelper
    {
        public static Vector2i CalculateCirclePosition(Vector2i pos, Vector2i size, Vector2i clickPoint, float scale) {
            Vector2i newHitCirlcePos = new Vector2i(pos.X - size.X / 2 + (int)(clickPoint.X * scale), pos.Y - size.Y / 2 + (int)(clickPoint.Y * scale));
            return newHitCirlcePos;
        }
    }
}
