 namespace Kinetra.Physics
{
 public class Orbital
        {
            public int n;
            public char subshell;
            public int index; 
            public List<Electron> Electrons;
            public Atom atom;
            public float occupancy; 

            public Orbital(int n, char subshell, int index, Atom atom)
            {
                this.n = n;
                this.subshell = subshell;
                this.index = index;
                Electrons = new List<Electron>();
                this.atom = atom;
            }

            public bool IsFull => Electrons.Count >= 2;

            public float GetAvailability()
            {
                return 1f - (Electrons.Count / 2f);
            }

            public Vector3 GetDirection()
            {
                return subshell switch
                {
                    'p' => index switch
                    {
                        0 => Vector3.UnitX,
                        1 => Vector3.UnitY,
                        2 => Vector3.UnitZ,
                        _ => Vector3.Zero
                    },

                    'd' => Vector3.UnitX, // simplificação (placeholder)

                    'f' => Vector3.UnitX, // simplificação

                    _ => Vector3.Zero
                };
            }

            public static Vector3 RandomUnitVector()
            {
                float z = Random.Shared.NextSingle() * 2f - 1f;
                float t = Random.Shared.NextSingle() * MathF.PI * 2f;
                float r = MathF.Sqrt(1 - z * z);

                return new Vector3(
                    r * MathF.Cos(t),
                    r * MathF.Sin(t),
                    z
                );
            }
        }
}