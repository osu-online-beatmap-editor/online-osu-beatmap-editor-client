using online_osu_beatmap_editor_client.common;
using online_osu_beatmap_editor_client.components.Container;
using online_osu_beatmap_editor_client.config;
using online_osu_beatmap_editor_client.Engine;
using SFML.Graphics;

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
            editorShortcuts = new EditorShortcuts();
            UIContainer mainContainer = new UIContainer(0, 0, 1920, 1080, 0, ContainerOrientation.Vertical, StyleVariables.colorBg);

            AddComponent(mainContainer);

            InitNavBar(mainContainer);
            InitToolBar(mainContainer);
            InitEditorField();

            int wi = (int)(OsuMath.GetCircleWidthByCS(4) * 2.3f);   

            UIImage gg = new UIImage("assets/baseSkin/hitcircle.png", 150, 150, wi, wi);


            UIText ggg = new UIText("tgest", 150, 150, 40);

            AddComponent(gg);
            AddComponent(ggg);
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
            editorField = new EditorField(1920 / 2, 1080 / 2);

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
