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
        private UIContainer navBar;
        private UIContainer toolbar;

        public EditorView(RenderWindow window)
        {
            BaseUIComponent.SetWindow(window);
            this.GenerateNavBar();
            this.GenerateToolbar();

            HitCircle hc = new HitCircle(300, 300, 50);

            AddComponent(hc);
        }

        private void GenerateNavBar()
        {
            navBar = new UIContainer(0, 0, 1920, 50, 0, ContainerOrientation.Horizontal);

            UIButtonLabel button1 = new UIButtonLabel("File");
            UIButtonLabel button2 = new UIButtonLabel("Edit");
            UIButtonLabel button3 = new UIButtonLabel("View");

            button1.Clicked += (sender, e) => Console.WriteLine("Button 1 clicked!");
            button2.Clicked += (sender, e) => Console.WriteLine("Button 2 clicked!");
            button3.Clicked += (sender, e) => Console.WriteLine("Button 3 clicked!");

            navBar.AddElement(button1);
            navBar.AddElement(button2);
            navBar.AddElement(button3);

            AddComponent(navBar);
        }

        private void GenerateToolbar()
        {
            toolbar = new UIContainer(0, navBar.height, 95, 1080 - navBar.height, 10, ContainerOrientation.Vertical);

            UIButtonIcon button1 = new UIButtonIcon("assets/icons/circle.png");
            UIButtonIcon button2 = new UIButtonIcon("assets/icons/circle.png");
            UIButtonIcon button3 = new UIButtonIcon("assets/icons/circle.png");

            button1.Clicked += (sender, e) => Console.WriteLine("Button 1 clicked!");
            button2.Clicked += (sender, e) => Console.WriteLine("Button 2 clicked!");
            button3.Clicked += (sender, e) => Console.WriteLine("Button 3 clicked!");

            toolbar.AddElement(button1);
            toolbar.AddElement(button2);
            toolbar.AddElement(button3);

            AddComponent(toolbar);
        }
    }
}
