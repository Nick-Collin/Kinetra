using Kinetra.Core;
using Kinetra.Render;


namespace Kinetra.Physics
{
public class AtomSimulation : FrameUpdateLogic
    {  
    private float damping = 1 - 1e-30f;
    public BoxContainer? container;


    public void Execute(Scene scene, float dt, KeyboardState keyboard, MouseState mouse, Vector2 windowSize, bool isFocused)
    {   
        List<Atom> atoms = scene.Entities.OfType<Atom>().ToList();
        int steps = 100;
        float h = dt/steps;
        for (int i = 0; i < steps; i++)
        {
            step(h, atoms);
        }
    }

    private void step(float dt, List<Atom> atoms) // Using Verlet Velocity Integration for energy conservation 
    {
        // --- 1. compute forces at t ---
        foreach (var a in atoms)
            a.force = Vector3.Zero;

        for (int i = 0; i < atoms.Count; i++)
        for (int j = i + 1; j < atoms.Count; j++)
        {
            var a = atoms[i];
            var b = atoms[j];

            var force = AtomInteractor.ElectromagneticForce(a, b);
            a.force += force;
            b.force -= force;
        }

        // --- 2. half-step velocity + position update ---
        foreach (var a in atoms)
        {
            float invMass = 1.0f / MathF.Max(a.mass, 1e-30f);
            var accel = a.force * invMass;

            a.velocity += accel * (dt * 0.5f);
            a.transform.translation += a.velocity * dt;

            if (container != null)
                container.ResolveCollision(ref a.transform.translation, ref a.velocity);
        }

        // --- 3. recompute forces at t + dt ---
        foreach (var a in atoms)
            a.force = Vector3.Zero;

        for (int i = 0; i < atoms.Count; i++)
        for (int j = i + 1; j < atoms.Count; j++)
        {
            var a = atoms[i];
            var b = atoms[j];

            var force = AtomInteractor.ElectromagneticForce(a, b);
            a.force += force;
            b.force -= force;
        }

        // --- 4. final half-step velocity ---
        foreach (var a in atoms)
        {
            float invMass = 1.0f / MathF.Max(a.mass, 1e-30f);
            var accel = a.force * invMass;

            a.velocity += accel * (dt * 0.5f);
            a.velocity *= damping;
        }
    }
}
}
