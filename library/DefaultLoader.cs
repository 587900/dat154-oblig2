using SpaceSim;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* Loads some SpaceObjects from data gotten from https://nineplanets.org/solar-system-data/ */

namespace library
{
    public static class DefaultLoader
    {
        // TODO (may skip): File (website) does not detail size, color nor rotationTime (without data gathering each page), so for now, they are hard coded
        public static List<SpaceObject> LoadDefaultSpaceObjects()
        {
            List<SpaceObject> list = new List<SpaceObject>();
            Dictionary<string, SpaceObject> nameToSpaceObject = new Dictionary<string, SpaceObject>();

            string[] csv = Properties.Resources.Nineplanets.Split('\n');

            for (int i = 1; i < csv.Length - 1; ++i)
            {
                string[] fields = csv[i].Split(',');

                if (fields[4] == "") continue; // skip the ones with no orbital period

                SpaceObjectMetadata metadata = new SpaceObjectMetadata
                {
                    Name = fields[0],
                    Number = fields[1],
                    Incl = ParseDouble(fields[5]),
                    Eccen = ParseDouble(fields[6]),
                    Discoverer = fields[7],
                    DiscoveryYear = ParseInt(fields[8]),
                    AKA = fields[9],
                    DiameterKM = ParseDouble(fields[10]) * 1000,
                    Type = fields[13],
                    RotationPeriodDays = ParseDouble(fields[12])
            };

                string orbitName = fields[2];
                double orbitRadiusKM = ParseDouble(fields[3]) * 1000;
                double periodDays = ParseDouble(fields[4]);
                string colorHex = fields[11];

                SpaceObject orbitObject;
                Orbit orbit = null;
                if (nameToSpaceObject.TryGetValue(orbitName, out orbitObject))
                {
                    orbit = new CircularOrbit(orbitObject, orbitRadiusKM);
                }

                double speedKMSec = (orbit == null ? 0 : orbit.CalculateKMSecRequiredForPeriod(periodDays));
                Color color = ColorTranslator.FromHtml(colorHex);

                SpaceObject obj;
                string type = fields[13];
                switch (type)
                {
                    case "Star": obj = new Star(metadata, speedKMSec, color, orbit); break;
                    case "Planet": obj = new Planet(metadata, speedKMSec, color, orbit); break;
                    case "Moon": obj = new Moon(metadata, speedKMSec, color, orbit); break;
                    default: throw new ArgumentException("internal csv must only detail 'star', 'planet' and 'moon', but got : " + type);
                }

                nameToSpaceObject[metadata.Name] = obj;
                list.Add(obj);
            }

            return list;
        }

        private static double ParseDouble(string value)
        {
            if (value == "-" || value == "") return 0;
            value = value.Replace('.', ',');
            return double.Parse(value);
        }

        private static int ParseInt(string value)
        {
            if (value == "-" || value == "") return 0;
            int index = value.IndexOf('.');
            if (index >= 0) value = value.Substring(0, index);
            return int.Parse(value);
        }

    }
}
