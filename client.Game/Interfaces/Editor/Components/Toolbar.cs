using System;
using System.Diagnostics;
using client.Game.Config;
using client.Game.Graphics.UserInterface;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Textures;
using osu.Framework.Input.Events;
using osuTK;
using osuTK.Input;

namespace client.Game.Interfaces.Editor.Components
{
    public partial class Toolbar : CompositeDrawable
    {
        private Container container;

        private int buttonSize = 70;
        private int gap = 5;

        private IconButton buttonSelect;

        private bool isSelectSelected = false;

        private int getPositionY(int index)
        {
            return (buttonSize + gap) * index;
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            InternalChild = container = new Container
            {
                Y = gap,
                X = gap,
                Children = new Drawable[]
                {
                    new Box
                    {
                        X = -gap,
                        Y = -gap,
                        Origin = Anchor.TopLeft,
                        Width = buttonSize + gap * 2,
                        Height = 1080,
                        Colour = Colour4.Brown,
                    },
                    buttonSelect = new IconButton
                    {
                        Y = getPositionY(0),
                        Origin = Anchor.TopLeft,
                        IsActive = isSelectSelected,
                    },
                    new IconButton
                    {
                        Y = getPositionY(1),
                        Origin = Anchor.TopLeft
                    },
                    new IconButton
                    {
                        Y = getPositionY(2),
                        Origin = Anchor.TopLeft
                    },
                    new IconButton
                    {
                        Y = getPositionY(3),
                        Origin = Anchor.TopLeft
                    },

                    new IconButton
                    {
                        Y = getPositionY(5),
                        Origin = Anchor.TopLeft
                    },
                    new IconButton
                    {
                        Y = getPositionY(6),
                        Origin = Anchor.TopLeft
                    },
                    new IconButton
                    {
                        Y = getPositionY(7),
                        Origin = Anchor.TopLeft
                    },
                    new IconButton
                    {
                        Y = getPositionY(8),
                        Origin = Anchor.TopLeft
                    },
                    new IconButton
                    {
                        Y = getPositionY(9),
                        Origin = Anchor.TopLeft
                    },
                }
            };
            setupEvents();
        }


        private void setupEvents ()
        {
            buttonSelect.Action += () =>
            {
                isSelectSelected = !isSelectSelected;
                buttonSelect.IsActive = isSelectSelected;
            };
        }
    }
}
