using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using OpenTK.Audio.OpenAL;
using OpenTK.Graphics.ES11;

namespace Kinetra.Physics
{
    public class Bond
    {
        public Atom a;
        public Atom b;

        public Orbital oa;
        public Orbital ob;

        public float alignment;
        public float electronSharing;
        public float baseEnergy;

        public float Strength => alignment * electronSharing * baseEnergy;

        public Bond(Atom a, Atom b, Orbital oa, Orbital ob)
        {
            this.a = a;
            this.b = b;
            this.oa = oa;
            this.ob = ob;
        }
    }
    public class AtomInteractor
    {

        public static Vector3 LennardJonesForce(Atom a, Atom b)
        {
            var r_vec = b.position - a.position;
            float r = MathF.Max(r_vec.Length, 1e-6f);

            var dir = r_vec / r;

            float sigma = a.sigma + b.sigma;
            //Console.WriteLine($"r = {r_vec}, sd = {sigma}");

            float invR2 = 1.0f / (r*r);

            float sigma2 = sigma * sigma;
            float sr2 = sigma2 * invR2;
            float sr6 = sr2 * sr2 * sr2;
            float sr12 = sr6 * sr6;

            float epsilon = UnitMaster.ScaleEnergy(1e-24);

            float forceMag = 24f * epsilon * (2f * sr12 - sr6) / r;
            Console.WriteLine(forceMag);

            return dir * forceMag;
        }
        
        public static Vector3 ElectromagneticForce(Atom a, Atom b)
        {
            var r_vec = a.position - b.position;
            float r = r_vec.Length;

            //Console.WriteLine($"r = {r}, radius_sum = {a.radius + b.radius}");

            if (r < a.radius + b.radius) return Vector3.Zero;

            var dir = r_vec / r;

            float k = UnitMaster.K_SCALED;

            float q1 = a.charge; // already in units of e
            float q2 = b.charge;

            float softening = 0.1f;
            float forceMag = k * q1 * q2 / ((r * r) + (softening * softening));

            return dir * forceMag;
        }
        public static float OrbitalAlignment(Orbital a, Orbital b, Vector3 rDir)
        {
            float da = MathF.Max(0, Vector3.Dot(a.GetDirection(), rDir)); // TODO Atom.GetBondAxis
            float db = MathF.Max(0, Vector3.Dot(b.GetDirection(), -rDir));

            return da * db;
        }
        /*
        public static bool TryElectronTransfer(Atom a, Atom b)
        {
            float energyBefore = SystemEnergy(a, b);

            // simulate swap
            MoveElectron(a, b); // TODO 

            float energyAfter = SystemEnergy(a, b);

            if (energyAfter < energyBefore)
                return true; // keep transfer

            UndoTransfer(); // TODO 
            return false;
        }
        */
        public static bool CanBond(Atom a, Atom b, Orbital oa, Orbital ob, Vector3 rDir)
        {
            float alignment = OrbitalAlignment(oa, ob, rDir);

            float r = (a.position - b.position).Length;
            float r0 = (a.sigma + b.sigma) * 0.5f;

            bool distanceOk = MathF.Abs(r - r0) < r0 * 0.3f;

            bool orbitalsAvailable =
                oa.GetAvailability() > 0 &&
                ob.GetAvailability() > 0;

            return alignment > 0.6f && distanceOk && orbitalsAvailable;
        }
    }

}