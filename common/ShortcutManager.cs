using SFML.Window;
using System;
using System.Collections.Generic;

namespace online_osu_beatmap_editor_client.core
{
    internal class ShortcutManager
    {

        private Dictionary<Keyboard.Key[], Action> shortcuts = new Dictionary<Keyboard.Key[], Action>();
        private Dictionary<Keyboard.Key[], bool> shortcutStates = new Dictionary<Keyboard.Key[], bool>();

        public void RegisterShortcut(Keyboard.Key[] keys, Action action)
        {
            shortcuts[keys] = action;
            shortcutStates[keys] = false;
        }

        public void CheckShortcuts()
        {
            foreach (var shortcut in shortcuts)
            {
                bool shortcutPressed = true;
                foreach (var key in shortcut.Key)
                {
                    if (!Keyboard.IsKeyPressed(key))
                    {
                        shortcutPressed = false;
                        break;
                    }
                }

                if (shortcutPressed && !shortcutStates[shortcut.Key])
                {
                    shortcut.Value.Invoke();
                    shortcutStates[shortcut.Key] = true;
                }
                else if (!shortcutPressed)
                {
                    shortcutStates[shortcut.Key] = false;
                }
            }
        }
    }
}
