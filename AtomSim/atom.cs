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

    public Atom(Vector3 position, Vector3 color, float radius = 0.1f)
        : base(position, color, radius)
        {
            this.Movement = new MovementComponent();
        }

    public Vector3 velocity;
    public float radius;
    public Vector3 force;

    public float protons = 0;
    public float neutrons = 0;
    public float eletrons = 0;

    public float charge => (protons * proton_charge) + (eletrons * eletron_charge);
    public float mass => (protons * proton_mass) + (neutrons * neutron_mass) + (eletrons * eletron_mass);
}
}