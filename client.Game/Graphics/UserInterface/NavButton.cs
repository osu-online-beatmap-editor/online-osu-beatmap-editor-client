using client.Game.Config;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;

namespace client.Game.Graphics.UserInterface
{
    public partial class NavButton : ClickableContainer
    {
        public string Label;
        private SpriteText text;
        private bool isActive = false;

        public NavButton()
        {
            Action = () =>
            {
                isActive = !isActive;
                Masking = !isActive;
            };
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            RelativeSizeAxes = Axes.Y;
            Masking = !isActive;
            InternalChild = new Container
            {
                RelativeSizeAxes = Axes.Both,
                Children = new Drawable[]
                {
                    new Box
                    {
                        RelativeSizeAxes = Axes.Both,
                        Colour = Styles.BACKGROUND_TERTIARY_COLOR
                    },
                    text = new SpriteText {
                        Text = Label,
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                    },
                    new Container {
                        X = 0,
                        Y = 30,
                        Children = new Drawable[]
                        {
                            new Box
                            {
                                Width = 100,
                                Height = 30,
                                Y = 0,
                                X = 0,
                                Colour = Colour4.Red
                            },
                            new Box
                            {
                                Width = 100,
                                Height = 30,
                                Y = 30,
                                X = 0,
                                Colour = Colour4.Red
                            },
                            new Box
                            {
                                Width = 100,
                                Height = 30,
                                Y = 60, X = 0,
                                Colour = Colour4.Red
                            },
                        }
                    }
                }
            };
            Width = text.Width + 40;
        }
    }
}
