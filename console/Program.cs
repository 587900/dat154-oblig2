using System;
using System.Collections.Generic;
using System.Drawing;
using SpaceSim;

class Astronomy
{
    public static void Main()
    {
        Star sun = new Star("Sun", 0, Color.Yellow, 1, 1);
        Planet earth = new Planet("Terra", 400, Color.Green, 1, 1, new CircularOrbit(sun, 400000));
        List<SpaceObject> solarSystem = new List<SpaceObject>
        {
            sun,
            new Planet("Mercury", 100, Color.Brown, 1, 1, new CircularOrbit(sun, 100000)),
            new Planet("Venus", 200, Color.GreenYellow, 1, 1, new CircularOrbit(sun, 200000)),
            earth,
            new Moon("The Moon", 20, Color.WhiteSmoke, 1, 1, new CircularOrbit(earth, 100)),
            new DwarfPlanet("Pluto", 50, Color.LightGoldenrodYellow, 1, 1, new CircularOrbit(sun, 1000000))
        };

        foreach (SpaceObject obj in solarSystem)
        {
            obj.Draw();
        }

        Console.ReadLine();
    }
}