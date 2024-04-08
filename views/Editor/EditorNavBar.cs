using online_osu_beatmap_editor_client.common;
using online_osu_beatmap_editor_client.components;
using online_osu_beatmap_editor_client.components.Button;
using online_osu_beatmap_editor_client.components.Container;
using SFML.Graphics;
using SFML.System;
using System;

namespace online_osu_beatmap_editor_client.views.Editor
{
    internal class EditorNavBar
    {
        private UIContainer navBar;

        public EditorNavBar ()
        {
            navBar = new UIContainer(new Vector2i(0, 0), new Vector2i(1920, 50), 0, ContainerOrientation.Horizontal);
            Console.WriteLine(navBar.origin);

            UIButtonLabel button1 = new UIButtonLabel("File");
            UIButtonLabel button2 = new UIButtonLabel("Edit");
            UIButtonLabel button3 = new UIButtonLabel("View");
            UIButtonLabel button4 = new UIButtonLabel("Timing");

            button1.Clicked += (sender, e) => Console.WriteLine("Button 1 clicked!");
            button2.Clicked += (sender, e) => Console.WriteLine("Button 2 clicked!");
            button3.Clicked += (sender, e) => Console.WriteLine("Button 3 clicked!");
            button4.Clicked += (sender, e) => Console.WriteLine("Button 4 clicked!");

            navBar.AddElement(button1);
            navBar.AddElement(button2);
            navBar.AddElement(button3);
            navBar.AddElement(button4);
        }

        public BaseUIComponent GetComponent()
        {
            return navBar;
        }
    }
}
