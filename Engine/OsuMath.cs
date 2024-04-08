namespace online_osu_beatmap_editor_client.Engine
{
    public static class OsuMath
    {
        // https://osu.ppy.sh/wiki/en/Beatmap/Circle_size
        public static float GetCircleRadiusByCS(float CS)
        {
            float r = 54.4f - 4.48f * CS;

            return r;
        }

        public static int GetCircleWidthByCS(float CS)
        {
            float radius = GetCircleRadiusByCS(CS);
            int width = (int)radius * 2;

            return width;
        }
    }
}
