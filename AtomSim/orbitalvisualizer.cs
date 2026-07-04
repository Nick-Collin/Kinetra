using Kinetra.Graphics;
using Kinetra.Entities;
using Kinetra.Core;
using Kinetra.Render;

namespace Kinetra.Physics
{

    
    public class OrbitalVisualizer
    {
        private const float scale_factor = 1.0f;
        private class UpdateElectronPos : SceneUpdateLogic
        {
            public void Execute(Entity entity, float dt)
            {
                if (entity is ElectronView ev)
                {
                    var orb = ev.atom.GetOrbital(ev.electron);

                    ev.transform.translation =
                        ev.atom.position + SamplePosition(ev.electron, orb, ev.atom);
                }

                if (entity is OrbitalView ov)
                {
                    ov.transform.translation = ov.atom.position;
                }
            }
        }
        private class ElectronView : Point
        {
            public Electron electron;
            public Atom atom;

            public ElectronView(Electron e, Atom atom, Vector3 pos, Vector3 color)
                : base(pos,color, 5f)
            {
                this.electron = e;
                this.atom = atom;
                this.transform.translation = pos;
                this.renderMode = PrimitiveType.Points;
            }
        }
        
        private class OrbitalView : Entity
        {
            public Atom atom;

            public OrbitalView(Mesh mesh, Material mat, Atom atom)
                : base(mesh, mat)
            {
                this.atom = atom;
            }
        }

        public static List<Entity> BuildElectronEntities(Atom atom, Vector3 color, KinetraWindow w)
        {
            var list = new List<Entity>();

            foreach (var e in atom.Electrons)
            {
                var orb = atom.GetOrbital(e);

                Vector3 pos = atom.position + SamplePosition(e, orb, atom);

                var entity = new ElectronView(e, atom, pos, color);
                entity.renderMode = PrimitiveType.Points;

                list.Add(entity);
                w.Scene.Add(entity);
            }

            if (!w.Scene.passes.Any(p => p is UpdateElectronPos))
            {
                w.Scene.passes.Add(new UpdateElectronPos());
            }

            return list;
        }

        static Vector3 SamplePosition(Electron e, Orbital orb, Atom atom)
        {
            float r = UnitMaster.MetersToUnit(atom.GetAtomicRadius() * (float)e.n * (float)e.n);

            var dir = RandomUnitVector();

            var axis = GetPDirection(orb.index);
            var ran = new Random();
            dir = Vector3.Normalize(dir + axis * (float)ran.NextDouble());

            return dir * r;
        }

        static Vector3 RandomUnitVector()
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

        static Vector3 GetPDirection(int index)
        {
            return index switch
            {
                0 => Vector3.UnitX,
                1 => Vector3.UnitY,
                2 => Vector3.UnitZ,
                _ => Vector3.UnitX
            };
        }

        public static List<Entity> BuildOrbitalsEntities(Atom atom, Vector3 color, float alpha, KinetraWindow w)
        {
            var l = new List<Entity>();
            foreach (var o in atom.orbitals)
            {
                var e = BuildOrbital(o, color, alpha);
                if (o.Electrons.Count > 0) 
                {
                    e.transform.translation += atom.position;
                    l.Add(e); 
                    w.Scene.Add(e);
                }
                
            }
            
            return l;
        }

        public static Entity BuildOrbital(Orbital orb, Vector3 color, float alpha)
        {
            float r = UnitMaster.MetersToUnit(orb.atom.GetAtomicRadius() * (float)orb.n * (float)orb.n);

            Mesh mesh;

            if (orb.subshell == 'p')
            {
                mesh = MeshFactory.CreateDumbbell(r, orb.index);
            }
            else
            {
                mesh = MeshFactory.CreateSphere(r, 16);
            }

            var mat = new SingleColoredMaterial(color, alpha);
            var entity = new OrbitalView(mesh, mat, orb.atom);

            return entity;
        }
    }
}