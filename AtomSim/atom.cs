using System.ComponentModel;
using System.Diagnostics;
using System.Net.Security;
using Kinetra.Entities;

namespace Kinetra.Physics
{

public class Atom : Point
    {
        public List<Orbital> orbitals = new();
    
        public int p;
        public int n;
        public List<Electron> Electrons = new();
        public float charge => (p - Electrons.Count) * UnitMaster.E_CHARGE_SCALED;

        public Vector3 velocity;
        public Vector3 force;

        public Vector3 position => this.transform.translation;
        public Vector3 prevPosition;
        public float mass =>
            p * UnitMaster.P_MASS_SCALED +
            n * UnitMaster.N_MASS_SCALED +
            Electrons.Count * UnitMaster.E_MASS_SCALED;
        
        public float radius;
        public float sigma; 

        public Atom(Vector3 position, Vector3 color, int p, int n, int e)
            : base(position, color)
        {
            this.p = p;
            this.n = n;

            this.InitializeOrbitals();
            this.DistributeElectrons(e);
            this.ComputeNucleusRadius();
            this.ComputeSigma();


            this.velocity = Vector3.Zero;
            this.force = Vector3.Zero;

            this.Movement = new MovementComponent();
        }

                private void InitializeOrbitals()
        {
            AddSubshell(1, 's');
            AddSubshell(2, 's');
            AddSubshell(2, 'p');
            AddSubshell(3, 's');
            AddSubshell(3, 'p');
            AddSubshell(4, 's');
            AddSubshell(3, 'd');
            AddSubshell(4, 'p');
            AddSubshell(5, 's');
            AddSubshell(4, 'd');
            AddSubshell(5, 'p');
            AddSubshell(6, 's');
            AddSubshell(4, 'f');
            AddSubshell(5, 'd');
            AddSubshell(6, 'p');
            AddSubshell(7, 's');
        }

        public void AddElectron()
        {
            DistributeElectrons(Electrons.Count + 1);
        }

        public void RemoveElectron()
        {
            if (Electrons.Count > 0)
                DistributeElectrons(Electrons.Count - 1);
        }

        public void DistributeElectrons(int totalElectrons)
        {
            Electrons.Clear();

            // clear orbitals
            for (int i = 0; i < orbitals.Count; i++)
            {
                var orb = orbitals[i];
                orb.Electrons.Clear();
                orbitals[i] = orb;
            }

            int remaining = totalElectrons;

            // spin up 
            foreach (var i in Enumerable.Range(0, orbitals.Count))
            {
                if (remaining == 0) break;

                var orb = orbitals[i];

                if (orb.Electrons.Count == 0)
                {
                    var e = new Electron
                    {
                        n = orb.n,
                        subshell = orb.subshell,
                        orbitalIndex = orb.index,
                        spin = Spin.Up
                    };

                    orb.Electrons.Add(e);
                    Electrons.Add(e);
                    orbitals[i] = orb;

                    remaining--;
                }
            }

            // spin down
            foreach (var i in Enumerable.Range(0, orbitals.Count))
            {
                if (remaining == 0) break;

                var orb = orbitals[i];

                if (orb.Electrons.Count == 1)
                {
                    var e = new Electron
                    {
                        n = orb.n,
                        subshell = orb.subshell,
                        orbitalIndex = orb.index,
                        spin = Spin.Down
                    };

                    orb.Electrons.Add(e);
                    Electrons.Add(e);
                    orbitals[i] = orb;

                    remaining--;
                }
            }
        }

        public void ComputeNucleusRadius() // In Units
        {
            int A = p + n;
            float R0 = 1.2e-15f; 

            this.radius = UnitMaster.MetersToUnit(R0 * MathF.Pow(A, 1.0f / 3.0f));
        }

        private void AddSubshell(int n, char subshell)
        {
            int count = subshell switch
            {
                's' => 1,
                'p' => 3,
                'd' => 5,
                'f' => 7,
                _ => 0
            };

            for (int i = 0; i < count; i++)
                orbitals.Add(new Orbital(n, subshell, i, this));
        }
        public Orbital GetOrbital(Electron e)
        {
            return orbitals.First(o => 
                o.n == e.n &&
                o.subshell == e.subshell &&
                o.index == e.orbitalIndex
            );
        }

        public float GetAtomicRadius() 
        {
            double a0 = UnitMaster.BOHR_RADIUS;

            int n_max = Electrons.Count == 0 ? 1 : Electrons.Max(e => e.n);

            var outer = Electrons.MaxBy(e => e.n);
            float Zeff;

            if (outer is not null)
                Zeff = outer.ComputeZeff(this);
            else
                Zeff = 1f;

            Zeff = MathF.Max(Zeff, 1.0f);

            float atomicRadius = (float)a0 * (n_max * n_max) / Zeff;
            return UnitMaster.MetersToUnit(atomicRadius);
        }

    public void ComputeSigma() 
        {
            this.sigma = 2 * GetAtomicRadius(); 
        }
    }
}