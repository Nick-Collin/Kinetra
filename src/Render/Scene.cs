namespace Kinetra.Render
{
    public class Scene
    {
        private List<RenderObject> objects = new();
        private Queue<RenderObject> pending = new();

        public void Add(RenderObject obj)
        {
            pending.Enqueue(obj);
        }

        public void Update()
        {
            while (pending.Count > 0)
            {
                var obj = pending.Dequeue();
                obj.Load();
                objects.Add(obj);
            }
        }

        public void Render()
        {
            foreach (var obj in objects)
                obj.Render();
        }

        public void Dispose()
        {
            foreach (var obj in objects)
                obj.Dispose();
        }
    }
}