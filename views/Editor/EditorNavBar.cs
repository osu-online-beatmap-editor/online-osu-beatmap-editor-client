using online_osu_beatmap_editor_client.common;
using online_osu_beatmap_editor_client.components.Button;
using online_osu_beatmap_editor_client.components.Container;
using online_osu_beatmap_editor_client.components.Slider;
using online_osu_beatmap_editor_client.config;
using online_osu_beatmap_editor_client.Engine;
using SFML.System;
using System;
using System.Windows.Forms;

namespace online_osu_beatmap_editor_client.views.Editor
{
    internal class EditorNavBar
    {
        private UIContainer navBar;

        private UIContainer fileTab;
        private UIContainer editTab;
        private UIContainer gridTab;
        private UIContainer bacgrkoundDimTab;
        private UIContainer beatmapSettingsTab;

        private EditorNavTab currentTab = EditorNavTab.None;

        public EditorNavBar ()
        {
            navBar = new UIContainer(new Vector2i(0, 0), new Vector2i(853 * 2, 40), 0, ContainerOrientation.Horizontal);
            Console.WriteLine(navBar.origin);

            UIButtonLabel buttonFile = new UIButtonLabel("File");
            UIButtonLabel buttonEdit = new UIButtonLabel("Edit");
            UIButtonLabel buttonView = new UIButtonLabel("View");
            UIButtonLabel buttonTiming = new UIButtonLabel("Timing");
            UIButtonLabel buttonGrid = new UIButtonLabel("Grid");
            UIButtonLabel buttonBackgroundDim = new UIButtonLabel("Background dim");
            UIButtonLabel buttonBeatmapSettings = new UIButtonLabel("Beatmap settings");

            navBar.AddElement(buttonFile);
            navBar.AddElement(buttonEdit);
            navBar.AddElement(buttonView);
            navBar.AddElement(buttonTiming);
            navBar.AddElement(buttonGrid);
            navBar.AddElement(buttonBackgroundDim);
            navBar.AddElement(buttonBeatmapSettings);

            CreateMenuItemsForFileTab(buttonFile);
            CreateMenuItemsForEditTab(buttonEdit);
            CreateMenuItemsForGridTab(buttonGrid);
            CreateMenuItemsForBackgroundDim(buttonBackgroundDim);
            CreateMenuItemsForBeatmapSettings(buttonBeatmapSettings);

            buttonFile.Clicked += (sender, e) => HandleTabButtonClick(EditorNavTab.File);
            buttonEdit.Clicked += (sender, e) => HandleTabButtonClick(EditorNavTab.Edit);
            buttonView.Clicked += (sender, e) => HandleTabButtonClick(EditorNavTab.View);
            buttonTiming.Clicked += (sender, e) => HandleTabButtonClick(EditorNavTab.Timing);
            buttonGrid.Clicked += (sender, e) => HandleTabButtonClick(EditorNavTab.Grid);
            buttonBackgroundDim.Clicked += (sender, e) => HandleTabButtonClick(EditorNavTab.BackgroundDim);
            buttonBeatmapSettings.Clicked += (sender, e) => HandleTabButtonClick(EditorNavTab.BeatmapSettings);
        }

        private void HandleTabButtonClick (EditorNavTab tab)
        {
            currentTab = tab == currentTab ? EditorNavTab.None : tab;
        }

        private void LoadBeatmapFromFile()
        {
            BeatmapLoader bl = new BeatmapLoader();
            bl.OpenBeatmapLoadingDialog();
        }

        private void CreateMenuItemsForFileTab(UIButtonLabel buttonFile)
        {
            fileTab = new UIContainer(buttonFile.pos + new Vector2i(0, 40), new Vector2i(0, 0), 2, ContainerOrientation.Vertical, StyleVariables.colorBgTertiary);

            UIButtonLabel buttonSave = new UIButtonLabel("Open beatmap");

            buttonSave.Clicked += (sender, e) => { LoadBeatmapFromFile(); };

            var buttons = new[]
            {
                buttonSave
            };

            foreach (var button in buttons)
            {
                button.Clicked += (sender, e) => currentTab = EditorNavTab.None;
                fileTab.AddElement(button);
            }
        }

        private void CreateMenuItemsForEditTab(UIButtonLabel buttonEdit)
        {
            editTab = new UIContainer(buttonEdit.pos + new Vector2i(0, 40), new Vector2i(0, 0), 2, ContainerOrientation.Vertical, StyleVariables.colorBgTertiary);

            UIButtonLabel buttonUndo = new UIButtonLabel("Undo");

            buttonUndo.Clicked += (sender, e) => { Console.WriteLine("Undo"); };

            var buttons = new[]
            {
                buttonUndo
            };

            foreach (var button in buttons)
            {
                button.Clicked += (sender, e) => currentTab = EditorNavTab.None;
                editTab.AddElement(button);
            }
        }

        private void CreateMenuItemsForGridTab (UIButtonLabel buttonGrid)
        {
            gridTab = new UIContainer(buttonGrid.pos + new Vector2i(0, 40), new Vector2i(0, 0), 2, ContainerOrientation.Vertical, StyleVariables.colorBgTertiary);

            UIButtonLabel buttonGridLarge = new UIButtonLabel("Large (x128)");
            UIButtonLabel buttonGridBig = new UIButtonLabel("Big (X64)");
            UIButtonLabel buttonGridMedium = new UIButtonLabel("Medium (x32)");
            UIButtonLabel buttonGridSmall = new UIButtonLabel("Small (x16)");

            buttonGridLarge.Clicked += (sender, e) => EditorData.gridType = EditorGridType.Large;
            buttonGridBig.Clicked += (sender, e) => EditorData.gridType = EditorGridType.Big;
            buttonGridMedium.Clicked += (sender, e) => EditorData.gridType = EditorGridType.Medium;
            buttonGridSmall.Clicked += (sender, e) => EditorData.gridType = EditorGridType.Small;

            var buttons = new[]
            {
                buttonGridLarge, buttonGridBig, buttonGridMedium, buttonGridSmall,
            };

            foreach (var button in buttons)
            {
                button.Clicked += (sender, e) => currentTab = EditorNavTab.None;
                gridTab.AddElement(button);
            }
        }

        private void CreateMenuItemsForBackgroundDim(UIButtonLabel buttonBackgroundDim)
        {
            bacgrkoundDimTab = new UIContainer(buttonBackgroundDim.pos + new Vector2i(0, 40), new Vector2i(0, 0), 10, ContainerOrientation.Vertical, StyleVariables.colorBg);

            UIText sliderLabel = new UIText("Background Dim", new Vector2i(0, 0));

            UISlider slider = new UISlider(new Vector2i(0, 0), 160);
            slider.sliderValue = EditorData.backgroundDim;
            slider.ValueChanged += (sender, e) => { EditorData.backgroundDim = slider.sliderValue; };

            bacgrkoundDimTab.AddElement(sliderLabel);
            bacgrkoundDimTab.AddElement(slider);
        }

        private void CreateMenuItemsForBeatmapSettings(UIButtonLabel buttonBeatmapSettings)
        {
            beatmapSettingsTab = new UIContainer(buttonBeatmapSettings.pos + new Vector2i(0, 40), new Vector2i(0, 0), 10, ContainerOrientation.Vertical, StyleVariables.colorBg);

            BaseUIComponent sliderHPLabel = new UIText("HP Drain Rate", new Vector2i(0, 0));
            UISlider sliderHP = new UISlider(new Vector2i(0, 0), 160);

            UIText sliderCircleSizeLabel = new UIText("Circle Size", new Vector2i(0, 0));
            UISlider sliderCircleSize = new UISlider(new Vector2i(0, 0), 160);

            UIText sliderApproachRateLabel = new UIText("Approach Rate", new Vector2i(0, 0));
            UISlider sliderApproachRate = new UISlider(new Vector2i(0, 0), 160);

            UIText sliderOverallDifficultyLabel = new UIText("HP Drain Rate", new Vector2i(0, 0));
            UISlider sliderOverallDifficulty = new UISlider(new Vector2i(0, 0), 160);


            var elements = new[]
            {
                sliderHPLabel, sliderHP, sliderCircleSizeLabel, sliderCircleSize,sliderApproachRateLabel,sliderApproachRate,sliderOverallDifficultyLabel,sliderOverallDifficulty
            };

            foreach (var button in elements)
            {
                beatmapSettingsTab.AddElement(button);
            }
        }

        public BaseUIComponent GetComponent()
        {
            return navBar;
        }

        public void Draw()
        {
            if (currentTab == EditorNavTab.File) { fileTab.Draw(); }
            if (currentTab == EditorNavTab.Edit) { editTab.Draw(); }
            if (currentTab == EditorNavTab.Grid) { gridTab.Draw(); }
            if (currentTab == EditorNavTab.BackgroundDim) { bacgrkoundDimTab.Draw(); }
            if (currentTab == EditorNavTab.BeatmapSettings) { beatmapSettingsTab.Draw(); }
        }

        public void Update()
        {
            if (currentTab == EditorNavTab.File) { fileTab.Update(); }
            if (currentTab == EditorNavTab.Edit) { editTab.Update(); }
            if (currentTab == EditorNavTab.Grid) { gridTab.Update(); }
            if (currentTab == EditorNavTab.BackgroundDim) { bacgrkoundDimTab.Update(); }
            if (currentTab == EditorNavTab.BeatmapSettings) { beatmapSettingsTab.Update(); }
        }
    }
}
