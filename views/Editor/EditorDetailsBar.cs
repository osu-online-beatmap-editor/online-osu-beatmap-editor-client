using online_osu_beatmap_editor_client.components.Container;
using SFML.System;
using online_osu_beatmap_editor_client.common;
using online_osu_beatmap_editor_client.components.Slider;

namespace online_osu_beatmap_editor_client.views.Editor
{
    public class EditorDetailsBar
    { 
        private UIContainer detailsBar;

        public EditorDetailsBar()
        {
            detailsBar = new UIContainer(new Vector2i(853 * 2 - 200, 40), new Vector2i(200, 480 * 2 - 40), 10, ContainerOrientation.Vertical);

            UISlider slider = new UISlider(new Vector2i(0, 0), 180);

            detailsBar.AddElement(slider);
        }

        public BaseUIComponent GetComponent()
        {
            return detailsBar;
        }
    }
}
