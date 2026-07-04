using Kinetra.Entities;
using Kinetra.Graphics;

//TODO move mesh criation to MeshFactoy

public class BoxContainer : Entity
{
    public BoxContainer(Vector3 min, Vector3 max)
        : base(CreateMesh(min, max), CreateMaterial())
    {
        this.min = min;
        this.max = max;
        this.renderMode = PrimitiveType.Lines;
    }
    public Vector3 min;
    public Vector3 max;
    public float stiffness = 50f;

    public Vector3 color = new(0.2f, 0.8f, 1f);

    private static float[] CreateVertices(Vector3 min, Vector3 max)
    {
        return new float[]
        {
            // 8 corners
            min.X, min.Y, min.Z,
            min.X, min.Y, max.Z,
            min.X, max.Y, min.Z,
            min.X, max.Y, max.Z,

            max.X, min.Y, min.Z,
            max.X, min.Y, max.Z,
            max.X, max.Y, min.Z,
            max.X, max.Y, max.Z,
        };
    }

    private static uint[] CreateIndices()
    {
        return new uint[]
        {
            0,1,  0,2,  1,3,  2,3,   // left face
            4,5,  4,6,  5,7,  6,7,   // right face
            0,4,  1,5,  2,6,  3,7    // connections
        };
    }

    private static Mesh CreateMesh(Vector3 min, Vector3 max)
    {
        var vertices = CreateVertices(min, max);
        var indices = CreateIndices();

        var mesh = new Mesh(vertices, indices);

        mesh.layout.AddAttribute(0, 3); // position

        return mesh;
    }

    private static SingleColoredMaterial CreateMaterial()
    {
        return new SingleColoredMaterial(new Vector3(0.2f, 0.8f, 1f));
    }

    public void UpdateBounds(Vector3 min, Vector3 max)
    {
        this.min = min;
        this.max = max;

        this.mesh = CreateMesh(min, max);
    }

    public Vector3 Apply(Vector3 pos, Vector3 vel)
    {
        Vector3 force = Vector3.Zero;

        if (pos.X < min.X) force.X += (min.X - pos.X) * stiffness;
        if (pos.X > max.X) force.X += (max.X - pos.X) * stiffness;

        if (pos.Y < min.Y) force.Y += (min.Y - pos.Y) * stiffness;
        if (pos.Y > max.Y) force.Y += (max.Y - pos.Y) * stiffness;

        if (pos.Z < min.Z) force.Z += (min.Z - pos.Z) * stiffness;
        if (pos.Z > max.Z) force.Z += (max.Z - pos.Z) * stiffness;

        return force;
    }

    public void ResolveCollision(ref Vector3 pos, ref Vector3 vel)
    {
        float restitution = 0.8f;

        if (pos.X < min.X)
        {
            pos.X = min.X;
            vel.X = MathF.Abs(vel.X) * restitution;
        }
        else if (pos.X > max.X)
        {
            pos.X = max.X;
            vel.X = -MathF.Abs(vel.X) * restitution;
        }

        if (pos.Y < min.Y)
        {
            pos.Y = min.Y;
            vel.Y = MathF.Abs(vel.Y) * restitution;
        }
        else if (pos.Y > max.Y)
        {
            pos.Y = max.Y;
            vel.Y = -MathF.Abs(vel.Y) * restitution;
        }

        if (pos.Z < min.Z)
        {
            pos.Z = min.Z;
            vel.Z = MathF.Abs(vel.Z) * restitution;
        }
        else if (pos.Z > max.Z)
        {
            pos.Z = max.Z;
            vel.Z = -MathF.Abs(vel.Z) * restitution;
        }
    }
}
