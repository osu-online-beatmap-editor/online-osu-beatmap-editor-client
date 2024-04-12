using System;
using System.Diagnostics;
using online_osu_beatmap_editor_client.common;
using online_osu_beatmap_editor_client.config;
using online_osu_beatmap_editor_client.Engine.GameplayElements.Objects;
using online_osu_beatmap_editor_client.views.Editor;
using SFML.Graphics;
using SFML.System;
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

        [STAThread]
        static void Main()
        {
            new AppConfigParser();

            RenderWindow app = new RenderWindow(new VideoMode(853 * 2, 480 * 2), "Online osu beatmap editor!");

            BaseUIComponent.SetWindow(app);

            app.Closed += new EventHandler(OnClose);

            Color windowColor = new Color(15, 15, 15);

            View viewPort = new View(new FloatRect(0, 0, 853 * 2, 480 * 2));
            app.SetView(viewPort);

            BaseView view = new EditorView(app);

            while (app.IsOpen)
            {
                app.DispatchEvents();

                app.Clear(windowColor);

                view.Draw();
                view.Update();

                Vector2i mousePosition = Mouse.GetPosition(app);
                Vector2f worldMousePosition = app.MapPixelToCoords(mousePosition);

                app.Display();
            }
        }
    }
}
