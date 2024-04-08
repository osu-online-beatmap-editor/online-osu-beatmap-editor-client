using online_osu_beatmap_editor_client.config;
using SFML.Graphics;
using SFML.System;

namespace online_osu_beatmap_editor_client.common
{
    internal class UIText : BaseUIComponent
    {
        private Text text;

        public UIText(string label, int posX, int posY, uint size = 20)
            : base(posX, posY)
        {
            text = new Text(label, StyleVariables.mainFont);
            text.CharacterSize = size;
            text.FillColor = Color.White;
            text.Origin = new Vector2f(text.GetGlobalBounds().Width / 2, text.GetGlobalBounds().Height / 2);
            text.Position = new Vector2f(posX, posY);
            this.width = (int)text.GetGlobalBounds().Width;
            this.height = (int)text.GetGlobalBounds().Height;
        }

        public override void Draw()
        {
            window.Draw(text);
        }

        public override void Update()
        {
            
        }
    }
}
