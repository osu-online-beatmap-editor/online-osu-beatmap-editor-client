using online_osu_beatmap_editor_client.common;
using online_osu_beatmap_editor_client.components.Container;
using online_osu_beatmap_editor_client.config;
using online_osu_beatmap_editor_client.Engine;
using SFML.Graphics;
using SFML.System;

namespace online_osu_beatmap_editor_client.views.Editor
{
    public class EditorTimeline
    {
        private int timeLineHeight = 50;

        private UIContainer timeline;
        private UIRectangle background;

        public EditorTimeline()
        {
            timeline = new UIContainer(new Vector2i(95, 480 * 2 - timeLineHeight), new Vector2i(853 * 2 - 95 - 200, timeLineHeight), 10, ContainerOrientation.Horizontal);

            background = new UIRectangle(new Vector2i(95, 480 * 2 - timeLineHeight), new Vector2i(853 * 2 - 95 - 200, timeLineHeight), StyleVariables.colorBgSecondary);
            UIText currentTime = new UIText(TimeConverter.MillisecondsToTime(EditorData.currentTime), new Vector2i(0,0), 22);
            currentTime.bold = true; 

            timeline.AddElement(currentTime);

            EditorData.CurrentTimeChanged += (s, e) => { currentTime.label = TimeConverter.MillisecondsToTime(EditorData.currentTime); };
        }

        public void Draw()
        {
            background.Draw();
            timeline.Update();
            timeline.Draw();
        }
    }
}
