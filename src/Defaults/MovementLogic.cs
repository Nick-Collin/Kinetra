using Kinetra.Core;
using Kinetra.Entities;

namespace Kinetra.Defaults
{
public class MovementLogic : SceneUpdateLogic
{
    public void Execute(Entity entity, float dt)
    {
        var m = entity.Movement;
        if (m == null)
            return;

        m.velocity += m.acceleration * dt;
        entity.transform.translation += m.velocity * dt;

        m.angularVelocity += m.angularAcceleration * dt;

        var angVel = m.angularVelocity;

        if (angVel.LengthSquared > 0.0001f)
        {
            float angle = angVel.Length * dt; // radians
            var axis = Vector3.Normalize(angVel);

            var deltaRotation = Quaternion.FromAxisAngle(axis, angle);

            entity.transform.rotation =
                Quaternion.Normalize(deltaRotation * entity.transform.rotation);
        }
    }
}
}