using System;
using System.Collections.Generic;
using SpaceSim;

class Astronomy
{
    public static void Main()
    {
        // name, radius/distance, orbitalperiod, objectradius, rotantionalperiod, spaceobject, color
        Star sun = new("Sun", 0, 0, 0, 0, null, "Yellow");
        Planet mercury = new("Mercury", 57910, 87.97, 0, 0, sun, "White");
        Planet venus = new("Venus", 108200, 224.7, 0, 0, sun, "Orange");
        Planet earth = new("Earth", 149600, 365.26, 0, 0, sun, "Blue");
        Planet mars = new("Mars", 227940, 686.98, 0, 0, sun, "Red");
        Planet jupiter = new("Jupiter", 778330, 4332.71, 0, 0, sun, "Brown");
        Planet saturn = new("Saturn", 1429400, 10759.5, 0, 0, sun, "Yellow");
        Planet uranus = new("Uranus", 4504300, 30685, 0, 0, sun, "White");
        Planet neptune = new("Neptune", 5913520, 60190, 0, 0, sun, "Blue");

        // Other Dwarf Planets
        DwarfPlanet pluto = new("Pluto", 0, 90550, 0, 0, sun, "Blue");
        DwarfPlanet ceres = new("Ceres", 0, 0, 0, 0, sun, "Yellow");
        DwarfPlanet haumea = new("Haumea", 0, 0, 0, 0, sun, "Red");
        DwarfPlanet makemake = new("Makemake", 0, 0, 0, 0, sun, "Brown");
        DwarfPlanet eris = new("Eris", 0, 0, 0, 0, sun, "Orange");

        // Earth moons
        Moon moon = new("The Moon", 384, 27.32, 0, 0, earth, "White");

        // Mars moon
        Moon phobos = new("Phobos", 9, 0.32, 0, 0, mars, "White");
        Moon deimos = new("Deimos", 23, 1.26, 0, 0, mars, "White");

        // Jupiter moons
        Moon metis = new("Metis", 128, 0.29, 0, 0, jupiter, "White");
        Moon adrastea = new("Adreastea", 129, 0.3, 0, 0, jupiter, "White");
        Moon amalthea = new("Amalthea", 181, 0.5, 0, 0, jupiter, "White");

        // Saturn moons
        Moon pan = new("Pan", 134, 0.58, 0, 0, saturn, "White");
        Moon atlas = new("Atlas", 138, 0.6, 0, 0, saturn, "White");
        Moon prometheus = new("Prometheus", 0.61, 1, 0, 0, saturn, "White");

        // Uranus moons
        Moon cordelia = new("Cordelia", 50, 0.34, 0, 0, uranus, "White");
        Moon ophelia = new("Phelia", 54, 0.38, 0, 0, uranus, "White");
        Moon bianca = new("Bianca", 59, 0.43, 0, 0, uranus, "White");

        // Neptune
        Moon naiad = new("Naiad", 48, 0.29, 0, 0, neptune, "White");
        Moon thalassa = new("Thalassa", 50, 0.31, 0, 0, neptune, "White");
        Moon despina = new("Despina", 53, 0.33, 0, 0, neptune, "White");

        // Pluto
        Moon charon = new("Charon", 20, 6.39, 0, 0, pluto, "White");
        Moon nix = new("Nix", 49, 24.86, 0, 0, pluto, "White");
        Moon hydra = new("Hydra", 65, 38.21, 0, 0, pluto, "White");

        // Asteroids
        AsteroidBelt asteroidBelt = new AsteroidBelt("The Asteroid Belt", 0, 0, 0, 0, sun, "");
        Asteroid eros = new("Eros", 0, 0, 0, 0, asteroidBelt, "");
        Asteroid ida = new("Ida", 0, 0, 0, 0, asteroidBelt, "");
        Asteroid gaspra = new("Gaspra", 0, 0, 0, 0, asteroidBelt, "");

        List<SpaceObject> solarSystem = new()
        {
            sun,
            mercury,
            venus,
            earth,
            mars,
            jupiter,
            saturn,
            uranus,
            neptune,
            pluto,
            ceres,
            haumea,
            makemake,
            eris,
            moon,
            phobos,
            deimos,
            charon,
            nix,
            hydra,
            asteroidBelt,
            eros,
            ida,
            gaspra,
            metis, 
            adrastea,
            amalthea,
            pan,
            atlas,
            prometheus,
            cordelia,
            ophelia,
            bianca,
            naiad,
            thalassa,
            despina
        };

        List<Planet> planetObjects = new()
        {
            earth, mars, jupiter, saturn, uranus, neptune, mercury, venus
        };

        Console.WriteLine("Enter time: ");
        String timeInput = Console.ReadLine();
        int time = Convert.ToInt32(timeInput);

        Console.WriteLine("Enter planet: ");
        String planet = Console.ReadLine();

        if (planet != null && planet != "")
        {
            foreach (var planetObject in planetObjects)
            {
                if (planetObject.name == planet)
                {
                    Console.WriteLine("Planeten sin position på tiden er er " + planetObject.CalcPos(time));
                    foreach (var solarObject in solarSystem)
                    {
                        if (solarObject.orbitalObject == planetObject)
                        {
                            Console.WriteLine("Månen " + solarObject.name + " sin posisjon på tiden er " + solarObject.CalcPos(time));
                        }
                    }
                }
            }
        } else
        {
            Console.WriteLine("Her er ein liste med planeter som går rundt sola: ");
            foreach (var planetObject in planetObjects)
            {
                Console.WriteLine("Planeten " + planetObject.name + " har denne posisjon på denne tida " + planetObject.CalcPos(time));
            }
        }

    }
}