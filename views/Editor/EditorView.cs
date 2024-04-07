using online_osu_beatmap_editor_client.common;
using online_osu_beatmap_editor_client.components;
using online_osu_beatmap_editor_client.components.Container;
using online_osu_beatmap_editor_client.config;
using SFML.Graphics;

namespace online_osu_beatmap_editor_client.views.Editor
{
    internal class EditorView : BaseView
    {
        private EditorNavBar navBar;
        private EditorToolBar toolBar;

        public EditorView(RenderWindow window)
        {
            UIContainer mainContainer = new UIContainer(0, 0, 1920, 1080, 0, ContainerOrientation.Vertical, StyleVariables.colorBg);

            AddComponent(mainContainer);

            InitNavBar(mainContainer);
            InitToolBar(mainContainer);

            HitCircle hc = new HitCircle(300, 300, 50);

            AddComponent(hc);
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
    }
}
