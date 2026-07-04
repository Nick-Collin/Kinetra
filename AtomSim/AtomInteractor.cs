using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using OpenTK.Audio.OpenAL;
using OpenTK.Graphics.ES11;

namespace Kinetra.Physics
{
    public class AtomInteractor
    {

        public static Vector3 LennardJonesForce(Atom a, Atom b)
        {
            var r_vec = a.position - b.position; // world units
            float r = UnitMaster.UnitsToMeters(r_vec.Length);
            
            var dir = Vector3.Normalize(r_vec);

            // Lennard-Jones
            float sigma = UnitMaster.UnitsToMeters((a.sigma + b.sigma) / 2) * UnitMaster.SIGMA_SCALER;
            float epsilon = 1e-21f * UnitMaster.EPSILON_SCALER; // TODO Calculate epsilon

            float invR = 1.0f / r;
            float sr = sigma * invR;
            float sr6 = MathF.Pow(sr, 6);
            float sr12 = sr6 * sr6;

            float lj = 24 * epsilon * (2 * sr12 - sr6) * invR;
            Vector3 lennardJones = dir * lj;

            return  UnitMaster.ForceIrlToSystem(lennardJones);
        }
        public static Vector3 EletromagneticForce(Atom a, Atom b)
        {
            var r_vec = a.position - b.position; // world units
            float r = UnitMaster.UnitsToMeters(r_vec.Length);
            
            var dir = Vector3.Normalize(r_vec);

            // Coulomb
            float k = UnitMaster.K;
            Vector3 coulomb = dir * (k * a.charge * b.charge) / (r*r);
            return  UnitMaster.ForceIrlToSystem(coulomb);
        }   

        public static Vector3 MorseForce(Atom a, Atom b)
        {
            var r_vec = a.position - b.position;
            float r = r_vec.Length;

            if (r < 1e-6f) return Vector3.Zero;

            var dir = r_vec / r;

            // --- MORSE PARAMETERS (tune these) ---
            float D = 1.0f;     // well depth (energy)
            float a_param = 3.0f; // stiffness
            float r0 = 1.0f;    // equilibrium distance

            // Morse force
            float expTerm = MathF.Exp(-a_param * (r - r0));
            float forceMag = 2 * D * a_param * (1 - expTerm) * expTerm;

            return dir * forceMag;
        }
    }
}