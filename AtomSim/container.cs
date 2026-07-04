using Kinetra.Entities;
using Kinetra.Graphics;

namespace Kinetra.Physics
{
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

    public void ResolveCollision(ref Vector3 pos, ref Vector3 vel)
    {
        float restitution = 0.8f;

        if (pos.X < min.X)
        {
            pos.X = min.X;
            if (vel.X < 0) vel.X = -vel.X * restitution;
        }
        else if (pos.X > max.X)
        {
            pos.X = max.X;
            if (vel.X > 0) vel.X = -vel.X * restitution;
        }

        if (pos.Y < min.Y)
        {
            pos.Y = min.Y;
            if (vel.Y < 0) vel.Y = -vel.Y * restitution;
        }
        else if (pos.Y > max.Y)
        {
            pos.Y = max.Y;
            if (vel.Y > 0) vel.Y = -vel.Y * restitution;
        }


        if (pos.Z < min.Z)
        {
            pos.Z = min.Z;
            if (vel.Z < 0) vel.Z = -vel.Z * restitution;
        }
        else if (pos.Z > max.Z)
        {
            pos.Z = max.Z;
            if (vel.Z > 0) vel.Z = -vel.Z * restitution;
        }

    }
}
}