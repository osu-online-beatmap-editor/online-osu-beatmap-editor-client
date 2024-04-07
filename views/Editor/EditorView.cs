using online_osu_beatmap_editor_client.common;
using online_osu_beatmap_editor_client.components.Button;
using SFML.Graphics;
using System;

namespace online_osu_beatmap_editor_client.views.Editor
{
    internal class EditorView : BaseView
    {
        public EditorView(RenderWindow window)
        {
            BaseUIComponent.SetWindow(window);

            UIButtonIcon button1 = new UIButtonIcon("assets/icons/circle.png", 100, 100);
            UIButtonIcon button2 = new UIButtonIcon("assets/icons/circle.png", 250, 100);
            UIButtonIcon button3 = new UIButtonIcon("assets/icons/circle.png", 400, 100);

            button1.Clicked += (sender, e) => Console.WriteLine("Button 1 clicked!");
            button2.Clicked += (sender, e) => Console.WriteLine("Button 2 clicked!");
            button3.Clicked += (sender, e) => Console.WriteLine("Button 3 clicked!");

            AddComponent(button1);
            AddComponent(button2);
            AddComponent(button3);
        }
    }
}
