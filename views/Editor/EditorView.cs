using online_osu_beatmap_editor_client.common;
using online_osu_beatmap_editor_client.components;
using online_osu_beatmap_editor_client.components.Container;
using online_osu_beatmap_editor_client.config;
using SFML.Graphics;
using SFML.System;

namespace online_osu_beatmap_editor_client.views.Editor
{
    internal class EditorView : BaseView
    {
        private EditorShortcuts editorShortcuts;

        private EditorNavBar navBar;
        private EditorToolBar toolBar;
        private EditorField editorField;

        public EditorView(RenderWindow window)
        {
            EditorData.gridType = EditorGridType.Large;
            EditorData.CS = 4;
            editorShortcuts = new EditorShortcuts();
            UIContainer mainContainer = new UIContainer(new Vector2i(0, 0), new Vector2i(1920, 1080), 0, ContainerOrientation.Vertical, StyleVariables.colorBg);

            AddComponent(mainContainer);

            InitNavBar(mainContainer);
            InitToolBar(mainContainer);
            InitEditorField();
        }

        private void InitNavBar(UIContainer mainContainer)
        {
            navBar = new EditorNavBar();
            BaseUIComponent navBarComponent = navBar.GetComponent();
            mainContainer.AddElement(navBarComponent);
        }

        private void InitToolBar(UIContainer mainContainer)
        {
            toolBar = new EditorToolBar();
            BaseUIComponent toolBarComponent = toolBar.GetComponent();
            mainContainer.AddElement(toolBarComponent);
        }

        private void InitEditorField ()
        {
            editorField = new EditorField(new Vector2i(1920 / 2, 1080 / 2));

            AddComponent(editorField);
        }

        public override void Update()
        {
            base.Update();
            editorShortcuts.CheckShortcuts();
            toolBar.Update();
        }
    }
}
