using online_osu_beatmap_editor_client.common;
using online_osu_beatmap_editor_client.components;
using online_osu_beatmap_editor_client.components.Button;
using online_osu_beatmap_editor_client.components.Container;
using SFML.Graphics;
using System;
using System.ComponentModel;

namespace online_osu_beatmap_editor_client.views.Editor
{
    internal class EditorView : BaseView
    {
        private UIContainer navBar;
        private EditorToolBar toolBar;

        public EditorView(RenderWindow window)
        {
            BaseUIComponent.SetWindow(window);
            this.GenerateNavBar();
            InitToolBar();



            HitCircle hc = new HitCircle(300, 300, 50);

            AddComponent(hc);
        }

        private void GenerateNavBar()
        {
            navBar = new UIContainer(0, 0, 1920, 50, 0, ContainerOrientation.Horizontal);

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

            AddComponent(navBar);
        }

        private void InitToolBar()
        {
            toolBar = new EditorToolBar();
            BaseUIComponent toolBarComponent = toolBar.GetComponent();
            AddComponent(toolBarComponent);
        }
    }
}
