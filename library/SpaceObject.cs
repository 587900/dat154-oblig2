using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceSim
{

    public class SpaceObject
    {
        public SpaceObjectMetadata Metadata { get; private set; }
        public double SpeedKMseconds { get; private set; }
        public Color Color { get; private set; }
        public Orbit Orbit { get; private set; }

        private (double, double) cachedPosition;
        private double cachedPositionTimeDays;

        public SpaceObject(SpaceObjectMetadata metadata, double speedKMsec, Color color, Orbit orbit)
        {
            Metadata = metadata;
            SpeedKMseconds = speedKMsec;
            Color = color;
            Orbit = orbit;

            cachedPosition = (0, 0);
            cachedPositionTimeDays = -1;
        }
        public virtual void Draw()
        {
            Console.WriteLine(Metadata.Name + " " + GetPosition(0));
        }

        public void DrawInfo(double timeDays)
        {
            Console.WriteLine(Metadata);
            Console.WriteLine(string.Format("Position after days: {0} = {1}", timeDays, GetPosition(timeDays)));
        }

        public (double, double) GetPosition(double timeDays)
        {
            // turn O(n^2) into O(n) when finding all
            if (timeDays != cachedPositionTimeDays) UpdateCachedPosition(timeDays);
            return (cachedPosition.Item1, cachedPosition.Item2);
        }

        private void UpdateCachedPosition(double timeDays)
        {
            cachedPosition = (Orbit == null ? (0, 0) : Orbit.GetPosition(this, timeDays));
            cachedPositionTimeDays = timeDays;
        }
    }

    public class Star : SpaceObject
    {
        public Star(SpaceObjectMetadata metadata, double speedKMsec, Color color, Orbit orbit) : base(metadata, speedKMsec, color, orbit) { }
        public override void Draw()
        {
            Console.Write("Star         : ");
            base.Draw();
        }
    }

    public class Planet : SpaceObject
    {
        public Planet(SpaceObjectMetadata metadata, double speedKMsec, Color color, Orbit orbit) : base(metadata, speedKMsec, color, orbit) { }
        public override void Draw()
        {
            Console.Write("Planet       : ");
            base.Draw();
        }
    }

    public class DwarfPlanet : Planet
    {
        public DwarfPlanet(SpaceObjectMetadata metadata, double speedKMsec, Color color, Orbit orbit) : base(metadata, speedKMsec, color, orbit) { }
        public override void Draw()
        {
            Console.Write("Dwarf ");
            base.Draw();
        }
    }

    public class Moon : SpaceObject
    {
        public Moon(SpaceObjectMetadata metadata, double speedKMsec, Color color, Orbit orbit) : base(metadata, speedKMsec, color, orbit) { }
        public override void Draw()
        {
            Console.Write("Moon         : ");
            base.Draw();
        }
    }

    public class Comet : SpaceObject
    {
        public Comet(SpaceObjectMetadata metadata, double speedKMsec, Color color, Orbit orbit) : base(metadata, speedKMsec, color, orbit) { }
        public override void Draw()
        {
            Console.Write("Comet        : ");
            base.Draw();
        }
    }

    public class Meteor : SpaceObject
    {
        public Meteor(SpaceObjectMetadata metadata, double speedKMsec, Color color, Orbit orbit) : base(metadata, speedKMsec, color, orbit) { }
        public override void Draw()
        {
            Console.Write("Meteor       : ");
            base.Draw();
        }
    }

    public class Asteroid : SpaceObject
    {
        public Asteroid(SpaceObjectMetadata metadata, double speedKMsec, Color color, Orbit orbit) : base(metadata, speedKMsec, color, orbit) { }
        public override void Draw()
        {
            Console.Write("Asteroid     : ");
            base.Draw();
        }
    }

    public class AsteroidBelt : SpaceObject
    {
        private List<Asteroid> list;
        public AsteroidBelt(SpaceObjectMetadata metadata, double speedKMsec, Color color, Orbit orbit) : base(metadata, speedKMsec, color, orbit)
        {
            list = new List<Asteroid>();
        }
        public override void Draw()
        {
            Console.Write("Asteroid belt: ");
            base.Draw();
        }
        public List<Asteroid> GetAsteroids() { return list; }
        public void AddAsteroid(Asteroid asteroid) { list.Add(asteroid); }
        public bool RemoveAsteroid(Asteroid asteroid) { return list.Remove(asteroid); }
    }
}
