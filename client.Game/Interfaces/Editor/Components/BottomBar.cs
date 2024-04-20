using client.Game.Config;
using osu.Framework.Allocation;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Textures;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;

namespace client.Game.Interfaces.Editor.Components
{
    public partial class BottomBar : CompositeDrawable
    {

        public BottomBar()
        {

        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            RelativeSizeAxes = Axes.X;
            Height = 50;
            Anchor = Anchor.BottomLeft;
            Origin = Anchor.BottomLeft;
            InternalChild = new Container
            {
                RelativeSizeAxes = Axes.Both,
                Children = new Drawable[]
                {
                    new Box
                    {
                        RelativeSizeAxes = Axes.Both,
                        Colour = Styles.BACKGROUND_SECONDARY_COLOR,
                    }
                }
            };
        }
    }
}
