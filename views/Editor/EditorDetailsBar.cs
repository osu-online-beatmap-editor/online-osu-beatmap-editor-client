using online_osu_beatmap_editor_client.components.Container;
using online_osu_beatmap_editor_client.components;
using SFML.System;
using online_osu_beatmap_editor_client.common;

namespace online_osu_beatmap_editor_client.views.Editor
{
    public class EditorDetailsBar
    { 
        private UIContainer detailsBar;

        public EditorDetailsBar()
        {
            detailsBar = new UIContainer(new Vector2i(853 * 2 - 200, 40), new Vector2i(200, 480 * 2 - 40), 10, ContainerOrientation.Vertical);
        }

        public BaseUIComponent GetComponent()
        {
            return detailsBar;
        }
    }
}
