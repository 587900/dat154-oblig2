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
        public string Name { get; private set; }
        public double SpeedKMseconds { get; private set; }
        public Color Color { get; private set; }
        public double Radius { get; private set; }
        public double RotationPeriod { get; private set; }
        public Orbit Orbit { get; private set; }

        private Point cachedPosition;
        private double cachedPositionTimeSeconds;

        public SpaceObject(string name, double speedKMseconds, Color color, double radius, double rotationPeriod) : this(name, speedKMseconds, color, radius, rotationPeriod, null) { }
        public SpaceObject(string name, double speedKMseconds, Color color, double radius, double rotationPeriod, Orbit orbit)
        {
            this.Name = name;
            this.SpeedKMseconds = speedKMseconds;
            this.Color = color;
            this.Radius = radius;
            this.RotationPeriod = rotationPeriod;
            this.Orbit = orbit;

            cachedPosition = new Point(0, 0);
            cachedPositionTimeSeconds = -1;
        }
        public virtual void Draw()
        {
            Console.WriteLine(Name + " " + GetPosition(0));
        }

        public Point GetPosition(double timeSeconds)
        {
            // turn O(n^2) into O(n) when finding all
            if (timeSeconds != cachedPositionTimeSeconds) UpdateCachedPosition(timeSeconds);
            return new Point(cachedPosition.X, cachedPosition.Y);
        }

        private void UpdateCachedPosition(double timeSeconds)
        {
            cachedPosition = (Orbit == null ? new Point(0, 0) : Orbit.GetPosition(this, timeSeconds));
            cachedPositionTimeSeconds = timeSeconds;
        }
    }

    public class Star : SpaceObject
    {
        public Star(string name, double speedKMseconds, Color color, double radius, double rotationPeriod) : this(name, speedKMseconds, color, radius, rotationPeriod, null) { }
        public Star(string name, double speedKMseconds, Color color, double radius, double rotationPeriod, Orbit orbit) : base(name, speedKMseconds, color, radius, rotationPeriod, orbit) { }
        public override void Draw()
        {
            Console.Write("Star         : ");
            base.Draw();
        }
    }

    public class Planet : SpaceObject
    {
        public Planet(string name, double speedKMseconds, Color color, double radius, double rotationPeriod) : this(name, speedKMseconds, color, radius, rotationPeriod, null) { }
        public Planet(string name, double speedKMseconds, Color color, double radius, double rotationPeriod, Orbit orbit) : base(name, speedKMseconds, color, radius, rotationPeriod, orbit) { }
        public override void Draw()
        {
            Console.Write("Planet       : ");
            base.Draw();
        }
    }

    public class DwarfPlanet : Planet
    {
        public DwarfPlanet(string name, double speedKMseconds, Color color, double radius, double rotationPeriod) : this(name, speedKMseconds, color, radius, rotationPeriod, null) { }
        public DwarfPlanet(string name, double speedKMseconds, Color color, double radius, double rotationPeriod, Orbit orbit) : base(name, speedKMseconds, color, radius, rotationPeriod, orbit) { }
        public override void Draw()
        {
            Console.Write("Dwarf ");
            base.Draw();
        }
    }

    public class Moon : SpaceObject
    {
        public Moon(string name, double speedKMseconds, Color color, double radius, double rotationPeriod) : this(name, speedKMseconds, color, radius, rotationPeriod, null) { }
        public Moon(string name, double speedKMseconds, Color color, double radius, double rotationPeriod, Orbit orbit) : base(name, speedKMseconds, color, radius, rotationPeriod, orbit) { }
        public override void Draw()
        {
            Console.Write("Moon         : ");
            base.Draw();
        }
    }

    public class Comet : SpaceObject
    {
        public Comet(string name, double speedKMseconds, Color color, double radius, double rotationPeriod) : this(name, speedKMseconds, color, radius, rotationPeriod, null) { }
        public Comet(string name, double speedKMseconds, Color color, double radius, double rotationPeriod, Orbit orbit) : base(name, speedKMseconds, color, radius, rotationPeriod, orbit) { }
        public override void Draw()
        {
            Console.Write("Comet        : ");
            base.Draw();
        }
    }

    public class Meteor : SpaceObject
    {
        public Meteor(string name, double speedKMseconds, Color color, double radius, double rotationPeriod) : this(name, speedKMseconds, color, radius, rotationPeriod, null) { }
        public Meteor(string name, double speedKMseconds, Color color, double radius, double rotationPeriod, Orbit orbit) : base(name, speedKMseconds, color, radius, rotationPeriod, orbit) { }
        public override void Draw()
        {
            Console.Write("Meteor       : ");
            base.Draw();
        }
    }

    public class Asteroid : SpaceObject
    {
        public Asteroid(string name, double speedKMseconds, Color color, double radius, double rotationPeriod) : this(name, speedKMseconds, color, radius, rotationPeriod, null) { }
        public Asteroid(string name, double speedKMseconds, Color color, double radius, double rotationPeriod, Orbit orbit) : base(name, speedKMseconds, color, radius, rotationPeriod, orbit) { }
        public override void Draw()
        {
            Console.Write("Asteroid     : ");
            base.Draw();
        }
    }

    public class AsteroidBelt : SpaceObject
    {
        private List<Asteroid> list;
        public AsteroidBelt(string name, double speedKMseconds, Color color, double radius, double rotationPeriod) : this(name, speedKMseconds, color, radius, rotationPeriod, null) { }
        public AsteroidBelt(string name, double speedKMseconds, Color color, double radius, double rotationPeriod, Orbit orbit) : base(name, speedKMseconds, color, radius, rotationPeriod, orbit)
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
