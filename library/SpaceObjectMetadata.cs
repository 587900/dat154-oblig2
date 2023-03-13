using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceSim
{
    // Info that is not used in the simulation
    public class SpaceObjectMetadata
    {
        public string Name;
        public string Number;
        public double Incl;
        public double Eccen;
        public string Discoverer;
        public int DiscoveryYear;
        public string AKA;
        public double DiameterKM;
        public string Type;
        public double RotationPeriodDays; // only here because we don't draw textures

        public static SpaceObjectMetadata JustName(string name)
        {
            return new SpaceObjectMetadata { Name = name };
        }

        public override string ToString()
        {
            return String.Format("{0}, {1} : {{ No: {2}, Year: {3}, Diameter Kilometers: {4}, Discoverer: {5} }}", Name, Type, Number, DiscoveryYear, DiameterKM, Discoverer);
        }
    }
}
