using online_osu_beatmap_editor_client.components.Container;
using SFML.System;
using online_osu_beatmap_editor_client.common;
using online_osu_beatmap_editor_client.components.Slider;
using System;
using online_osu_beatmap_editor_client.config;

namespace online_osu_beatmap_editor_client.views.Editor
{
    public class EditorDetailsBar
    { 
        private UIContainer detailsBar;

        public EditorDetailsBar()
        {
            detailsBar = new UIContainer(new Vector2i(853 * 2 - 200, 40), new Vector2i(200, 480 * 2 - 40), 10, ContainerOrientation.Vertical, StyleVariables.colorBg);


            UIContainer backgroundDimContainer = new UIContainer(new Vector2i(0, 0), new Vector2i(0, 0), 10, ContainerOrientation.Vertical, StyleVariables.colorBgSecondary);

            UIText sliderLabel = new UIText("Background dim", new Vector2i(0, 0));

            backgroundDimContainer.AddElement(sliderLabel);

            UISlider slider = new UISlider(new Vector2i(0, 0), 160);
            slider.sliderValue = EditorData.backgroundDim;
            slider.ValueChanged += (sender, e) => { EditorData.backgroundDim = slider.sliderValue; };

            backgroundDimContainer.AddElement(slider);

            detailsBar.AddElement(backgroundDimContainer);
        }

        public BaseUIComponent GetComponent()
        {
            return detailsBar;
        }
    }
}
