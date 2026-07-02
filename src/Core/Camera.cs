namespace Kinetra.Core
{
public class Camera
{
    public Vector3 position = Vector3.Zero;
    public float yaw = 0f;
    public float pitch = 0f;
    public Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(90.0f), 800f / 600f, 0.1f, 100.0f);

    private Quaternion Rotation => Quaternion.FromAxisAngle(Vector3.UnitY, yaw) * Quaternion.FromAxisAngle(Vector3.UnitX, pitch);
        
    public Vector3 Forward =>
    Vector3.Normalize(Vector3.Transform(-Vector3.UnitZ, Rotation));

    public Vector3 Right =>
        Vector3.Normalize(Vector3.Transform(Vector3.UnitX, Rotation));

    public Vector3 Up =>
        Vector3.Normalize(Vector3.Transform(Vector3.UnitY, Rotation));

    public Matrix4 GetViewMatrix()
    {
        
        var rotation = Matrix4.CreateFromQuaternion(Quaternion.Invert(Rotation));
        var translation = Matrix4.CreateTranslation(-position);

        return translation * rotation;
        

    }
}
}