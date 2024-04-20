using System;
using System.Diagnostics;
using client.Game.Config;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Events;
using osuTK.Graphics;

namespace client.Game.Graphics.UserInterface
{
    public partial class IconButton : ClickableContainer
    {
        private Container buttonContainer;
        private Box box;
        public string Icon;
        private bool _IsActive = false;

        public bool IsActive
        {
            get { return _IsActive; }
            set
            {
                _IsActive = value;
                UpdateButton();
            }
        }

        public IconButton(bool test = false)
        {
            AutoSizeAxes = Axes.Both;
            Origin = Anchor.Centre;
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            InternalChild = buttonContainer = new Container
            {
                AutoSizeAxes = Axes.Both,
                Anchor = Anchor.Centre,
                Origin = Anchor.TopLeft,
                Children = new Drawable[]
                {
                    box = new Box
                    {
                        Width = 70,
                        Height = 70,
                        Origin = Anchor.Centre,
                        Colour = IsActive ? Colour4.Blue : Colour4.Red,
                    },
                    new Sprite
                    {
                        Width = 50,
                        Height = 50,
                        Origin = Anchor.Centre,
                        Texture = textures.Get(Icon)
                    },
                }
            };
        }

        public void UpdateButton()
        {
            if (box != null) {
                box.Colour = IsActive ? Colour4.Blue : Colour4.Red;
            }
        }
    }
}
