namespace Kinetra.Core
{
    public class Scene
    {
        private readonly List<Entity> entities = [];
        private readonly Queue<Entity> pending = new();
        public IReadOnlyList<Entity> Entities => entities;

        public void Add(Entity entity)
        {
            pending.Enqueue(entity);
        }

        public void Update()
        {
            while (pending.Count > 0)
            {
                var entity = pending.Dequeue();
                entities.Add(entity);
            }
        }

        public void Dispose()
        {
            foreach (var ent in entities)
                ent.Dispose();
        }
    }
}