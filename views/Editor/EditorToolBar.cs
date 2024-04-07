using online_osu_beatmap_editor_client.common;
using online_osu_beatmap_editor_client.components.Button;
using online_osu_beatmap_editor_client.components.Container;
using online_osu_beatmap_editor_client.config;
using SFML.Graphics;
using System;

namespace online_osu_beatmap_editor_client.views.Editor
{
    internal class EditorToolBar
    {
        private UIContainer toolbar;

        public EditorToolBar ()
        {
            toolbar = new UIContainer(0, 50, 95, 1080 - 50, 10, ContainerOrientation.Vertical);

            UIButtonIcon button1 = new UIButtonIcon("assets/icons/circle.png");
            UIButtonIcon button2 = new UIButtonIcon("assets/icons/circle.png");
            UIButtonIcon button3 = new UIButtonIcon("assets/icons/circle.png");

            button1.Clicked += (sender, e) => Console.WriteLine("Button 1 clicked!");
            button2.Clicked += (sender, e) => Console.WriteLine("Button 2 clicked!");
            button3.Clicked += (sender, e) => Console.WriteLine("Button 3 clicked!");

            toolbar.AddElement(button1);
            toolbar.AddElement(button2);
            toolbar.AddElement(button3);
        }

        public BaseUIComponent GetComponent()
        {
            return toolbar;
        }
    }
}
