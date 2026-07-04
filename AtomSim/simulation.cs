using Kinetra.Core;
using Kinetra.Render;


namespace Kinetra.Physics
{
public class AtomSimulation : FrameUpdateLogic
    {  
    
    private float damping = 0.999f;
    public BoxContainer? container;


    public void Execute(Scene scene, float dt, KeyboardState keyboard, MouseState mouse, Vector2 windowSize, bool isFocused)
    {   
        List<Atom> atoms = scene.Entities.OfType<Atom>().ToList();
        int steps = 100;
        float h = dt/steps;
        for (int i = 0; i <= steps; i++)
        {
            step(h, atoms);
        }
    }

    private void step(float dt, List<Atom> atoms)
    {

        foreach (var a in atoms)
        {
            a.force = Vector3.Zero;
        }

        for (int i = 0; i < atoms.Count; i++)
        for (int j = i + 1; j < atoms.Count; j++)
        {
            var a = atoms[i];
            var b = atoms[j];

            var force = AtomInteractor.MorseForce(a, b);
            a.force += force;
            b.force -= force;
            Console.Out.WriteLine(force);
        }

        foreach (var a in atoms)
        {
            if (container != null)
            {
                container.ResolveCollision(ref a.transform.translation, ref a.velocity);
                a.force += container.Apply(a.transform.translation, a.velocity);
            }

            /* euler integrator 
            a.velocity += a.force * dt * damping / MathF.Max(a.mass, 0.0001f); 
            a.transform.translation += a.velocity * dt;
            */

            var current = a.transform.translation;

            float invMass = 1.0f / MathF.Max(a.mass, 1e-30f);
            var acceleration = a.force * invMass;

            // Verlet position update
            var next = current + (current - a.prevPosition) * damping + acceleration * dt * dt;

            // update
            a.prevPosition = current;
            a.transform.translation = next;

            // optional: reconstruct velocity (for collisions/debug)
            a.velocity = (next - current) / dt;
        }
    }
}
}
