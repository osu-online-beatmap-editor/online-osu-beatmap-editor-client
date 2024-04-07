using online_osu_beatmap_editor_client.common;
using online_osu_beatmap_editor_client.components;
using online_osu_beatmap_editor_client.components.Button;
using online_osu_beatmap_editor_client.components.Container;
using SFML.Graphics;
using System;

namespace online_osu_beatmap_editor_client.views.Editor
{
    internal class EditorView : BaseView
    {
        public EditorView(RenderWindow window)
        {
            BaseUIComponent.SetWindow(window);
            this.GenerateToolbar();

            HitCircle hc = new HitCircle(300, 300, 50);

            AddComponent(hc);
        }

        private void GenerateToolbar()
        {
            UIVerticalContainer verticalContainer = new UIVerticalContainer(0, 0, 95, 1080, 10);

            UIButtonIcon button1 = new UIButtonIcon("assets/icons/circle.png");
            UIButtonIcon button2 = new UIButtonIcon("assets/icons/circle.png");
            UIButtonIcon button3 = new UIButtonIcon("assets/icons/circle.png");

            button1.Clicked += (sender, e) => Console.WriteLine("Button 1 clicked!");
            button2.Clicked += (sender, e) => Console.WriteLine("Button 2 clicked!");
            button3.Clicked += (sender, e) => Console.WriteLine("Button 3 clicked!");

            verticalContainer.AddElement(button1);
            verticalContainer.AddElement(button2);
            verticalContainer.AddElement(button3);

            AddComponent(verticalContainer);
        }
    }
}
