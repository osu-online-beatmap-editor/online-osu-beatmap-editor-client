using online_osu_beatmap_editor_client.common;
using online_osu_beatmap_editor_client.components;
using online_osu_beatmap_editor_client.components.Background;
using online_osu_beatmap_editor_client.components.Container;
using online_osu_beatmap_editor_client.config;
using SFML.Graphics;
using SFML.System;
using System;
using System.IO;
using System.Linq;

namespace online_osu_beatmap_editor_client.views.Editor
{
    internal class EditorView : BaseView
    {
        private EditorShortcuts editorShortcuts;

        private UIBackground background;
        private EditorNavBar navBar;
        private EditorToolBar toolBar;
        private EditorField editorField;

        public EditorView(RenderWindow window)
        {
            InitBackground();

            EditorData.gridType = EditorGridType.Large;
            EditorData.CS = 4;
            editorShortcuts = new EditorShortcuts();
            UIContainer mainContainer = new UIContainer(new Vector2i(0, 0), new Vector2i(853 * 2, 480 * 2), 0, ContainerOrientation.Vertical, new Color(1,1,1,0));

            AddComponent(mainContainer);
            InitNavBar(mainContainer); InitToolBar(mainContainer);
            InitEditorField();
        }

        private string GetRandomSeasonalBackground()
        {
            string path = AppConfig.osuFolderPath + @"\Data\bg";

            if (!Directory.Exists(path))
            {
                return null;
            }

            string[] imageFiles = Directory.GetFiles(path, "*.jpg");
            imageFiles = imageFiles.Concat(Directory.GetFiles(path, "*.png")).ToArray();

            if (imageFiles.Length > 0)
            {
                Random rand = new Random();
                int randomIndex = rand.Next(0, imageFiles.Length);

                return imageFiles[randomIndex];
            }
            else
            {
                return null;
            }
        }

        private void InitBackground()
        {
            string randomSeasonal = GetRandomSeasonalBackground();
            if (randomSeasonal != null)
            {
                background = new UIBackground(randomSeasonal);
                AddComponent(background);
            }
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
            editorField = new EditorField(new Vector2i(853 * 2 / 2, 480 * 2 / 2 + 30), editorShortcuts);

            AddComponent(editorField);
        }

        public override void Update()
        {
            base.Update();
            editorShortcuts.CheckShortcuts();
            toolBar.Update();
            navBar.Update();
            navBar.Draw();
        }
    }
}
