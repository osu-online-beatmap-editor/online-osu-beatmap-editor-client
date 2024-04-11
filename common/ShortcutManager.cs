using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace online_osu_beatmap_editor_client.core
{
    public class ShortcutManager
    {

        private Dictionary<Keyboard.Key[], Action> shortcuts = new Dictionary<Keyboard.Key[], Action>();
        private Dictionary<Keyboard.Key[], bool> shortcutStates = new Dictionary<Keyboard.Key[], bool>();
        private MouseWheelScrollEventArgs moveBufferEvent = null;

        public delegate void ScrollUp(object sender, EventArgs e);
        public delegate void ScrollDown(object sender, EventArgs e);

        public event ScrollUp ScrollUpEvent;
        public event ScrollDown ScrollDownEvent;


        protected static RenderWindow window;

        public static void SetWindow(RenderWindow _window)
        {
            window = _window;
        }

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

            window.MouseWheelScrolled += MouseWheelScrolledHandler;
            moveBufferEvent = null;
        }

        private void MouseWheelScrolledHandler(object sender, MouseWheelScrollEventArgs e)
        {
            if (moveBufferEvent != null)
            {
                return;
            }

            moveBufferEvent = e;

            if (e.Wheel != Mouse.Wheel.VerticalWheel)
            {
                return;
            }

            if (e.Delta > 0)
            {
                ScrollUpEvent?.Invoke(null, EventArgs.Empty);
            }
            else
            {
                ScrollDownEvent?.Invoke(null, EventArgs.Empty);
            }
        }
    }
}
