using online_osu_beatmap_editor_client.common;
using online_osu_beatmap_editor_client.components.Button;
using online_osu_beatmap_editor_client.components.Container;
using System;

namespace online_osu_beatmap_editor_client.views.Editor
{
    internal class EditorToolBar
    {
        private UIContainer toolbar;


        private UIButtonIcon buttonSelect;
        private UIButtonIcon buttonCircle;
        private UIButtonIcon buttonSlider;
        private UIButtonIcon buttonSpinner;

        public EditorToolBar ()
        {
            toolbar = new UIContainer(0, 50, 95, 1080 - 50, 10, ContainerOrientation.Vertical);

            buttonSelect = new UIButtonIcon("assets/icons/select.png");
            buttonCircle = new UIButtonIcon("assets/icons/circle.png");
            buttonSlider = new UIButtonIcon("assets/icons/slider.png");
            buttonSpinner = new UIButtonIcon("assets/icons/spinner.png");

            buttonSelect.Clicked += (sender, e) => EditorData.currentlySelectedEditorTool = EditorTools.Select;
            buttonCircle.Clicked += (sender, e) => EditorData.currentlySelectedEditorTool = EditorTools.Circle;
            buttonSlider.Clicked += (sender, e) => EditorData.currentlySelectedEditorTool = EditorTools.Slider;
            buttonSpinner.Clicked += (sender, e) => EditorData.currentlySelectedEditorTool = EditorTools.Spinner;

            toolbar.AddElement(buttonSelect);
            toolbar.AddElement(buttonCircle);
            toolbar.AddElement(buttonSlider);
            toolbar.AddElement(buttonSpinner);
        }

        public BaseUIComponent GetComponent()
        {
            return toolbar;
        }

        public void Update()
        {
            buttonSelect.isActive = EditorData.currentlySelectedEditorTool == EditorTools.Select;
            buttonCircle.isActive = EditorData.currentlySelectedEditorTool == EditorTools.Circle;
            buttonSlider.isActive = EditorData.currentlySelectedEditorTool == EditorTools.Slider;
            buttonSpinner.isActive = EditorData.currentlySelectedEditorTool == EditorTools.Spinner;
        }
    }
}
