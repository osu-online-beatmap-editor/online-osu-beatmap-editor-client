using online_osu_beatmap_editor_client.common;
using online_osu_beatmap_editor_client.components;
using online_osu_beatmap_editor_client.components.Button;
using online_osu_beatmap_editor_client.components.Container;
using SFML.System;

namespace online_osu_beatmap_editor_client.views.Editor
{
    internal class EditorToolBar
    {
        private UIContainer toolbar;

        private UIButtonIcon buttonSelect;
        private UIButtonIcon buttonCircle;
        private UIButtonIcon buttonSlider;
        private UIButtonIcon buttonSpinner;

        private UISpacer toolBarSeparator;

        private UIButtonIcon buttonNewCombo;
        private UIButtonIcon buttonWhistle;
        private UIButtonIcon buttonFinish;
        private UIButtonIcon buttonClap;
        private UIButtonIcon buttonGridSnap;
        private UIButtonIcon buttonDistanceSnap;

        public EditorToolBar()
        {
            toolbar = new UIContainer(new Vector2i(0, 50), new Vector2i(95, 1080 - 50), 10, ContainerOrientation.Vertical);

            CreateToolButtons();

            toolBarSeparator = new UISpacer(new Vector2i(0, 0), new Vector2i(0, 50));
            toolbar.AddElement(toolBarSeparator);

            CreateToogleButtons();
        }

        private void CreateToolButtons()
        {
            buttonSelect = new UIButtonIcon("assets/icons/select.png");
            buttonCircle = new UIButtonIcon("assets/icons/circle.png");
            buttonSlider = new UIButtonIcon("assets/icons/slider.png");
            buttonSpinner = new UIButtonIcon("assets/icons/spinner.png");

            buttonSelect.Clicked += (sender, e) => EditorData.currentlySelectedEditorTool = EditorTools.Select;
            buttonCircle.Clicked += (sender, e) => EditorData.currentlySelectedEditorTool = EditorTools.Circle;
            buttonSlider.Clicked += (sender, e) => EditorData.currentlySelectedEditorTool = EditorTools.Slider;
            buttonSpinner.Clicked += (sender, e) => EditorData.currentlySelectedEditorTool = EditorTools.Spinner;

            var buttons = new[]
            {
                buttonSelect, buttonCircle, buttonSlider, buttonSpinner,
            };

            foreach (var button in buttons)
            {
                toolbar.AddElement(button);
            }
        }

        private void CreateToogleButtons()
        {
            buttonNewCombo = new UIButtonIcon("assets/icons/newCombo.png");
            buttonWhistle = new UIButtonIcon("assets/icons/whistle.png");
            buttonFinish = new UIButtonIcon("assets/icons/finish.png");
            buttonClap = new UIButtonIcon("assets/icons/finish.png"); //@TODO find clap icon
            buttonGridSnap = new UIButtonIcon("assets/icons/gridSnap.png");
            buttonDistanceSnap = new UIButtonIcon("assets/icons/distanceSnap.png");

            buttonNewCombo.Clicked += (sender, e) => EditorData.isNewComboActive = !EditorData.isNewComboActive;
            buttonWhistle.Clicked += (sender, e) => EditorData.isWhistleActive = !EditorData.isWhistleActive;
            buttonFinish.Clicked += (sender, e) => EditorData.isFinishActive = !EditorData.isFinishActive;
            buttonClap.Clicked += (sender, e) => EditorData.isClapActive = !EditorData.isClapActive;
            buttonGridSnap.Clicked += (sender, e) => EditorData.isGridSnapActive = !EditorData.isGridSnapActive;
            buttonDistanceSnap.Clicked += (sender, e) => EditorData.isDistanceSnapActive = !EditorData.isDistanceSnapActive;

            var buttons = new[]
            {
                buttonNewCombo, buttonWhistle, buttonFinish, buttonClap, buttonGridSnap, buttonDistanceSnap
            };

            foreach (var button in buttons)
            {
                toolbar.AddElement(button);
            }
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

            buttonNewCombo.isActive = EditorData.isNewComboActive;
            buttonWhistle.isActive = EditorData.isWhistleActive;
            buttonFinish.isActive = EditorData.isFinishActive;
            buttonClap.isActive = EditorData.isClapActive;
            buttonDistanceSnap.isActive = EditorData.isDistanceSnapActive;
            buttonGridSnap.isActive = EditorData.isGridSnapActive;
        }
    }
}
