using Kinetra.Core;
namespace Kinetra.Physics
{
    public static class WorldGenerator
    {
        static Random rng = new Random();

        static float Rand(float min, float max)
            => (float)(min + rng.NextDouble() * (max - min));

        static Vector3 RandomVec(float scale)
            => new Vector3(
                Rand(-1f, 1f),
                Rand(-1f, 1f),
                Rand(-1f, 1f)
            ) * scale;

        public static void Generate(Scene scene, BoxContainer container, int nucleiCount, int electronCount)
        {
            GenerateNuclei(scene, container, nucleiCount);
            GenerateFreeElectrons(scene, container, electronCount);
        }

        public static void GenerateNuclei(Scene scene, BoxContainer container, int count)
        {
            for (int i = 0; i < count; i++)
            {
                Vector3 pos = RandomPosition(container);

                var spec = AtomSpec.Random(rng);

                // núcleo NÃO tem elétrons aqui
                var nucleus = new Atom(
                    pos,
                    new Vector3(Rand(0, 1), Rand(0, 1), Rand(0, 1)),
                    spec.Proton,
                    spec.Neutron,
                    0
                );

                nucleus.velocity = RandomVec(0.1f); // mais pesado = mais lento
                scene.Add(nucleus);
            }
        }

        public static void GenerateFreeElectrons(Scene scene, BoxContainer container, int count)
        {
            for (int i = 0; i < count; i++)
            {
                Vector3 pos = RandomPosition(container);

                var electron = new Atom(
                    pos,
                    new Vector3(0.2f, 0.8f, 1f), // cor típica
                    0,
                    0,
                    1
                );

                electron.velocity = RandomVec(1.5f); // elétron mais rápido
                scene.Add(electron);
            }
        }

        static Vector3 RandomPosition(BoxContainer container)
        {
            Vector3 min = container.min;
            Vector3 max = container.max;

            return new Vector3(
                Rand(min.X, max.X),
                Rand(min.Y, max.Y),
                Rand(min.Z, max.Z)
            );
        }
    }
    public struct AtomSpec
    {
        public int Proton;
        public int Neutron;
        public int Electron;

        public static AtomSpec Random(Random rng)
        {
            int p = rng.Next(1, 6);
            int n = rng.Next(0, 7);

            int e = p + rng.Next(-2, 3);
            e = Math.Max(0, e);

            return new AtomSpec
            {
                Proton = p,
                Neutron = n,
                Electron = e
            };
        }
    }

    public static class AtomGenerator
    {
        static Random rng = new Random();

        static float Rand(float min, float max)
            => (float)(min + rng.NextDouble() * (max - min));

        static Vector3 RandomVec(float scale)
            => new Vector3(
                Rand(-1f, 1f),
                Rand(-1f, 1f),
                Rand(-1f, 1f)
            ) * scale;

        public static Atom CreateAtom(Vector3 center, AtomSpec? spec = null)
        {
            var s = spec ?? AtomSpec.Random(rng);

            var atom = new Atom(
                center + RandomVec(0.2f),
                new Vector3(Rand(0, 1), Rand(0, 1), Rand(0, 1)),
                s.Proton,
                s.Neutron,
                s.Electron
            );

            atom.velocity = RandomVec(0.5f);

            return atom;
        }

        public static (int p, int n) CreateNucleus(Vector3 center)
        {
            var spec = AtomSpec.Random(rng);
            return (Math.Max(1, spec.Proton), spec.Neutron);
        }

        public static Atom CreateIon(Vector3 center)
        {
            var spec = AtomSpec.Random(rng);

            // força desequilíbrio leve (íon)
            spec.Electron = Math.Max(0, spec.Proton + rng.Next(-3, 4));

            return CreateAtom(center, spec);
        }
    }
}