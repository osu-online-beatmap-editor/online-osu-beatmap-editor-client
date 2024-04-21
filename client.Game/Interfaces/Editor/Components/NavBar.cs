using client.Game.Config;
using client.Game.Graphics.UserInterface;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Textures;

namespace client.Game.Interfaces.Editor.Components
{
    public partial class NavBar : CompositeDrawable
    {
        private NavButton buttonFile;
        private NavButton buttonEdit;
        private NavButton buttonView;

        public NavBar() {
            
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            RelativeSizeAxes = Axes.X;
            Height = 30;
            InternalChild = new Container
            {
                RelativeSizeAxes = Axes.Both,
                Children = new Drawable[]
                {
                    new Box
                    {
                        RelativeSizeAxes = Axes.Both,
                        Colour = Styles.BACKGROUND_SECONDARY_COLOR,
                    },
                }
            };
            buttonFile = new NavButton
            {
                Label = "File"
            };
            AddInternal(buttonFile);

            buttonEdit = new NavButton
            {
                Label = "Edit",
                X = buttonFile.X + buttonFile.Width,
            };
            AddInternal(buttonEdit);

            buttonView = new NavButton
            {
                Label = "View",
                X = buttonEdit.X + buttonEdit.Width,
            };
            AddInternal(buttonView);
        }
    }
}
