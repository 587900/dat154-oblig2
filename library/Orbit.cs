using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceSim
{
    //public abstract class Orbit<T> where T : SpaceObject
    public abstract class Orbit
    {
        public SpaceObject Target { get; private set; }
        public double OrbitLength { get; protected set; }
        public Orbit(SpaceObject target)
        {
            this.Target = target;
        }
        public Point GetPosition(SpaceObject orbiter, double timeSeconds)
        {
            Point relative = GetRelativePosition(orbiter, timeSeconds);
            Point host = Target.GetPosition(timeSeconds);
            return new Point(relative.X + host.X, relative.Y + host.Y);
        }
        public abstract Point GetRelativePosition(SpaceObject orbiter, double timeSeconds);
    }

    //public class CircularOrbit<T> : Orbit<T> where T : SpaceObject
    public class CircularOrbit : Orbit
    {
        public double Radius { get; private set; }

        public CircularOrbit(SpaceObject target, double radius) : base(target)
        {
            this.Radius = radius;
            this.OrbitLength = 2 * radius * Math.PI;
        }
        public override Point GetRelativePosition(SpaceObject orbiter, double timeSeconds)
        {
            double distance = orbiter.SpeedKMseconds * timeSeconds;
            double rads = 2 * Math.PI * (distance % OrbitLength) / OrbitLength;

            return new Point((int)(Radius * Math.Cos(rads)), (int)(Radius * Math.Sin(rads)));
        }
    }
}
