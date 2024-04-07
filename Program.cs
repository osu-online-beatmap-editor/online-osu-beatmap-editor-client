using System;
using online_osu_beatmap_editor_client.common;
using SFML.Graphics;
using SFML.Window;

namespace online_osu_beatmap_editor_client
{
    static class Program
    {
        static void OnClose(object sender, EventArgs e)
        {
            RenderWindow window = (RenderWindow)sender;
            window.Close();
        }

        static void Main()
        {
            RenderWindow app = new RenderWindow(new VideoMode(1920, 1080), "Online osu beatmap editor!");

            BaseUIComponent.SetWindow(app);

            app.Closed += new EventHandler(OnClose);

            Color windowColor = new Color(0, 0, 0);

            while (app.IsOpen)
            {
                app.DispatchEvents();

                app.Clear(windowColor);

                app.Display();
            }
        }
    }
}
