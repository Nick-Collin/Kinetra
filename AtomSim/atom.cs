using Kinetra.Entities;

namespace Kinetra.Physics
{
public class Atom : Point
{
    private const float proton_mass = 1.0f;
    private const float neutron_mass = 1.0f;
    private const float eletron_mass = 0.0005f;
    private const float proton_charge = +1.0f;
    private const float eletron_charge = -1.0f;

    public Atom(Vector3 position, Vector3 color, float protons, int eletrons, float neutrons, float radius = 50.0f)
        : base(position, color)
        {
            this.Movement = new MovementComponent();
            this.electronShells = BuildShells(eletrons);
        }

    public Vector3 velocity;
    public float radius;
    public Vector3 force;

    public float protons = 0;
    public float neutrons = 0;
    public float eletrons = 0;

    public int[] electronShells;

    public float ValenceCapacity => GetValence();
    public float valenceUsed;
    public Vector3 preferredDirection;


    float GetAtomicRadius()
    {
        int shellCount = this.electronShells.Count(s => s > 0);

        float baseRadius = 0.25f + shellCount * 0.35f;

        float zeff = this.EffectiveCharge(); // carga nuclear efetiva

        float contraction = 1f / (1f + 0.15f * zeff);

        return baseRadius * contraction;
    }

    public float charge => (protons * proton_charge) + (eletrons * eletron_charge);
    public float EffectiveCharge()
        {
        float shielding = 0.85f; // aproximação
        float innerElectrons = this.electronShells[0];
        float coreCharge = this.protons - innerElectrons * shielding;
        return coreCharge;
        }

    public float mass => (protons * proton_mass) + (neutrons * neutron_mass) + (eletrons * eletron_mass);

    public static int[] BuildShells(int electrons)
    {
        int[] caps = { 2, 8, 8 };
        int[] shells = new int[caps.Length];

        for (int i = 0; i < caps.Length; i++)
        {
            int fill = Math.Min(electrons, caps[i]);
            shells[i] = fill;
            electrons -= fill;
            if (electrons <= 0) break;
        }

        return shells;
    }
    public float GetValence()
    {
        int outerIndex = electronShells.Length - 1;

        for (int i = electronShells.Length - 1; i >= 0; i--)
        {
            if (electronShells[i] > 0)
            {
                outerIndex = i;
                break;
            }
        }

        int capacity = (outerIndex == 0) ? 2 : 8;

        int outerElectrons = electronShells[outerIndex];

        return capacity - outerElectrons;
    }

    public void UpdatePreferredDirection(List<Atom> neighbors)
    {
        Vector3 accum = Vector3.Zero;

        foreach (var n in neighbors)
        {
            Vector3 r = n.transform.translation - transform.translation;
            float dist2 = r.LengthSquared + 0.0001f;

            float interaction = (charge * n.charge) / dist2;

            accum += Vector3.Normalize(r) * interaction;
        }

        if (accum.LengthSquared > 0.0001f)
            preferredDirection = Vector3.Normalize(accum);
    }
}
}