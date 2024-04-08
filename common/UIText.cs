using online_osu_beatmap_editor_client.config;
using SFML.Graphics;
using SFML.System;
using System;
using System.Security.Policy;

namespace online_osu_beatmap_editor_client.common
{
    internal class UIText : BaseUIComponent
    {
        private Text text;

        public UIText(string label, Vector2i pos, uint size = 20)
            : base(pos)
        {
            text = new Text(label, StyleVariables.mainFont);
            text.CharacterSize = size;
            text.FillColor = Color.White;
            text.Origin = new Vector2f(text.GetGlobalBounds().Width / 2, text.GetGlobalBounds().Height / 2);

            FloatRect textSize = text.GetGlobalBounds();
            this.size = new Vector2i((int)textSize.Width, (int)textSize.Height);
            text.Origin = new Vector2f(text.GetLocalBounds().Width * this.origin.X, text.GetLocalBounds().Height * this.origin.Y);
            text.Position = (Vector2f)this.pos;
        }

        public override void HandlePositionUpdate(Vector2i pos)
        {
            base.HandlePositionUpdate(pos);
            if (text != null) 
            {
                text.Position = (Vector2f)pos;
            }
        }

        public override void HandleOriginUpdate(Vector2f origin)
        {
            base.HandleOriginUpdate(origin);
            text.Origin = new Vector2f(text.GetLocalBounds().Width * this.origin.X, text.GetLocalBounds().Height * this.origin.Y); ;
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
