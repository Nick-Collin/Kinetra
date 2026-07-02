public class Camera
{
    public Vector3 position = new(0, 0, 0);
    public Vector3 rotation = new(0, 0, 0); // pitch (X), yaw (Y), roll (Z)

    public Matrix4 GetViewMatrix()
    {
        var rot =
            Matrix4.CreateRotationX(rotation.X) *
            Matrix4.CreateRotationY(rotation.Y) *
            Matrix4.CreateRotationZ(rotation.Z);

        var trans = Matrix4.CreateTranslation(-position);

        return rot * trans;
    }

    public Vector3 Forward =>
    Vector3.Normalize(new Vector3(
        MathF.Cos(rotation.X) * MathF.Sin(rotation.Y),
        MathF.Sin(rotation.X),
        MathF.Cos(rotation.X) * MathF.Cos(rotation.Y)
    ));

    public Vector3 Up =>
    Vector3.Normalize(new Vector3(
        MathF.Cos(rotation.X) * MathF.Sin(rotation.Y + MathF.PI / 2),
        MathF.Sin(rotation.X),
        MathF.Cos(rotation.X) * MathF.Cos(rotation.Y + MathF.PI / 2)
    ));

    public Vector3 Backward =>
    Vector3.Normalize(new Vector3(
        MathF.Cos(rotation.X) * MathF.Sin(rotation.Y + MathF.PI),
        MathF.Sin(rotation.X),
        MathF.Cos(rotation.X) * MathF.Cos(rotation.Y + MathF.PI)
    ));

    public Vector3 Left =>
    Vector3.Normalize(new Vector3(
        MathF.Cos(rotation.X) * MathF.Sin(rotation.Y - MathF.PI / 2),
        MathF.Sin(rotation.X),
        MathF.Cos(rotation.X) * MathF.Cos(rotation.Y - MathF.PI / 2)
    ));

    public Vector3 Right =>
    Vector3.Normalize(new Vector3(
        MathF.Cos(rotation.X) * MathF.Sin(rotation.Y + MathF.PI / 2),
        MathF.Sin(rotation.X),
        MathF.Cos(rotation.X) * MathF.Cos(rotation.Y + MathF.PI / 2)
    ));
}