using online_osu_beatmap_editor_client.common;
using online_osu_beatmap_editor_client.components.Button;
using online_osu_beatmap_editor_client.components.Container;
using System;

namespace online_osu_beatmap_editor_client.views.Editor
{
    internal class EditorToolBar
    {
        private UIContainer toolbar;

        public EditorToolBar ()
        {
            toolbar = new UIContainer(0, 50, 95, 1080 - 50, 10, ContainerOrientation.Vertical);

            UIButtonIcon buttonSelect = new UIButtonIcon("assets/icons/select.png");
            UIButtonIcon buttonCircle = new UIButtonIcon("assets/icons/circle.png");
            UIButtonIcon buttonSlider = new UIButtonIcon("assets/icons/slider.png");
            UIButtonIcon buttonSpinner = new UIButtonIcon("assets/icons/spinner.png");

            buttonSelect.Clicked += (sender, e) => Console.WriteLine("Button 1 clicked!");
            buttonCircle.Clicked += (sender, e) => Console.WriteLine("Button 2 clicked!");
            buttonSlider.Clicked += (sender, e) => Console.WriteLine("Button 3 clicked!");
            buttonSpinner.Clicked += (sender, e) => Console.WriteLine("Button 3 clicked!");

            toolbar.AddElement(buttonSelect);
            toolbar.AddElement(buttonCircle);
            toolbar.AddElement(buttonSlider);
            toolbar.AddElement(buttonSpinner);
        }

        public BaseUIComponent GetComponent()
        {
            return toolbar;
        }
    }
}
