using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpHook;
using SharpHook.Native;
using SpaceSim;

namespace GfxMaui
{
    internal class Main
    {

        private static SpaceObjectRenderer renderer;
        private static (bool, bool, bool, bool) wasdPressed;

        public static void Initialize(GraphicsView draw)
        {
            InputManager.Initialize();
            InputManager.OnKeyboardEvent += OnKeyboardEvent;
            InputManager.OnMouseWheelEvent += OnMouseWheelEvent;

            List<SpaceObject> solarSystem = DefaultLoader.LoadDefaultSpaceObjects();

            wasdPressed = (false, false, false, false);

            renderer = new SpaceObjectRenderer(solarSystem);
            renderer.SetTarget(draw);

            // NOTE: Due to library architecture, the positions are never saved and thus nothing can register as event listeners for the 'tick' method
            // The only listener could be the renderer, but having to send in 'Main' and then have 'renderer' attaching to it is messy, so that is avoided here.
            // Instead, see the InputManager class. There are examples of how events are used in this application.
            IDispatcherTimer timer = Application.Current.Dispatcher.CreateTimer();
            timer.Interval = TimeSpan.FromMilliseconds(10);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            renderer.Tick();
            int dx = 0, dy = 0;
            if (wasdPressed.Item1) dy -= 1;
            if (wasdPressed.Item3) dy += 1;
            if (wasdPressed.Item2) dx -= 1;
            if (wasdPressed.Item4) dx += 1;
            renderer.MoveCamera(dx * 10f, dy * 10f);

            renderer.Invalidate();
        }

        private static void OnMouseWheelEvent(object sender, MouseWheelEventArgs args)
        {
            renderer.ApplyZoom(args.Direction);
        }

        private static void OnKeyboardEvent(object sender, KeyboardEventArgs args)
        {
            if (args.Type == KeyboardEventType.KeyDown) OnKeyDown(args.KeyCode);
            else if (args.Type == KeyboardEventType.KeyUp) OnKeyUp(args.KeyCode);

            System.Diagnostics.Debug.WriteLine(String.Format("KeyboardEvent: Type: {0}, KeyCode: {1}", args.Type.ToString(), args.KeyCode.ToString()));
        }

        private static void OnKeyDown(KeyCode keyCode)
        {
            switch (keyCode)
            {
                case KeyCode.W: wasdPressed.Item1 = true; break;
                case KeyCode.A: wasdPressed.Item2 = true; break;
                case KeyCode.S: wasdPressed.Item3 = true; break;
                case KeyCode.D: wasdPressed.Item4 = true; break;
                case KeyCode.M: renderer.CameraFollowNextMoon(false); break;
                case KeyCode.N: renderer.CameraFollowNextPlanet(false); break;
                case KeyCode.K: renderer.CameraFollowNextMoon(true); break;
                case KeyCode.J: renderer.CameraFollowNextPlanet(true); break;
                case KeyCode.L: renderer.CameraFollowClosestPlanetOrStar(); break;
                case KeyCode.I: renderer.AdjustTimeDaysPerTick(0.9f); break;
                case KeyCode.O: renderer.AdjustTimeDaysPerTick(1.1f); break;
            }
        }

        private static void OnKeyUp(KeyCode keyCode)
        {
            switch (keyCode)
            {
                case KeyCode.W: wasdPressed.Item1 = false; break;
                case KeyCode.A: wasdPressed.Item2 = false; break;
                case KeyCode.S: wasdPressed.Item3 = false; break;
                case KeyCode.D: wasdPressed.Item4 = false; break;
            }
        }
    }
}