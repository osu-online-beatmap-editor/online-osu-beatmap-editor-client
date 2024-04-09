using online_osu_beatmap_editor_client.common;
using online_osu_beatmap_editor_client.components.Button;
using online_osu_beatmap_editor_client.components.Container;
using online_osu_beatmap_editor_client.config;
using SFML.System;
using System;

namespace online_osu_beatmap_editor_client.views.Editor
{
    internal class EditorNavBar
    {
        private UIContainer navBar;

        private UIContainer fileTab;
        private UIContainer editTab;
        private UIContainer gridTab;

        private EditorNavTab currentTab = EditorNavTab.None;

        public EditorNavBar ()
        {
            navBar = new UIContainer(new Vector2i(0, 0), new Vector2i(640 * 2, 40), 0, ContainerOrientation.Horizontal);
            Console.WriteLine(navBar.origin);

            UIButtonLabel buttonFile = new UIButtonLabel("File");
            UIButtonLabel buttonEdit = new UIButtonLabel("Edit");
            UIButtonLabel buttonView = new UIButtonLabel("View");
            UIButtonLabel buttonTiming = new UIButtonLabel("Timing");
            UIButtonLabel buttonGrid = new UIButtonLabel("Grid");

            navBar.AddElement(buttonFile);
            navBar.AddElement(buttonEdit);
            navBar.AddElement(buttonView);
            navBar.AddElement(buttonTiming);
            navBar.AddElement(buttonGrid);

            CreateMenuItemsForFileTab(buttonFile);
            CreateMenuItemsForEditTab(buttonEdit);
            CreateMenuItemsForGridTab(buttonGrid);

            buttonFile.Clicked += (sender, e) => HandleTabButtonClick(EditorNavTab.File);
            buttonEdit.Clicked += (sender, e) => HandleTabButtonClick(EditorNavTab.Edit);
            buttonView.Clicked += (sender, e) => HandleTabButtonClick(EditorNavTab.View);
            buttonTiming.Clicked += (sender, e) => HandleTabButtonClick(EditorNavTab.Timing);
            buttonGrid.Clicked += (sender, e) => HandleTabButtonClick(EditorNavTab.Grid);
        }

        private void HandleTabButtonClick (EditorNavTab tab)
        {
            currentTab = tab == currentTab ? EditorNavTab.None : tab;
        }

        private void CreateMenuItemsForFileTab(UIButtonLabel buttonFile)
        {
            fileTab = new UIContainer(buttonFile.pos + new Vector2i(0, 40), new Vector2i(0, 0), 2, ContainerOrientation.Vertical, StyleVariables.colorBgTertiary);

            UIButtonLabel buttonSave = new UIButtonLabel("Save");

            buttonSave.Clicked += (sender, e) => { Console.WriteLine("Save"); };

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

        public BaseUIComponent GetComponent()
        {
            return navBar;
        }

        public void Draw()
        {
            if (currentTab == EditorNavTab.File) { fileTab.Draw(); }
            if (currentTab == EditorNavTab.Edit) { editTab.Draw(); }
            if (currentTab == EditorNavTab.Grid) { gridTab.Draw(); }
        }

        public void Update()
        {
            if (currentTab == EditorNavTab.File) { fileTab.Update(); }
            if (currentTab == EditorNavTab.Edit) { editTab.Update(); }
            if (currentTab == EditorNavTab.Grid) { gridTab.Update(); }
        }
    }
}
