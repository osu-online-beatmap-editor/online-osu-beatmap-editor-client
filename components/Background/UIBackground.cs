using online_osu_beatmap_editor_client.common;
using SFML.System;
using System;
using System.IO;

namespace online_osu_beatmap_editor_client.components.Background
{
    public class UIBackground : BaseUIComponent
    {
        private UIImage image;

        public UIBackground(string imagePath): base(new Vector2i(0, 0))
        {
            try { 
                image = new UIImage(imagePath, new Vector2i(0, 0), new Vector2i(853 * 2, 480 * 2));
            } catch { }
        }

        public override void Draw()
        {
            if (image != null)
            {
                image.Draw();
            }
        }

        public override void Update()
        {
   
        }
    }
}
