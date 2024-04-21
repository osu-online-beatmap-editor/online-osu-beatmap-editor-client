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

        public IconButton()
        {
            AutoSizeAxes = Axes.Both;
            Origin = Anchor.Centre;
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            InternalChild = buttonContainer = new Container
            {
                Width = Styles.TOOLBAR_BUTTON_SIZE,
                Height = Styles.TOOLBAR_BUTTON_SIZE,
                Masking = true,
                CornerRadius = Styles.CORNER_RADIUS_DEFAULT,
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Children = new Drawable[]
                {
                    box = new Box
                    {
                        Width = Styles.TOOLBAR_BUTTON_SIZE,
                        Height = Styles.TOOLBAR_BUTTON_SIZE,
                        Origin = Anchor.Centre,
                        Anchor = Anchor.Centre,
                    },
                    new Sprite
                    {
                        Width = getIconSize(),
                        Height = getIconSize(),
                        Origin = Anchor.Centre,
                        Anchor = Anchor.Centre,
                        Texture = textures.Get(Icon)
                    },
                }
            };
            UpdateButton();
        }

        private int getIconSize()
        {
            return Styles.TOOLBAR_BUTTON_SIZE - 20;
        }

        public void UpdateButton()
        {
            if (box != null) {
                box.Colour = Styles.GetButtonColor(IsActive);
            }
        }
    }
}
