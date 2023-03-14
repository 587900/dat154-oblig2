using SharpHook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GfxMaui
{

    public delegate void OnKeyboardEvent(object sender, KeyboardEventArgs args);
    public delegate void OnMouseWheelEvent(object sender, MouseWheelEventArgs args);
    public delegate void OnMouseEvent(object sender, MouseEventArgs args);

    public class KeyboardEventArgs : EventArgs
    {
        public KeyCode KeyCode { get; }
        public KeyboardEventType Type { get; }

        public KeyboardEventArgs(KeyCode keycode, KeyboardEventType type) 
        {
            KeyCode = keycode;
            Type = type;
        }
    }

    public class MouseWheelEventArgs : EventArgs
    {
        public int Direction { get; }
        public bool Vertical { get; }
        public int Amount { get; }
        public MouseWheelEventArgs(int direction, bool vertical, int amount)
        {
            Direction = direction;
            Vertical = vertical;
            Amount = amount;
        }
    }

    public class MouseEventArgs : EventArgs
    {
        public short X { get; }
        public short Y { get; }
        public MouseButton Button { get; }
        public MouseEventType Type { get; }
        public MouseEventArgs(short x, short y, MouseButton button, MouseEventType type)
        {
            X = x;
            Y = y;
            Button = button;
            Type = type;
        }
    }

    // Could enter rest, but can't be bothered
    public enum KeyCode
    {
        W = SharpHook.Native.KeyCode.VcW,
        A = SharpHook.Native.KeyCode.VcA,
        S = SharpHook.Native.KeyCode.VcS,
        D = SharpHook.Native.KeyCode.VcD,
        I = SharpHook.Native.KeyCode.VcI,
        O = SharpHook.Native.KeyCode.VcO,
        J = SharpHook.Native.KeyCode.VcJ,
        K = SharpHook.Native.KeyCode.VcK,
        N = SharpHook.Native.KeyCode.VcN,
        M = SharpHook.Native.KeyCode.VcM,
        L = SharpHook.Native.KeyCode.VcL,
        UNKNOWN = -1
    }

    public enum KeyboardEventType
    {
        KeyDown, KeyUp, KeyTyped
    }

    public enum MouseButton
    {
        Left, Right, UNKNOWN = -1
    }

    public enum MouseEventType
    {
        Clicked, Pressed, Released, Moved, Dragged
    }

    internal static class InputManager
    {
        public static event OnKeyboardEvent OnKeyboardEvent;
        public static event OnMouseWheelEvent OnMouseWheelEvent;
        public static event OnMouseEvent OnMouseEvent;

        public static bool appFocused { get; private set; } = true;

        public static void Initialize()
        {
            Application.Current.Windows[0].Activated += (object sender, EventArgs a) => appFocused = true;
            Application.Current.Windows[0].Deactivated += (object sender, EventArgs a) => appFocused = false;

            // MAUI does not have ANY custom user input. I guess RIP cross-platform after all (this is windows only)
            StartSharpHook();
        }

        private static void StartSharpHook()
        {
            TaskPoolGlobalHook hook = new TaskPoolGlobalHook();

            hook.KeyTyped += (object sender, KeyboardHookEventArgs a) =>
            {
                if (!appFocused) return;
                if (OnKeyboardEvent == null) return;
                KeyboardEventArgs args = new KeyboardEventArgs(FromSharpHook(a.Data.KeyCode), KeyboardEventType.KeyTyped);
                OnKeyboardEvent(null, args);
            };
            hook.KeyPressed += (object sender, KeyboardHookEventArgs a) =>
            {
                if (!appFocused) return;
                if (OnKeyboardEvent == null) return;
                KeyboardEventArgs args = new KeyboardEventArgs(FromSharpHook(a.Data.KeyCode), KeyboardEventType.KeyDown);
                OnKeyboardEvent(null, args);
            };
            hook.KeyReleased += (object sender, KeyboardHookEventArgs a) =>
            {
                if (!appFocused) return;
                if (OnKeyboardEvent == null) return;
                KeyboardEventArgs args = new KeyboardEventArgs(FromSharpHook(a.Data.KeyCode), KeyboardEventType.KeyUp);
                OnKeyboardEvent(null, args);
            };

            hook.MouseWheel += (object sender, MouseWheelHookEventArgs a) =>
            {
                if (!appFocused) return;
                if (OnMouseWheelEvent == null) return;
                MouseWheelEventArgs args = new MouseWheelEventArgs(-Math.Sign(a.Data.Rotation), a.Data.Direction == SharpHook.Native.MouseWheelScrollDirection.VerticalDirection, a.Data.Amount);
                OnMouseWheelEvent(null, args);
            };

            hook.MouseClicked += (object sender, MouseHookEventArgs a) =>
            {
                if (!appFocused) return;
                if (OnMouseEvent == null) return;
                MouseEventArgs args = new MouseEventArgs(a.Data.X, a.Data.Y, FromSharpHook(a.Data.Button), MouseEventType.Clicked);
                OnMouseEvent(null, args);
            };
            hook.MousePressed += (object sender, MouseHookEventArgs a) =>
            {
                if (!appFocused) return;
                if (OnMouseEvent == null) return;
                MouseEventArgs args = new MouseEventArgs(a.Data.X, a.Data.Y, FromSharpHook(a.Data.Button), MouseEventType.Pressed);
                OnMouseEvent(null, args);
            };
            hook.MouseReleased += (object sender, MouseHookEventArgs a) =>
            {
                if (!appFocused) return;
                if (OnMouseEvent == null) return;
                MouseEventArgs args = new MouseEventArgs(a.Data.X, a.Data.Y, FromSharpHook(a.Data.Button), MouseEventType.Released);
                OnMouseEvent(null, args);
            };
            hook.MouseMoved += (object sender, MouseHookEventArgs a) =>
            {
                if (!appFocused) return;
                if (OnMouseEvent == null) return;
                MouseEventArgs args = new MouseEventArgs(a.Data.X, a.Data.Y, FromSharpHook(a.Data.Button), MouseEventType.Moved);
                OnMouseEvent(null, args);
            };
            hook.MouseDragged += (object sender, MouseHookEventArgs a) =>
            {
                if (!appFocused) return;
                if (OnMouseEvent == null) return;
                MouseEventArgs args = new MouseEventArgs(a.Data.X, a.Data.Y, FromSharpHook(a.Data.Button), MouseEventType.Dragged);
                OnMouseEvent(null, args);
            };

            Task task = hook.RunAsync();

            Application.Current.Windows[0].Destroying += (object sender, EventArgs a) => hook.Dispose();
        }

        private static KeyCode FromSharpHook(SharpHook.Native.KeyCode code)
        {
            switch (code)
            {
                case SharpHook.Native.KeyCode.VcW: return KeyCode.W;
                case SharpHook.Native.KeyCode.VcA: return KeyCode.A;
                case SharpHook.Native.KeyCode.VcS: return KeyCode.S;
                case SharpHook.Native.KeyCode.VcD: return KeyCode.D;
                case SharpHook.Native.KeyCode.VcI: return KeyCode.I;
                case SharpHook.Native.KeyCode.VcO: return KeyCode.O;
                case SharpHook.Native.KeyCode.VcJ: return KeyCode.J;
                case SharpHook.Native.KeyCode.VcK: return KeyCode.K;
                case SharpHook.Native.KeyCode.VcN: return KeyCode.N;
                case SharpHook.Native.KeyCode.VcM: return KeyCode.M;
                case SharpHook.Native.KeyCode.VcL: return KeyCode.L;
                default: return KeyCode.UNKNOWN;
            }
        }

        private static MouseButton FromSharpHook(SharpHook.Native.MouseButton button)
        {
            switch (button)
            {
                case SharpHook.Native.MouseButton.Button1: return MouseButton.Left;
                case SharpHook.Native.MouseButton.Button2: return MouseButton.Right;
                default: return MouseButton.UNKNOWN;
            }
        }

    }
}
