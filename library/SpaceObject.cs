using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceSim
{
    public class SpaceObject
    {

        public String name { get; set; }
        public double orbitalRadius { get; set; }
        public double orbitalPeriod { get; set; }
        public int objectRadius { get; set; }
        public int rotantionalPeriod { get; set; }
        public SpaceObject orbitalObject { get; set; }
        public String color { get; set; }

        public SpaceObject(
            string name, 
            double orbitalRadius, 
            double orbitalPeriod, 
            int objectRadius, 
            int rotantionalPeriod, 
            SpaceObject orbitalObject,
            String color)
        {
            this.name = name;
            this.orbitalRadius = orbitalRadius;
            this.orbitalPeriod = orbitalPeriod;
            this.objectRadius = objectRadius;
            this.rotantionalPeriod = rotantionalPeriod;
            this.orbitalObject = orbitalObject;
            this.color = color;
        }
        public virtual void Draw()
        {
            Console.WriteLine(name);
        }

        public virtual (double, double) CalcPos(int days)
        {
            double x = ((this.orbitalRadius * Math.Cos((360 / this.orbitalPeriod) * (Math.PI / 180)) * days));
            double y = ((this.orbitalRadius * Math.Sin((360 / this.orbitalPeriod) * (Math.PI / 180)) * days));
            return (x, y);
        }
    }

    public class Star : SpaceObject
    {
        public Star(String name, double orbitalRadius, double orbitalPeriod, int objectRadius, int rotantionalPeriod, SpaceObject orbitalObject, String color) 
            : base(name, orbitalRadius, orbitalPeriod, objectRadius, rotantionalPeriod, orbitalObject, color) { }
        public override void Draw()
        {
            Console.Write("Star  : ");
            base.Draw();
        }
    }

    public class Planet : SpaceObject
    {
        public Planet(String name, double orbitalRadius, double orbitalPeriod, int objectRadius, int rotantionalPeriod, SpaceObject orbitalObject, String color)
            : base(name, orbitalRadius, orbitalPeriod, objectRadius, rotantionalPeriod, orbitalObject, color) { }
        public override void Draw()
        {
            Console.Write("Planet: ");
            base.Draw();
        }

        public virtual (double, double) CalcPos(int days)
        {
            double x = ((this.orbitalRadius * Math.Cos((360 / this.orbitalPeriod) * (Math.PI / 180)) * days));
            double y = ((this.orbitalRadius * Math.Sin((360 / this.orbitalPeriod) * (Math.PI / 180)) * days));
            return (x, y);
        }
    }

    public class Moon : SpaceObject
    {
        public Moon (String name, double orbitalRadius, double orbitalPeriod, int objectRadius, int rotantionalPeriod, SpaceObject orbitalObject, String color)
            : base(name, orbitalRadius, orbitalPeriod, objectRadius, rotantionalPeriod, orbitalObject, color) { }
        public override void Draw()
        {
            Console.Write("Moon  : ");
            base.Draw();
        }
        public virtual (double, double) CalcPos(int days)
        {
            double x = ((this.orbitalRadius * Math.Cos((360 / this.orbitalPeriod) * (Math.PI / 180)) * days));
            double y = ((this.orbitalRadius * Math.Sin((360 / this.orbitalPeriod) * (Math.PI / 180)) * days));
            return (x, y);
        }
    }

    public class Comet : SpaceObject
    {
        public Comet(String name, double orbitalRadius, double orbitalPeriod, int objectRadius, int rotantionalPeriod, SpaceObject orbitalObject, String color)
            : base(name, orbitalRadius, orbitalPeriod, objectRadius, rotantionalPeriod, orbitalObject, color) { }

        public override void Draw()
        {
            Console.Write("Comet : ");
            base.Draw();
        }
    }

    public class AsteroidBelt : SpaceObject
    {
        public AsteroidBelt(String name, double orbitalRadius, double orbitalPeriod, int objectRadius, int rotantionalPeriod, SpaceObject orbitalObject, String color)
            : base(name, orbitalRadius, orbitalPeriod, objectRadius, rotantionalPeriod, orbitalObject, color) { }

        public override void Draw()
        {
            base.Draw();
        }
    }

    public class Asteroid : AsteroidBelt
    {
        public Asteroid(String name, double orbitalRadius, double orbitalPeriod, int objectRadius, int rotantionalPeriod, SpaceObject orbitalObject, String color)
            : base(name, orbitalRadius, orbitalPeriod, objectRadius, rotantionalPeriod, orbitalObject, color) { }

        public override void Draw()
        {
            Console.Write("Asteroid : ");
            base.Draw();
        }
    }

    public class DwarfPlanet : SpaceObject
    {
        public DwarfPlanet(String name, double orbitalRadius, double orbitalPeriod, int objectRadius, int rotantionalPeriod, SpaceObject orbitalObject, String color)
            : base(name, orbitalRadius, orbitalPeriod, objectRadius, rotantionalPeriod, orbitalObject, color) { }

        public override void Draw()
        {
            base.Draw();
        }
    }

}
