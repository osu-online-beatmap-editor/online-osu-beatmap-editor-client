using osu.Framework.Platform;
using osu.Framework;
using client.Game;

namespace client.Desktop
{
    public static class Program
    {
        public static void Main()
        {
            using (GameHost host = Host.GetSuitableDesktopHost(@"client"))
            using (osu.Framework.Game game = new clientGame())
                host.Run(game);
        }
    }
}
