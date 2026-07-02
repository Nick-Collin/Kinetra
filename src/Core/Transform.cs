namespace Kinetra.Core
{
    public class Transform
    {
        public Vector3 translation;
        public Quaternion rotation;
    
        public Vector3 scale;

        public Transform()
        {
            translation = Vector3.Zero;
            rotation = Quaternion.Identity;
            scale = Vector3.One;
        }

        public Matrix4 GetModelMatrix()
        {
            var t = Matrix4.CreateTranslation(translation);

            var r = Matrix4.CreateFromQuaternion(rotation);

            var s = Matrix4.CreateScale(scale);

            return t * r * s;
        }

        public void SetEuler(Vector3 eulerDegrees)
{
            var radians = new Vector3(
                MathHelper.DegreesToRadians(eulerDegrees.X),
                MathHelper.DegreesToRadians(eulerDegrees.Y),
                MathHelper.DegreesToRadians(eulerDegrees.Z)
            );

            rotation = Quaternion.FromEulerAngles(radians);
        }
    }
}