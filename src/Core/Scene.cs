namespace Kinetra.Core
{

    public interface SceneUpdateLogic
    {
        void Execute(Entity entity, float dt);
    }

    public class Scene
    {
        private readonly List<Entity> entities = [];
        private readonly Queue<Entity> pending = new();
        public Camera camera = new();
        public IReadOnlyList<Entity> Entities => entities;

        public readonly List<SceneUpdateLogic> passes = new();

        public void Add(Entity entity)
        {
            pending.Enqueue(entity);
        }

        public void Update(float dt)
        {
            while (pending.Count > 0)
                entities.Add(pending.Dequeue());

            foreach (var e in entities)
            {
                foreach (var logic in passes)
                    logic.Execute(e, dt);
            }
        }

        public void Dispose()
        {
            foreach (var ent in entities)
                ent.Dispose();
        }
    }
}