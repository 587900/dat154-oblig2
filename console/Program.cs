﻿using System;
using System.Collections.Generic;
using System.Drawing;
using library;
using SpaceSim;

class Astronomy
{
    public static void Main()
    {
        List<SpaceObject> solarSystem = DefaultLoader.LoadDefaultSpaceObjects();

        SpaceObject? obj = null;

        while (obj == null)
        {
            Console.WriteLine("Please input the name of a Sun, Planet or Moon:");
            string? name = Console.ReadLine();
            if (name == null) continue;
            obj = solarSystem.Find(obj => obj.Metadata.Name == name);
            if (obj == null) Console.WriteLine("Name not recognized, try another name.\n");
        }

        double days = -1;
        while (days == -1)
        {
            Console.WriteLine("Please input the time as a decimal number in days:");
            string? time = Console.ReadLine();
            if (time == null) continue;
            try { days = double.Parse(time); } catch { Console.WriteLine("Invalid number. Please try again.\n"); }
        }

        obj.DrawInfo(days);

        Console.ReadLine();

    }
}