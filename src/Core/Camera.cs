public class Camera
{
    public Vector3 position = Vector3.Zero;
    public Quaternion rotation = Quaternion.Identity;
        
    public Vector3 Forward =>
    Vector3.Normalize(Vector3.Transform(-Vector3.UnitZ, rotation));

    public Vector3 Right =>
        Vector3.Normalize(Vector3.Transform(Vector3.UnitX, rotation));

    public Vector3 Up =>
        Vector3.Normalize(Vector3.Transform(Vector3.UnitY, rotation));
    public Matrix4 GetViewMatrix()
    {
        var f = Vector3.Normalize(Forward);
        var r = Vector3.Normalize(Right);
        var u = Vector3.Normalize(Up);

        var rot = new Matrix4(
            r.X, u.X, -f.X, 0,
            r.Y, u.Y, -f.Y, 0,
            r.Z, u.Z, -f.Z, 0,
            0,   0,    0,   1
        );

        var trans = Matrix4.CreateTranslation(-position);

        return trans * rot;
    }
}