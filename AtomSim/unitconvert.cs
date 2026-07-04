using OpenTK.Graphics.OpenGL;

namespace Kinetra.Physics
{
    public class UnitMaster
    {
        public const float WORLD_SCALE = 1.0e-10f; // meters / Unit
        public const float TIME_SCALE =  1e-14f; // Second / Unit
        public const float MASS_SCALE = 1e-27f; // KG / Unit
        public const float CHARGE_SCALE = 1.602176634e-19f; // Couloumb / Unit
        
        public const float FORCE_SCALE = MASS_SCALE * WORLD_SCALE / (TIME_SCALE * TIME_SCALE);
        public const float VELOCITY_SCALE = WORLD_SCALE / TIME_SCALE;
        public const float ACCELERATION_SCALE = WORLD_SCALE / (TIME_SCALE * TIME_SCALE);
        public const float ENERGY_SCALE = FORCE_SCALE * WORLD_SCALE;

        public const float SIGMA_SCALER = 1f;
        public const float EPSILON_SCALER = 1f;
        public const float VISUAL_SCALER = 0.8e-10f;

        public const double PROTON_MASS   = 1.67262192369e-27; // kg
        public const double NEUTRON_MASS  = 1.67492749804e-27; // kg
        public const double ELECTRON_MASS = 9.1093837015e-31;  // kg
        public const double BOHR_RADIUS   = 5.29177210903e-11; // meters
        public const double ELEMENTARY_CHARGE = 1.602176634e-19; // Coulomb
        public const double K = 8.9875517923e9; // Coulomb constant (N·m²/C²)


        public const float K_SCALED =
            (float)(K * CHARGE_SCALE * CHARGE_SCALE /
            (FORCE_SCALE * WORLD_SCALE * WORLD_SCALE));

        public const float P_MASS_SCALED = 
            (float)(PROTON_MASS / MASS_SCALE);

        public const float N_MASS_SCALED = 
            (float)(NEUTRON_MASS / MASS_SCALE);

        public const float E_MASS_SCALED = 
            (float)(ELECTRON_MASS / MASS_SCALE);

        public const float BOHR_R_SCALED = 
            (float)(BOHR_RADIUS / WORLD_SCALE);
        
        public const float E_CHARGE_SCALED = 
            (float)(ELEMENTARY_CHARGE / CHARGE_SCALE);

        public static float MetersToUnit(float m)
        {
            return m / WORLD_SCALE;
        }

        public static float UnitsToMeters(float u)
        {
            return u * WORLD_SCALE;
        }

        public static Vector3 ScaleForce(Vector3 f)
        {
            return f / FORCE_SCALE;
        }

        public static Vector3 ScaleVelocity(Vector3 v)
        {
            return v / VELOCITY_SCALE;
        }

        public static float ScaleMass(double m)
        {
            return (float)(m / MASS_SCALE);
        }
        public static float ScaleEnergy(double m)
        {
            return (float)(m / ENERGY_SCALE);
        }

    }

}