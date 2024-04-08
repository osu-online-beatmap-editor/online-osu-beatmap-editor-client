using SFML.System;

namespace online_osu_beatmap_editor_client.views.Editor
{
    public static class EditorHelper
    {
        public static Vector2i CalculateCirclePosition(int posX, int posY, int width, int height, Vector2i clickPoint, float scale) {
            Vector2i newHitCirlcePos = new Vector2i(posX - width / 2 + (int)(clickPoint.X * scale), posY - height / 2 + (int)(clickPoint.Y * scale));
            return newHitCirlcePos;
        }
    }
}
