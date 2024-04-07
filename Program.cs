using System;
using online_osu_beatmap_editor_client.common;
using online_osu_beatmap_editor_client.components;
using online_osu_beatmap_editor_client.components.Button;
using online_osu_beatmap_editor_client.config;
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

        static void OnButtonClick(object sender, EventArgs e)
        {
            Console.WriteLine("Button clicked!");
        }


        static void Main()
        {
            RenderWindow app = new RenderWindow(new VideoMode(1920, 1080), "Online osu beatmap editor!");

            BaseUIComponent.SetWindow(app);

            app.Closed += new EventHandler(OnClose);

            Color windowColor = StyleVariables.colorBg;

            UIButton myButton = new UIButton("assets/icons/circle.png", 100, 100);
            myButton.Clicked += OnButtonClick;

            while (app.IsOpen)
            {
                app.DispatchEvents();

                app.Clear(windowColor);

                myButton.Draw();
                myButton.Update();

                app.Display();
            }
        }
    }
}
