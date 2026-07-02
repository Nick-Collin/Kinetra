public class Camera
{
    public Vector3 position = Vector3.Zero;
    public float yaw = 0f;
    public float pitch = 0f;

    private Quaternion Rotation => Quaternion.FromAxisAngle(Vector3.UnitY, yaw) * Quaternion.FromAxisAngle(Vector3.UnitX, pitch);
        
    public Vector3 Forward =>
    Vector3.Normalize(Vector3.Transform(-Vector3.UnitZ, Rotation));

    public Vector3 Right =>
        Vector3.Normalize(Vector3.Transform(Vector3.UnitX, Rotation));

    public Vector3 Up =>
        Vector3.Normalize(Vector3.Transform(Vector3.UnitY, Rotation));
    public Matrix4 GetViewMatrix()
    {
        var f = Forward;
        var r = Right;
        var u = Up;

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