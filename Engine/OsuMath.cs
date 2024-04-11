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

        // THERE'S NO FUCKING CLAMP FUNCTION IN .NET FRAMEWORK
        public static int Clamp(int value, int min, int max)
        {
            return (value < min) ? min : (value > max) ? max : value;
        }

        public static float GetDistanceBetweenWhiteTimingTicks(float bpm)
        {
            return 60000f / bpm;
        }

        public static float GetDistanceBetweenTimingTicksInMilliseconds(float bpm, int currentSnapping)
        {
            float distanceBetweenWhiteTicks = GetDistanceBetweenWhiteTimingTicks(bpm);
            return distanceBetweenWhiteTicks / currentSnapping;
        }

        public static double Lerp(double a, double b, double t) => t < 0 ? a : (t > 1 ? b : a + (b - a) * t);

        public static double RemapNumbers(double x, double x_min, double x_max, double y_min, double y_max)
        {
            double normalized_x = (x - x_min) / (x_max - x_min);
            double y = y_min + normalized_x * (y_max - y_min);

            return y;
        }
    }
}
