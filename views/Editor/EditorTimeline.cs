using online_osu_beatmap_editor_client.common;
using online_osu_beatmap_editor_client.components;
using online_osu_beatmap_editor_client.components.Container;
using online_osu_beatmap_editor_client.components.Slider;
using online_osu_beatmap_editor_client.config;
using online_osu_beatmap_editor_client.Engine;
using SFML.Graphics;
using SFML.System;
using System;

namespace online_osu_beatmap_editor_client.views.Editor
{
    public class EditorTimeline
    {
        private int timeLineHeight = 50;
        private int timeLineWidth = 853 * 2 - 95 - 200;

        private UIContainer timeline;
        private UIRectangle background;

        public EditorTimeline()
        {
            timeline = new UIContainer(new Vector2i(95, 480 * 2 - timeLineHeight), new Vector2i(timeLineWidth, timeLineHeight), 10, ContainerOrientation.Horizontal);

            background = new UIRectangle(new Vector2i(95, 480 * 2 - timeLineHeight), new Vector2i(timeLineWidth, timeLineHeight), StyleVariables.colorBgSecondary);
            UIText currentTime = new UIText(TimeConverter.MillisecondsToTime(EditorData.currentTime), new Vector2i(0,0), 22);
            currentTime.bold = true;

            UISpacer spacer = new UISpacer(new Vector2i(0, 0), new Vector2i(5, 0));

            UISlider timelineSlider = new UISlider(new Vector2i(0,0), (int)(timeLineWidth - 45f - currentTime.GetLocalBounds().Width), 50);
            timelineSlider.sliderValue = (int)OsuMath.RemapNumbers(EditorData.currentTime, 0, EditorData.totalTime, 0, 1);
            timelineSlider.ValueChanged += (s,e) => {
                EditorData.currentTime = (int)OsuMath.Lerp(0, EditorData.totalTime, timelineSlider.sliderValue);
            };

            timeline.AddElement(currentTime);
            timeline.AddElement(spacer);
            timeline.AddElement(timelineSlider);

            EditorData.CurrentTimeChanged += (s, e) => { 
                currentTime.label = TimeConverter.MillisecondsToTime(EditorData.currentTime);
                timelineSlider.sliderValue = (float)EditorData.currentTime / EditorData.totalTime;
            };
        }

        public void Draw()
        {
            background.Draw();
            timeline.Update();
            timeline.Draw();
            Console.WriteLine(EditorData.currentTime);
        }
    }
}
