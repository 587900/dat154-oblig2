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
        public double OrbitLengthKM { get; protected set; }
        public Orbit(SpaceObject target)
        {
            Target = target;
        }
        public (double, double) GetPosition(SpaceObject orbiter, double timeDays)
        {
            (double, double) relative = GetRelativePosition(orbiter, timeDays);
            (double, double) host = Target.GetPosition(timeDays);
            return (relative.Item1 + host.Item1, relative.Item2 + host.Item2);
        }
        public abstract (double, double) GetRelativePosition(SpaceObject orbiter, double timeDays);
        public abstract double CalculateKMSecRequiredForPeriod(double periodDays);
    }

    //public class CircularOrbit<T> : Orbit<T> where T : SpaceObject
    public class CircularOrbit : Orbit
    {
        public double RadiusKM { get; private set; }

        public CircularOrbit(SpaceObject target, double radiusKM) : base(target)
        {
            RadiusKM = radiusKM;
            OrbitLengthKM = 2 * radiusKM / 1000 * Math.PI;
        }
        public override (double, double) GetRelativePosition(SpaceObject orbiter, double timeDays)
        {
            double distanceKM = orbiter.SpeedKMseconds * timeDays * 86400;
            double rads = 2 * Math.PI * (distanceKM % OrbitLengthKM) / OrbitLengthKM;

            return (RadiusKM * Math.Cos(rads), RadiusKM * Math.Sin(rads));
        }

        public override double CalculateKMSecRequiredForPeriod(double periodDays)
        {
            return OrbitLengthKM / periodDays / 86400;
        }
    }
}
