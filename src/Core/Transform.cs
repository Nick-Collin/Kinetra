namespace Kinetra.Core
{
    public class Transform
    {
        public Vector3 translation;
        public Vector3 rotation;
        public Vector3 scale;

        public Transform()
        {
            translation = Vector3.Zero;
            rotation = Vector3.Zero;
            scale = Vector3.One;
        }

        public Matrix4 GetModelMatrix()
        {
            var t = Matrix4.CreateTranslation(translation);

            var rx = Matrix4.CreateRotationX(MathHelper.DegreesToRadians(rotation.X));
            var ry = Matrix4.CreateRotationY(MathHelper.DegreesToRadians(rotation.Y));
            var rz = Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(rotation.Z));

            var r = rz * ry * rx;

            var s = Matrix4.CreateScale(scale);

            return t * r * s;
        }
    }
}