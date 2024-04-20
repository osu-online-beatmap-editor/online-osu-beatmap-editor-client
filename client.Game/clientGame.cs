﻿using client.Game.Core.Shortcuts;
using client.Game.Interfaces.Editor;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Input.Events;
using osu.Framework.Screens;
using osuTK.Input;

namespace client.Game
{
    public partial class clientGame : clientGameBase
    {
        private ScreenStack screenStack;

        [BackgroundDependencyLoader]
        private void load()
        {
            // Add your top-level game components here.
            // A screen stack and sample screen has been provided for convenience, but you can replace it if you don't want to use screens.
            Child = screenStack = new ScreenStack { RelativeSizeAxes = Axes.Both };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            screenStack.Push(new EditorScreen());
        }

        protected override bool OnKeyDown(KeyDownEvent e)
        {
            if (EditorShortcuts.OnKeyDown(e))
                return true;

            return base.OnKeyDown(e);
        }
    }
}
