﻿using online_osu_beatmap_editor_client.core;
using SFML.Window;
using System;

namespace online_osu_beatmap_editor_client.views.Editor
{
    public delegate void DeleteCircle(object sender, EventArgs e);
    public delegate void TimeForward(object sender, EventArgs e);
    public delegate void TimeBackward(object sender, EventArgs e);

    public class EditorShortcuts : ShortcutManager
    {
        public event DeleteCircle DeleteCircleEvent;
        public event TimeForward TimeForwardEvent;
        public event TimeBackward TimeBackwardEvent;

        public EditorShortcuts()
        {
            RegisterShortcut(new Keyboard.Key[] { Keyboard.Key.Num1 }, () => { EditorData.currentlySelectedEditorTool = EditorTools.Select; });
            RegisterShortcut(new Keyboard.Key[] { Keyboard.Key.Num2 }, () => { EditorData.currentlySelectedEditorTool = EditorTools.Circle; });
            RegisterShortcut(new Keyboard.Key[] { Keyboard.Key.Num3 }, () => { EditorData.currentlySelectedEditorTool = EditorTools.Slider; });
            RegisterShortcut(new Keyboard.Key[] { Keyboard.Key.Num4 }, () => { EditorData.currentlySelectedEditorTool = EditorTools.Spinner; });


            RegisterShortcut(new Keyboard.Key[] { Keyboard.Key.Q }, () => { EditorData.isNewComboActive = !EditorData.isNewComboActive; });
            RegisterShortcut(new Keyboard.Key[] { Keyboard.Key.W }, () => { EditorData.isWhistleActive = !EditorData.isWhistleActive; });
            RegisterShortcut(new Keyboard.Key[] { Keyboard.Key.E }, () => { EditorData.isFinishActive = !EditorData.isFinishActive; });
            RegisterShortcut(new Keyboard.Key[] { Keyboard.Key.R }, () => { EditorData.isClapActive = !EditorData.isClapActive; });
            RegisterShortcut(new Keyboard.Key[] { Keyboard.Key.T }, () => { EditorData.isGridSnapActive = !EditorData.isGridSnapActive; });
            RegisterShortcut(new Keyboard.Key[] { Keyboard.Key.Y }, () => { EditorData.isDistanceSnapActive = !EditorData.isDistanceSnapActive; });

            RegisterShortcut(new Keyboard.Key[] { Keyboard.Key.Delete }, () => { DeleteCircleEvent?.Invoke(null, EventArgs.Empty); });

            RegisterShortcut(new Keyboard.Key[] { Keyboard.Key.Right }, () => { TimeForwardEvent?.Invoke(null, EventArgs.Empty); });
            RegisterShortcut(new Keyboard.Key[] { Keyboard.Key.Left }, () => { TimeBackwardEvent?.Invoke(null, EventArgs.Empty); });
        }
    }
}
