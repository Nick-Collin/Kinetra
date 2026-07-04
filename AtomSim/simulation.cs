using Kinetra.Core;
using Kinetra.Render;


namespace Kinetra.Physics
{
public class AtomSimulation : FrameUpdateLogic
{
    
    private float softening = 0.5f;
    private float damping = 0.9f;
    private float couloumb_const = 0.1f;
    private float sigma = 0.8f;
    private float epsilon = 0.1f;


    public BoxContainer? container;

    float ComputeAffinity(Atom a, Atom b)
    {
        float va = a.ValenceCapacity - a.valenceUsed;
        float vb = b.ValenceCapacity - b.valenceUsed;

        float chargeCompatibility = MathF.Exp(-MathF.Abs(a.charge + b.charge));

        float valenceMatch = MathF.Min(va, vb);

        return valenceMatch * chargeCompatibility;
    }

    float ComputeBondStrength(Atom a, Atom b)
    {
        float va = a.ValenceCapacity - a.valenceUsed;
        float vb = b.ValenceCapacity - b.valenceUsed;

        float valenceMatch = MathF.Min(va, vb);

        float chargeBalance =
            MathF.Exp(-MathF.Abs(a.EffectiveCharge() + b.EffectiveCharge()));

        float sizeFactor =
            1f / (1f + MathF.Abs(a.radius - b.radius));

        return valenceMatch * chargeBalance * sizeFactor;
    }

    float ComputeBondRange(Atom a, Atom b)
    {
        float ra = a.radius;
        float rb = b.radius;

        float baseRange = ra + rb;

        float chargeFactor =
            1f - 0.2f * MathF.Abs(a.EffectiveCharge() + b.EffectiveCharge());

        return baseRange * MathF.Max(0.5f, chargeFactor);
    }

    public void Execute(Scene scene, float dt, KeyboardState keyboard, MouseState mouse, Vector2 windowSize, bool isFocused)
    {   
        List<Atom> atoms = scene.Entities.OfType<Atom>().ToList();
        foreach (var a in atoms)
        {
                a.force = (0f,0f,0f);
                foreach (var b in atoms)
                {
                    if (a == b) continue;

                    Vector3 r = b.transform.translation - a.transform.translation;

                    float dist2 = r.LengthSquared + softening * softening;
                    float dist = MathF.Sqrt(dist2);
                    Vector3 dir = r / dist;

                    float invDist = 1f / (dist + softening);

                    // Coulomb
                    float coulomb = couloumb_const * (a.EffectiveCharge() * b.EffectiveCharge()) * invDist * invDist;

                    // LJ
                    float inv = sigma / dist; float inv6 = inv * inv * inv * inv * inv * inv; float inv12 = inv6 * inv6;
                    float lj = -24f * epsilon * (2 * inv12 - inv6) * invDist * invDist;

                    // Bond
                    float bond = -ComputeBondStrength(a,b) * ComputeAffinity(a,b) * MathF.Exp(-dist / ComputeBondRange(a,b));

                    float forceScalar = coulomb + lj + bond;

                    a.force += dir * forceScalar;
                }
            if (container != null)
            {
                container.ResolveCollision(ref a.transform.translation, ref a.velocity);
                a.force += container.Apply(a.transform.translation, a.velocity);
            }

            a.velocity += a.force * dt * damping / MathF.Max(a.mass, 0.0001f); 
            a.transform.translation += a.velocity * dt;
            }
        }
    }
}
