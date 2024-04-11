namespace online_osu_beatmap_editor_client.Engine
{
    public class TimeConverter
    {
        public static string MillisecondsToTime(long milliseconds)
        {
            long minutes = milliseconds / (60 * 1000);
            long seconds = (milliseconds / 1000) % 60;
            long remainingMilliseconds = milliseconds % 1000;

            return string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, remainingMilliseconds);
        }
    }
}
