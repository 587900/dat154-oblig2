using SpaceSim;


namespace GfxMaui
{
    internal class SpaceObjectRenderer : IDrawable
    {
        private List<SpaceObject> objects;
        private GraphicsView target;
        private float zoom = 1f;
        private (double, double) camera = (0, 0);
        private SpaceObject cameraFollow = null;
        private SpaceObject cameraFollowLast = null;

        private bool lerping = false;

        private double timeDaysPerTick = 1;
        private double timeDays = 0;
        
        public SpaceObjectRenderer(List<SpaceObject> objects)
        {
            this.objects = objects;
            SetCameraFollow(objects[0]);
        }

        public void SetTarget(GraphicsView target)
        {
            if (this.target != null && this.target.Drawable == this) this.target.Drawable = null; 
            this.target = target;
            target.Drawable = this;
        }

        public void AdjustTimeDaysPerTick(float scale)
        {
            timeDaysPerTick *= scale;
            if (timeDaysPerTick <= 0.001f && scale > 1f) timeDaysPerTick = 0.001f;
            if (timeDaysPerTick <= 0.001f && scale < 1f) timeDaysPerTick = 0f;
        }

        public void ApplyZoom(int direction)
        {
            zoom *= (1f + 0.1f * direction);
            zoom = Math.Max(zoom, .03f);
            zoom = Math.Min(zoom, 1000f);
            //target.Invalidate();
        }

        public void MoveCamera(float x, float y)
        {
            if (x == 0 && y == 0) return;
            camera.Item1 += x / zoom; camera.Item2 += y / zoom;
            cameraFollow = null;
        }

        // reverse = follow previous planet
        public void CameraFollowNextPlanet(bool reverse)
        {
            SpaceObject x = cameraFollowLast;

            while (x is not Star && x is not Planet && x.Metadata.Type != "Star" && x.Metadata.Type != "Planet")
            {
                if (x.Orbit == null) return; // did not find star / planet to choose next of
                x = x.Orbit.Target;
            }

            SpaceObject star = null;
            SpaceObject planet = null;

            if (x is Star || x.Metadata.Type == "Star")
            {
                star = x;
            } 
            else if (x is Planet || x.Metadata.Type == "Planet")
            {
                planet = x;
                star = x.Orbit.Target;
            }

            List<SpaceObject> planets = objects.FindAll(a => a.Orbit != null && a.Orbit.Target == star);
            if (planets.Count == 0) return;
            if (reverse) planets.Reverse();

            // no planet = first, or last planet = first
            if (planets.Last() == planet) planet = null;
            if (planet == null)
            {
                SetCameraFollow(planets[0]);
                return;
            }

            // find next planet after myself
            SpaceObject nextPlanet = null;
            for (int i = 0; i < planets.Count - 1; ++i)
            {
                if (planets[i] == planet)
                {
                    nextPlanet = planets[i + 1];
                    break;
                }
            }

            SetCameraFollow(nextPlanet);
        }

        // Very similar to CameraFollowNextPlanet. They could be merged, probably by using some 'host', 'satellite' phrasing,
        // but the extra objects that were created due to task 2 make things uncessecarily hard
        // reverse = follow previous moon
        public void CameraFollowNextMoon(bool reverse)
        {
            SpaceObject x = cameraFollowLast;

            if (x is not Planet && x is not Moon && x.Metadata.Type != "Planet" && x.Metadata.Type != "Moon") return;

            while (x is not Planet && x is not Moon && x.Metadata.Type != "Planet" && x.Metadata.Type != "Moon")
            {
                if (x.Orbit == null) return; // did not find star / planet to choose next of
                x = x.Orbit.Target;
            }

            SpaceObject planet = null;
            SpaceObject moon = null;

            if (x is Planet || x.Metadata.Type == "Planet")
            {
                planet = x;
            }
            else if (x is Moon || x.Metadata.Type == "Moon")
            {
                moon = x;
                planet = x.Orbit.Target;
            }

            List<SpaceObject> moons = objects.FindAll(a => a.Orbit != null && a.Orbit.Target == planet);
            if (moons.Count == 0) return;
            if (reverse) moons.Reverse();

            // no moon = first, or last moon = first
            if (moons.Last() == moon) moon = null;
            if (moon == null)
            {
                SetCameraFollow(moons[0]);
                return;
            }

            // find next moon after myself
            SpaceObject nextMoon = null;
            for (int i = 0; i < moons.Count - 1; ++i)
            {
                if (moons[i] == moon)
                {
                    nextMoon = moons[i + 1];
                    break;
                }
            }

            SetCameraFollow(nextMoon);
        }

        public void CameraFollowClosestPlanetOrStar()
        {
            SetCameraFollow(FindClosestPlanetOrStarToCamera());
        }

        public SpaceObject FindClosestPlanetOrStarToCamera()
        {
            SpaceObject closest = null;
            double closestDistanceSquared = double.MaxValue;
            foreach (SpaceObject obj in objects)
            {
                if (obj is not Star && obj is not Planet && obj.Metadata.Type != "Star" && obj.Metadata.Type != "Planet") continue;
                (double, double) pos = obj.GetScaledPosition(timeDays, ScaleFunction);
                double dx = camera.Item1 - pos.Item1, dy = camera.Item2 - pos.Item2;
                double distanceSquared = dx * dx + dy * dy;
                if (distanceSquared < closestDistanceSquared)
                {
                    closest = obj;
                    closestDistanceSquared = distanceSquared;
                }
            }
            return closest;
        }

        private void SetCameraFollow(SpaceObject target)
        {
            cameraFollow = target;
            cameraFollowLast = target;
            lerping = true;
        }

        public void Tick()
        {
            timeDays += timeDaysPerTick;

            // The camera code should really be abstracted into it's own class, but oh well. This is just for style points anway :)
            if (cameraFollow != null)
            {
                (double, double) pos = cameraFollow.GetScaledPosition(timeDays, ScaleFunction);
                double dx = camera.Item1 - pos.Item1, dy = camera.Item2 - pos.Item2;
                double distanceSquared = dx*dx + dy*dy;
                if (distanceSquared < 25) lerping = false;

                if (lerping)
                {
                    double lerp;
                    if (timeDaysPerTick * zoom < 1f) lerp = 0.16;
                    else if (timeDaysPerTick * zoom < 2f) lerp = 0.3;
                    else if (timeDaysPerTick * zoom < 3f) lerp = 0.6;
                    else if (timeDaysPerTick * zoom < 3.5f) lerp = 0.9;
                    else lerp = 1.0;

                    camera.Item1 = Util.Lerp(camera.Item1, pos.Item1, lerp);
                    camera.Item2 = Util.Lerp(camera.Item2, pos.Item2, lerp);
                }
                else
                {
                    camera.Item1 = pos.Item1;
                    camera.Item2 = pos.Item2;
                }
            }
        }

        public void Invalidate()
        {
            target.Invalidate();
        }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            canvas.FillColor = Color.FromHex("#000000");
            canvas.FontColor = Color.FromHex("#FFFFFF");
            canvas.FillRectangle(dirtyRect);

            canvas.SaveState();
            canvas.Translate((float)(target.Width / 2 - camera.Item1 * zoom), (float)(target.Height / 2 - camera.Item2 * zoom));
            canvas.Scale(zoom, zoom);
            
            foreach (SpaceObject obj in objects)
            {                
                // Draw orbit
                if (obj.Orbit != null)
                {
                    canvas.StrokeColor = Color.FromHex("#AAAAAA");
                    canvas.StrokeSize = (zoom < 0.1f ? 10f : zoom < 0.25f ? 5f : zoom < 2f ? 1f : 0.1f);
                    (double, double) oScaledPosition = obj.Orbit.Target.GetScaledPosition(timeDays, ScaleFunction);
                    double oScaledRadius = ScaleFunction(obj.GetDistanceToHost(timeDays));
                    canvas.DrawCircle((float)oScaledPosition.Item1, (float)oScaledPosition.Item2, (float)oScaledRadius);
                }

                canvas.FillColor = (obj.ColorHex == "" ? Color.FromHex("#FFFFFF") : Color.FromHex(obj.ColorHex));

                (double, double) point = obj.GetScaledPosition(timeDays, ScaleFunction);
                Point pos = new Point(point.Item1, point.Item2);
                canvas.FillCircle(pos, ScaleFunction(obj.DiameterKM / 2) * 10f);

                canvas.FontSize = 20 / zoom;
                float textOffset = (float)(ScaleFunction(obj.DiameterKM / 2) * 2);
                if (obj is Planet || obj is Star) canvas.DrawString(obj.Metadata.Name, (float)pos.X, (float)pos.Y + textOffset, HorizontalAlignment.Center);

            }

            canvas.Scale(1/zoom, 1/zoom);
            canvas.Translate((float)(camera.Item1 * zoom), (float)(camera.Item2 * zoom));

            // Draw HUD
            int ySize = 20, yOffset;

            canvas.FontSize = 20;
            canvas.DrawString(string.Format("Simulation speed: {0} days per second", timeDaysPerTick * 100), (float)-target.Width / 2, (float)-target.Height / 2 + 20, HorizontalAlignment.Left);
            canvas.DrawString(string.Format("Camera x: {0}, y: {1}", camera.Item1, camera.Item2), (float)-target.Width / 2, (float)-target.Height / 2 + 40, HorizontalAlignment.Left);
            (double, double) p2 = cameraFollowLast.GetScaledPosition(timeDays, ScaleFunction);
            canvas.DrawString(string.Format("{0} x: {1}, y: {2}", cameraFollowLast.Metadata.Name, (int)p2.Item1, (int)p2.Item2), (float)-target.Width / 2, (float)-target.Height / 2 + 60, HorizontalAlignment.Left);
            canvas.DrawString(string.Format("Closest star/planet to camera: {0}", FindClosestPlanetOrStarToCamera().Metadata.Name), (float)-target.Width / 2, (float)-target.Height / 2 + 80, HorizontalAlignment.Left);

            // selected planet
            if (cameraFollow != null)
            {
                string alias = cameraFollow.Metadata.AKA;
                List<string> info = new List<string>();
                info.Add(cameraFollow.Metadata.Name);
                if (alias != "" && alias != "-") info.Add(string.Format("Alias: {0}", alias));
                info.Add(string.Format("Satellite no: {0}", cameraFollow.Metadata.Number));
                info.Add(string.Format("Discovered by: {0} in {1}", cameraFollow.Metadata.Discoverer, cameraFollow.Metadata.DiscoveryYear));
                info.Add(string.Format("Rotation period (days): {0}", cameraFollow.Metadata.RotationPeriodDays));

                yOffset = info.Count * ySize;

                foreach (string s in info)
                {
                    canvas.DrawString(s, (float)target.Width / 2 - 10, (float)target.Height / 2 - yOffset, HorizontalAlignment.Right);
                    yOffset -= ySize;
                }

            }

            // controls
            List<string> controls = new List<string>
            {
                "Scroll to zoom, WASD to move",
                "l for closest planet/star",
                "n/j for next/previous planet",
                "m/k for next/previous moon",
                "i/o to control time"
            };

            yOffset = controls.Count * ySize;

            foreach (string s in controls)
            {
                canvas.DrawString(s, (float)-target.Width / 2 + 10, (float)target.Height / 2 - yOffset, HorizontalAlignment.Left);
                yOffset -= ySize;
            }

            canvas.RestoreState();

        }

        private double ScaleFunction(double value)
        {
            if (value == 0) return 0;
            //return Math.Pow(value, 1 / 2.5) / 12.0;
            //return Math.Pow(value, 1 / 1.5) / 10000;
            //return Math.Log(value, 1.0005);
            return value / 500000;
        }
    }
}
