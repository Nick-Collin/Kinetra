using OpenTK.Graphics.OpenGL;

namespace Kinetra.Physics
{
    public class UnitMaster
    {
        public const float WORLD_SCALE = 0.0859e-10f; // meter per units
        public const float SIGMA_SCALER = 1f;
        public const float EPSILON_SCALER = 1f;

        public const float PROTON_MASS = 1.6726219e-27f; // N (s^2) / m
        public const float NEUTRON_MASS = 1.6749275e-27f; // N (s^2) / m
        public const float ELECTRON_MASS = 9.1093837e-31f; // N (s^2) / m
        public const float K = 8.99e9f / (WORLD_SCALE * WORLD_SCALE); //  N*(m^2)/(C^2)

        public static float MetersToUnit(float m)
        {
            return m / WORLD_SCALE;
        }

        public static float UnitsToMeters(float u)
        {
            return u * WORLD_SCALE;
        }

        public static Vector3 ForceIrlToSystem(Vector3 f)
        {
            return f / WORLD_SCALE;
        } 


    }

}