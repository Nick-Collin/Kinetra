using Kinetra.Core;
using Kinetra.Render;


namespace Kinetra.Physics
{
public class AtomSimulation : FrameUpdateLogic
{
    private float softening = 0.05f;
    private float damping = 0.98f;
    private float couloumb_const = 0.1f; 
    private float sigma = 1f;
    private float epsilon = 1.0f;

    public BoxContainer? container;

    public void Execute(Scene scene, float dt, KeyboardState keyboard, MouseState mouse, Vector2 windowSize, bool isFocused)
    {
        foreach (var entity in scene.Entities)
        {
            if (entity is not Atom a) continue;
                a.force = (0f,0f,0f);
                foreach (var entity2 in scene.Entities)
                {
                    if (entity2 is not Atom b) continue;
                    if (a == b) continue;

                    Vector3 r = b.transform.translation - a.transform.translation;

                    float dist2 = r.LengthSquared + softening * softening;
                    float dist = MathF.Sqrt(dist2);

                    Vector3 dir = r / dist;

                    float coulomb = couloumb_const * (a.charge * b.charge) / dist2;

                    float inv = sigma / dist;
                    float inv6 = inv * inv * inv * inv * inv * inv;
                    float inv12 = inv6 * inv6;

                    float lj = -24f * epsilon * (2 * inv12 - inv6) / dist2;
                    a.force += dir * (coulomb + lj);
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
